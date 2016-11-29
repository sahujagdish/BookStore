using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BookStore.Business;
using Bookstore.Data.Application.Account.Entities;
using Newtonsoft.Json;

namespace BookStoreApi.Controllers
{
    public class UserController : BaseController
    {
        readonly IUserRepository userRepository;

        public UserController(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        [HttpGet()]
        public string Index()
        {
            var data = userRepository.GetUserByUserName("Jagdish","Sahu");
            return JsonConvert.SerializeObject(data);
        }

    }
}
