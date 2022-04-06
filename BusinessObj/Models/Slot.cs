using System.ComponentModel.DataAnnotations;
#nullable disable

namespace BusinessObj.Models
{
    public class Slot
    {
        [Key]
        [Required]
        [Display(Name = "Slot")]
        public int SlotID { get; set; }

        [Required]
        [Display(Name = "Thời gian bắt đầu")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "Thời gian kết thúc")]
        public DateTime EndTime { get; set; }   


    }
}
