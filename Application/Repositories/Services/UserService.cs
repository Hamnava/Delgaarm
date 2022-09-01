
using Application.Repository.Interfaces;
using Application.ViewModels;
using Infrastracture.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repository.Services
{
    public class UserService :IUserService
    {
        private readonly ApplicationContext _context;
        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public List<UserFullNameViewModel> GetUserForSearchInAutoComplete(string term)
        {
            var query = (from U in _context.Users
                         select new UserFullNameViewModel()
                         {
                             UserFullName = U.UserName + " " + U.Email  + U.EmailPassword,
                             UserId = U.Id
                         }).Where(U => U.UserFullName.Contains(term)).ToList();
            return query;
        }


    }
}
