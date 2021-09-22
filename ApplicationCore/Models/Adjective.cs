using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Models
{
    public class Adjective
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Definition { get; set; }

        [Required]
        /// <summary>
        /// 1 = Positive
        /// 0 = Negative
        /// </summary>
        public byte Type { get; set; }
    }
}
