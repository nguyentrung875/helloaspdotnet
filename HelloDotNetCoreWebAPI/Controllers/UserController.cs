using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HelloDotNetCoreWebAPI.Data;
using HelloDotNetCoreWebAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HelloDotNetCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly AppSettings _appSettings;

        public UserController(MyDBContext context, IOptionsMonitor<AppSettings> optionsMonitor)
        {
            _context = context;
            _appSettings = optionsMonitor.CurrentValue;
        }

        //public UserController(MyDBContext context, IConfiguration configuration)
        //{
        //    _context = context;
        //    _appSettings = (AppSettings?)configuration.GetSection("AppSettings");
        //}

        [HttpPost("login")]
        public async Task<IActionResult> login(LoginModel request)
        {
            var user = _context.NguoiDungs.SingleOrDefault(p => 
                p.username == request.username & p.password == request.password );
            if (user == null)
            {
                return Ok(new APIResponse
                {
                    success = false,
                    message = "Invalid username or password"
                });
            } else
            {
                var token = await generateToken(user);

                return Ok(new APIResponse
                {
                    success = true,
                    message = "Authenticate Success",
                    data = token
                });
            }
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> refreshToken(TokenModel model)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenValidateParam = new TokenValidationParameters
            {
                //Tự cấp token, Có thể dùng Oauth, Single sign on (SSO)
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,

                //ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,

            }

            try
            {
                             //check 1: AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(model.accessToken, tokenValidateParam, out var validatedToken);


                //check 2: Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (!result)//false
                    {
                        return Ok(new APIResponse
                        {
                            success = false,
                            message = "Invalid token"
                        });
                    }
                }

            }
            catch (Exception ex)
            { 

            }

        }

        private async Task<TokenModel> generateToken(NguoiDungEntity nguoiDung)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, nguoiDung.email),
                    new Claim(JwtRegisteredClaimNames.UniqueName, nguoiDung.username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                    new Claim("username", nguoiDung.username),
                    new Claim("id", nguoiDung.id.ToString()),

                    //Roles


                }),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey
                    (secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = jwtTokenHandler.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandler.WriteToken(securityToken);

            var refreshToken = this.generateRefreshToken();
            //Lưu database
            var refreshTokenEntity = new RefreshToken
            {
                id = Guid.NewGuid(),
                jwtId = securityToken.Id,
                token = refreshToken,
                isUsed = false,
                isRevoked = false,
                issuedAt = DateTime.UtcNow,
                expiredAt = DateTime.UtcNow.AddHours(1),
            };

            await _context.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();
            return new TokenModel
            {
                accessToken = accessToken,
                refreshToken = refreshToken
            };
        }

        private string generateRefreshToken()
        {
            var random = new byte[32];
            using (var rng = RandomNumberGenerator.Create()) { 
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            }
        }


    }
}
