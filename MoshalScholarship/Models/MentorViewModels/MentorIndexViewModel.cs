using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoshalScholarship.Models
{
    public class MentorIndexViewModel
    {
        public IList<Mentor> Mentors { get; set; }
        public Array MentorStatus { get; set; }
    }
}
