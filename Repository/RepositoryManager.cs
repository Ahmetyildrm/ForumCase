using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IUserRepository _userRepository;
        private IReviewRepository _reviewRepository;
        private IAdminRepository _adminRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);

                return _userRepository;
            }
        }

        public IReviewRepository Review
        {
            get
            {
                if (_reviewRepository == null)
                    _reviewRepository = new ReviewRepository(_repositoryContext);

                return _reviewRepository;
            }
        }

        public IAdminRepository Admin
        {
            get
            {
                if (_adminRepository == null)
                    _adminRepository = new AdminRepository(_repositoryContext);

                return _adminRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
