using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.IdentityEntities
{
    public class AppUserRole : IdentityUserRole<int>
    {
        
    }

    public class AppRole : IdentityRole<int, AppUserRole>
    {
        
    }

    public class AppUserClaim : IdentityUserClaim<int>
    {
        
    }
    public class AppUserLogin : IdentityUserLogin<int>
    {
    }
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
    }
    /*
    public virtual Customer Customer { get; set; }
    public virtual Employee Employee { get; set; }
    */
}
