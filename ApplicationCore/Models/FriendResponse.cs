using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
namespace ApplicationCore.Models
{
    public class FriendResponse
    {
        [Key]
        public int Id{ get; set; }

        public int AdjectiveId { get; set; }
        public int ClientId { get; set; }
        public int FriendId { get; set; }


        [ForeignKey("AdjectiveId")]
        public virtual Adjective Adjective { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }

        [ForeignKey("FriendId")]
        public virtual Friend Friend { get; set; }
    }
}
