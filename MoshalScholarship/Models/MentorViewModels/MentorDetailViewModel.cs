using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MoshalScholarship.Models
{
    public class MentorDetailViewModel
    {
        public Mentor Mentor { get; set; }
        public IList<Message> Messages { get; set; }
    }
}
