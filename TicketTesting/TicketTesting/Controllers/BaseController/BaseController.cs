using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketTesting.Controllers
{
    public class BaseController : Controller
    {
        public int GetUserIdFromHeader(HttpRequest request)
        {
            var user = request.Headers["userId"];
            int userId;
            try
            {
                userId = Int32.Parse(user);
            }
            catch
            {
                throw new ValidationException("กรุณาใส่ UserId ในส่วนของ header.");
            }
            return userId;
        }
    }
}
