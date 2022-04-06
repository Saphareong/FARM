using System.ComponentModel.DataAnnotations;

namespace FAM.Models
{
    public class UpgradeTimetableModel
    {
        [Required]
        [Display(Name = "Choose the room (as the location of the class)")]
        public int RoomID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateStart { get; set; } // To specify which day will start the loop

        [Required]
        [Display(Name = "Slot (from 1 to 6)")]
        [Range(1, 6)]
        public int RepeatedSlot { get; set; }

        [Required]
        [Display(Name = "Repeat follow: ")]
        [Range(1, 3)]
        public int RepeatType { get; set; }

        [Required]
        [Display(Name = "Every day/week/month repeated: ")]
        [Range(1, 5)]
        public int RepeatSeperated { get; set; }

        [Required]
        [Display(Name = "Class That you want to create")]
        public string ClassID { get; set; }

        [Required]
        [Display(Name = "Teacher that you want to assign")]
        public string TeacherID { get; set; }

        [Display(Name = "Monday")]
        public bool Monday { get; set; }

        [Display(Name = "Tuesday")]
        public bool Tuesday { get; set; }

        [Display(Name = "Wednesday")]
        public bool Wednesday { get; set; }

        [Display(Name = "Thursday")]
        public bool Thursday { get; set; }

        [Display(Name = "Friday")]
        public bool Friday { get; set; }

        [Display(Name = "Saturday")]
        public bool Saturday { get; set; }

        [Display(Name = "Sunday")]
        public bool Sunday { get; set; }

    }
}
