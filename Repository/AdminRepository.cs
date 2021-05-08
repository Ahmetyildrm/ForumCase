using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public Admin GetAdmin(string username, bool trackChanges) =>
            FindByCondition(c => c.Username.Equals(username), trackChanges)
            .SingleOrDefault();
    }
}
