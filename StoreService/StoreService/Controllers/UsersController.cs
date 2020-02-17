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
        StoreDBEntities entities = new StoreDBEntities();
        // get all users /api/Users
        public IEnumerable<Users> Get()
        {
            if (Request.RequestUri.Query !=null)
            {
                return Enumerable.Empty<Users>();
            }
            return entities.Users.ToList();
        }
        // get user by id /api/Users/1
        public Users Get(int id)
        {
            return entities.Users.FirstOrDefault(usr => usr.userID == id);
        }
        // get user by email /api/Users?email={email}
        public HttpResponseMessage Get(string email, string password)
        {
            try
            {
                Users user = entities.Users.FirstOrDefault(usr => usr.email.Contains(email) && usr.psword ==password);
                if(user != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                } else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                }
            }
            catch(HttpResponseException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
        // register
        public HttpResponseMessage Post([FromBody] Users user)
        {
            try
            {
                entities.Users.Add(user);
                entities.SaveChanges();
                var message = Request.CreateResponse(HttpStatusCode.Created, "User Created");
                message.Headers.Location = new Uri(Request.RequestUri + user.userID.ToString());
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
