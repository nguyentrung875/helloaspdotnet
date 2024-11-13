namespace HelloDotNetCoreWebAPI.Model
{
    public class HangHoaVM
    {
        public string tenHangHoa { get; set; }
        public string donGia { get; set; }
    }

    public class HangHoa : HangHoaVM
    {
        public Guid maHangHoa { get; set; }
    }
}
