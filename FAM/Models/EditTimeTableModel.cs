using System.ComponentModel.DataAnnotations;

namespace FAM.Models
{
    public class EditTimeTableModel
    {
        [Required]
        [Display(Name = "Đổi Phòng nào đây anh zai")]
        public int RoomID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Đổi ngày nào đây anh zai")]
        public DateTime NewDateBusy { get; set; }

        [Required]
        [Range(1, 6)]
        [Display(Name = "Slot nào của ngày đó dậy anh zai (từ 1 đến 6)")]
        public int NewSlotCurrent { get; set; }
    }
}
