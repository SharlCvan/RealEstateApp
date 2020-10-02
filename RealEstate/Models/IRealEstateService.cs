using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public interface IRealEstateService  
    {
        public Task<IEnumerable<Propertys>> GetRealEstates();
        public Task<Propertys> GetRealEstate(int id);
        public Task<User> GetUser(string UserName);

        public Task<Comment> PostComment(PostedComment comment);

        public Task<PostedRating> RateUser(PostedRating postedRating);

        public Task<CommentsPaging> GetUserComments(string UserName, int page, int quantityPerPage);

        public Task<CommentsPaging> GetRealEstateComments(string RealEstateId, int page, int quantityPerPage);

        public Task<Propertys> PostANewRealEstate(Propertys newRealEstate);
        public Task<bool> UserLoggedInAndValid();
    }
}
