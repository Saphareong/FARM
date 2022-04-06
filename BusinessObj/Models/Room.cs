using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace BusinessObj.Models
{
    public class Room
    {
        [Key]
        [Required]
        public int RoomID { get; set; }

        [Required]
        public string RoomName { get; set; }

        [Required]
        public string Status { get; set; }

        public virtual ICollection<RoomDetail> RoomDetails { get; set; }

    }
}
