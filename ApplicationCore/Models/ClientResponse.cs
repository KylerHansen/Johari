using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class ClientResponse
    {
        [Key]
        public int Id { get; set; }     

        [ForeignKey("AdjectiveId")]
        public virtual Adjective AdjectiveId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client ClientId { get; set; }

    }
}
