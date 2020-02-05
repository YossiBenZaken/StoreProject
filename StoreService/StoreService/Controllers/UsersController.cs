using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StoreDataAccess;
namespace StoreService.Controllers
{
    public class UsersController : ApiController
    {
        // get all users /api/Users
        public IEnumerable<Users> Get()
        {
            using (StoreDBEntities entities = new StoreDBEntities())
            {
                return entities.Users.ToList();
            }
        }
        // get user by id /api/Users/1
        public Users Get(int id)
        {
            using (StoreDBEntities entities = new StoreDBEntities())
            {
                return entities.Users.FirstOrDefault(usr => usr.userID == id);
            }
        }
    }
}
