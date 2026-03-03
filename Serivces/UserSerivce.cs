// UserService.cs
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Models.DTO;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using blogapi.Serivces.Context;

namespace blogapi.Serivces
{
    public class UserService
    {
        private readonly DataContext _context;

        public UserService(DataContext dataContext)
        {
            _context = dataContext;
        }

    internal bool AddUser(CreateAccountDTO userToAdd)
        {
       throw new NotImplementedException();
        }
    }
}
