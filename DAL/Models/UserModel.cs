using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }
        public string Email { get; set; }
        public int? IdRole { get; set; }
        public string Token { get; set; }
       


    }
}
