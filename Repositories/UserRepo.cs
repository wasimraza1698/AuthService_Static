using AuthService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Repositories
{
    public class UserRepo : IUserRepo
    {
        private static List<User> users = new List<User>()
        {
            new User(){UserID = 1,UserName = "wasim1698",Password = "123456"},
            new User(){UserID = 2,UserName = "Raza",Password = "123456"},
            new User(){UserID = 3,UserName = "Mohammad",Password = "123456"},
            new User(){UserID = 4,UserName = "LM",Password = "123456"},
            new User(){UserID = 5,UserName = "KDB",Password = "123456"},
            new User(){UserID = 6,UserName = "Xavi",Password = "123456"},
            new User(){UserID = 7,UserName = "Admin",Password = "Admin@123"},
            new User(){UserID = 8,UserName = "User",Password = "User@123"}
        };
        public User Get(User user)
        {
            return users.SingleOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
        }
    }
}
