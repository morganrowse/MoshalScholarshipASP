using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoshalScholarship.Models
{
    public class Mentee
    {
        public int ID { get; set; }
        [ForeignKey("Mentor")]
        public string MentorId { get; set; }
        public virtual ApplicationUser Mentor { get; set; }
        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual ApplicationUser Student { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndedAt { get; set; }
        public Boolean Accepted { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
