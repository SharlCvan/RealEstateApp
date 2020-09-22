using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class RealEstateService : IRealEstateService
    {
        private readonly HttpClient http;

        public RealEstateService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<Propertys> GetRealEstate(int id)
        {
            return await http.GetJsonAsync<Propertys>($"RealEstates/{id}");
        }

        public async Task<IEnumerable<Propertys>> GetRealEstates()
        {
            return await http.GetJsonAsync<Propertys[]>("RealEstates");
        }


    }
}
