namespace Stats.API.Dto
{
    public class SundayDataDto
    {
        public int SundayDataId { get; set; }
        public int PlatformId { get; set; }
        public string? PlatformName { get; set; }
        public DateTime SundayDataDate { get; set; }
        public int Total { get; set; }
        public bool IsDeleted { get; set; }
    }
}
