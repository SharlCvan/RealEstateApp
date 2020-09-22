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

    }
}
