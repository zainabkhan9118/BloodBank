using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBankSystem.BLL
{
    internal class userBLL
    {
        public int user_id { get; set; }
        public String username { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String full_name { get; set; }
        public String contact { get; set; }
        public String address { get; set; }
        public DateTime added_date { get; set; }
        public String image_name { get; set; }

    }
}
