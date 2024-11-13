namespace HelloDotNetCoreWebAPI.Model
{
    public class APIResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
