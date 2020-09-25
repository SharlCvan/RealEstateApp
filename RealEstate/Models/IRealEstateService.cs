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

        public Task<Comment> PostComment(int realEstateId, string message);

        public Task<Propertys> PostANewRealEstate(Propertys newRealEstate);
        public Task<bool> UserLoggedInAndValid();
    }
}
