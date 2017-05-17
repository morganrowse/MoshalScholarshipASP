using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoshalScholarship.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string Body { get; set; }
        [ForeignKey("FromUser")]
        public string FromUserId { get; set; }
        public virtual ApplicationUser FromUser { get; set; }
        [ForeignKey("ToUser")]
        public string ToUserId { get; set; }
        public virtual ApplicationUser ToUser { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
