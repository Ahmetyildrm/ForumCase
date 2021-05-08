using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IReviewRepository
    {
        IEnumerable<Review> GetAllReviews(bool trackChanges);
        IEnumerable<Review> GetReviewsByStatus(string Status, bool trackChanges);
        IEnumerable<Review> GetReviews(Guid userId, bool trackChanges);
        Review GetReview(Guid userId, Guid id, bool trackChanges);
        Review GetReviewById(Guid id, bool trackChanges);
        void CreateReview(Guid userId, Review review);
        void DeleteReview(Review review);
    }
}
