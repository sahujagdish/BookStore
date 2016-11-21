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
        readonly IUserRepository repo;

        public UserController(IUserRepository tempProduct)
        {
            this.repo = tempProduct;
        }

        [HttpGet()]
        public string Index()
        {
            var data = repo.GetAll();
            return JsonConvert.SerializeObject(data);
        }

    }
}
