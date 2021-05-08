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
    class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Review> GetAllReviews(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Title)
            .ToList();

        public IEnumerable<Review> GetReviewsByStatus(string Status, bool trackChanges) =>
            FindByCondition(e => e.Status.Equals(Status), trackChanges)
            .OrderBy(e => e.Title)
            .ToList();

        public IEnumerable<Review> GetReviews(Guid userId, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId), trackChanges)
            .OrderBy(e => e.Title);

        public Review GetReview(Guid userId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId) && e.Id.Equals(id),
                trackChanges)
            .SingleOrDefault();

        public Review GetReviewById(Guid id, bool trackChanges) =>
            FindByCondition(e => e.Id.Equals(id),
                trackChanges)
            .SingleOrDefault();

        public void CreateReview(Guid userId, Review review)
        {
            review.UserId = userId;
            Create(review);
        }

        public void DeleteReview(Review review)
        {
            Delete(review);
        }
    }
}
