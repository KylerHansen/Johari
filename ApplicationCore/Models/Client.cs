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
        public string fName { get; set; }

        [Required]
        public string lName { get; set; }

        [ForeignKey("AspNetUsersId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime birthday { get; set; }

        [Required]
        public string gender { get; set; }
    }
}
