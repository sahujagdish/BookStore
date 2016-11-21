using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Data.Application.Account.Entities;
namespace BookStore.Business
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
    }

    public class UserRepository : IUserRepository
    {
        private List<User> user = new List<User>();
        private int _nextId = 1;

        public UserRepository ()
        {
            // Add products for the Demonstration  
            Add(new User { UserName = "chenoa", Email = "Engenering@chenoa.com" });
            Add(new User { UserName = "Wipro", Email = "software@wipro.com" });
            Add(new User { UserName = "HSBC", Email = "Bank@HSBC.com" });
        }

        public IEnumerable<User> GetAll()
        {
            return user;
        }


        public User Add(User item)
        {
            item.ID = _nextId++;
            user.Add(item);
            return item;
        }
    }
}
