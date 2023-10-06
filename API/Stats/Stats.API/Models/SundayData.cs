using System;
using System.ComponentModel.DataAnnotations;

namespace Stats.API.Models
{
    public class SundayData
    {
        [Key]
        public int SundayDataId { get; set; }
        public int PlatformId { get; set; }
        public DateTime SundayDataDate { get; set; }
        public int Total { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModifiedDt { get; set; }

        public Platform Platform { get; set; }  

    }
}
