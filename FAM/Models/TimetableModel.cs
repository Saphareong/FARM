using System.ComponentModel.DataAnnotations;

namespace FAM.Models
{
    public class TimetableModel
    {
        [Required]
        [Display(Name = "Chọn phòng học")]
        public int RoomID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; } // To specify which day will start the loop

        /*
        [Required]
        [Display(Name = "Muốn lặp lại vào thứ mấy?")]
        [Range(1, 3)]
        public int FixedDayOfAWeek { get; set; } //First = 2, 4, 6. Second = 3, 5. Third = 7. Or Even more
        */

        [Required]
        [Display(Name = "Slot (from 1 to 6)")]
        [Range(1, 10)]
        public int RepeatedSlot { get; set; }

        [Required]
        [Display(Name = "Class That you want to create")]
        public string ClassID { get; set; }

        [Required]
        [Display(Name = "Teacher that you want to assign")]
        public string TeacherID { get; set; }

    }
}
