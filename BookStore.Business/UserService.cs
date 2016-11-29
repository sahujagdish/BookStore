using System.Collections.Generic;
using System.Linq;
using Bookstore.Data.Application.Account.Entities;
using BookStore.Business.Domains.Output;
using Bookstore.Data.Application;

namespace BookStore.Business
{
    public interface IUserRepository
    {
        List<UserOutputModel> GetAll();
        UserOutputModel GetUserById(int Id);
        UserOutputModel GetUserByUserName(string userName, string password);
    }

    public class UserRepository : IUserRepository
    {
        //private List<User> user = new List<User>();
        //private int _nextId = 1;


        private UnitOfWork unitOfWork = new UnitOfWork();
        private Repository<User> userRepository;

        public UserRepository ()
        {
            userRepository = unitOfWork.Repository<User>();
        }

        public List<UserOutputModel> GetAll()
        {
            return (from user in userRepository.AsQueryable() select new UserOutputModel
                        {
                            UserId = user.ID,
                            UserName =user.UserName,
                            Email = user.Email
                            //Address = user.UserProfile.Address

                        }).ToList<UserOutputModel>();
        }

        public UserOutputModel GetUserById(int Id)
        {
            return (from user in userRepository.AsQueryable()
                    select new UserOutputModel
                    {
                        UserId = user.ID,
                        UserName = user.UserName,
                        Email = user.Email
                    }).Where(a => a.UserId == Id).FirstOrDefault<UserOutputModel>();
        }

        public UserOutputModel GetUserByUserName(string userName, string password)
        {
            return (from user in userRepository.AsQueryable()
                    select new UserOutputModel
                    {
                        UserId = user.ID,
                        UserName = user.UserName,
                        Email = user.Email
                    }).Where(a => a.UserName == userName && a.Password == password).FirstOrDefault<UserOutputModel>();
        }
    }
}
