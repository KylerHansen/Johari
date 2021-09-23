using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace ApplicationCore.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string First_Name { get; set; }

        [Required]
        public string Last_Name { get; set; }

        public int AspNetUsersId { get; set; }  

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string Gender { get; set; }

        [ForeignKey("AspNetUsersId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
