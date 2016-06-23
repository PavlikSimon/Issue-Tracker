using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email in wrong format")]
        public string Email { get; set; }
        public string Password { get; set; }
        [RegularExpression(@"customer|employee|admin", ErrorMessage = "SecretCode is users role, must be 'customer' or 'employee' or 'admin' ")]
        public string SecretCode { get; set; }

    }
}