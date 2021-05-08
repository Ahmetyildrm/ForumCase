using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(bool trackChanges);
        User GetUser(Guid userId, bool trackChanges);
        void CreateUser(User user);
        User GetUserWithEmail(string email, bool trackChanges);
        
    }
}
