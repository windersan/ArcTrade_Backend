using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArcTrade
{
    public class User
    {
        public int Id { get; set; }
        public UsernamePasswordPair Login {get;set;}
        public string Usertype { get; set; }
        public int Authorization { get; set; }
    }
}
