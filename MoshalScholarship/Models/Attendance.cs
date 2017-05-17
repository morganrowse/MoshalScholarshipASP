using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoshalScholarship.Models
{
    public class Attendance
    {
        public int ID { get; set; }
        [ForeignKey("Event")]
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser ToUser { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
