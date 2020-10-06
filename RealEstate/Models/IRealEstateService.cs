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
        public Task<Propertys> GetRealEstate(int id);
        public Task<User> GetUser(string UserName);

        public Task<Comment> PostComment(PostedComment comment);

        public Task<Propertys> PostANewRealEstate(Propertys newRealEstate);
        public Task<bool> UserLoggedInAndValid();
    }
}
