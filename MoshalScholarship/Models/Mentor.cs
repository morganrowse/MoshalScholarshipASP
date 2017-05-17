using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MoshalScholarship.Models
{
    public class Mentor
    {
        public int ID { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public String Degree { get; set; }
        public String Institution { get; set; }
        public String Faculty { get; set; }
        public String IdentityNumber { get; set; }
        public String PassportNumber { get; set; }
        public String WorkLocation { get; set; }
        public String CurrentCompany { get; set; }
        public String CurrentJob { get; set; }
        public String StudentCount { get; set; }
        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String AddressLine3 { get; set; }
        public String AddressLine4 { get; set; }
    }
}
