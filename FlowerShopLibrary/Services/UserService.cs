using FlowerShopLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopLibrary.Services
{
    public class UserService : IUserService
    {
        private readonly IDataContext _idatacontext;

        public UserService(IDataContext idatacontext)
        {
            _idatacontext = idatacontext;
        }

        //public IQueryable<User> GetUsers()
        //{
        //    return _idatacontext.Users;
        //}

    }

    public interface IUserService
    {
      
    }
}
