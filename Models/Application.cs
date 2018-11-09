using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTrade
{
    public class Application
    {
        int Id { get; set; }
        int UserId { get; set; }
        int ResumeId { get; set; }
        int Salary { get; set; }

        string FirstName { get; set; }
        string LastName { get; set; }
        string Gender { get; set; }

        DateTime DateApplied { get; set; }
        string Job { get; set; }

        string Address { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
    }
}
