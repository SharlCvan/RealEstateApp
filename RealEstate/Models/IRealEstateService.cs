using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public interface IRealEstateService  
    {
        public Task<IEnumerable<Propertys>> GetRealEstates(int page, int quantityPerPage);
        public Task<int> GetTotalPages();

        public Task<int> GetTotalUserComments(string UserName);

        public Task<int> GetTotalRealEstateComments(int id);

        public Task<Propertys> GetRealEstate(int id);
        public Task<User> GetUser(string UserName);

        public Task<Comment> PostComment(PostedComment comment);

        public Task<PostedRating> RateUser(PostedRating postedRating);

        public Task<List<Comment>> GetUserComments(string UserName, int page, int quantityPerPage);

        public Task<List<Comment>> GetRealEstateComments(string RealEstateId, int page, int quantityPerPage);

        public Task<PropertysForRegistration> PostANewRealEstate(PropertysForRegistration newRealEstate);
        public Task<bool> UserLoggedInAndValid();
    }
}
