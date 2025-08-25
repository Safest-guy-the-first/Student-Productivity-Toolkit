namespace SPT_API.Data.DTOs
{
    public class updateStudentDTO
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? department { get; set; }
        public string? level { get; set; }
        public uint? _numLevel { get; set; }
        public string? email { get; set; }
    }
}
