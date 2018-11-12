using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTrade
{
    public class Application
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ResumeId { get; set; }
        public int Salary { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }

        public DateTime DateApplied { get; set; }
        public string Job { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }

        public string ApplicationStatus { get; set; }
    }
}
