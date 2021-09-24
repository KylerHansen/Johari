using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class Friend
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Relationship { get; set; }

        [Required]
        public string HowLong { get; set; }

        public string AspNetUsersId { get; set; }

        [ForeignKey("AspNetUsersId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
