using System;
using System.ComponentModel.DataAnnotations;

namespace Stats.API.Models
{
    public class Platform
    {
        [Key]
        public int PlatformId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime LastModifiedDt { get; set; }

        public ICollection<SundayData> SundayDatas { get; set; }
    }
}
