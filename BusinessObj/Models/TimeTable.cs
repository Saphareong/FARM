using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace BusinessObj.Models
{
    public class TimeTable
    {
        [Key]
        [Required]
        public int TimeTableID { get; set; }

        public string ClassID { get; set; }

        public string AccountId { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [ForeignKey("ClassID")]
        public virtual Class Class { get; set; }

        [ForeignKey("AccountId")]
        public virtual User User { get; set; }

        public virtual ICollection<RoomDetail> RoomDetails { get; set; }
    }
}
