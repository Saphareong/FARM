using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable

namespace BusinessObj.Models
{
    public class UserClass
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string AccountId { get; set; }


        [ForeignKey("AccountId")]
        public virtual User User { get; set; }


        [Required]
        public string SubjectID { get; set; }

        [ForeignKey("SubjectID")]
        public virtual  Subject Subject { get; set; }


        [Required]
        public string ClassID { get; set; }

        [ForeignKey("ClassID")]
        public virtual Class Class { get; set; }



    }
}
