using System.Collections.Generic;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class Magenta1Service : IMagenta1Service
    {
        private readonly IMagenta1Repository _magenta1Repository;

        public Magenta1Service(IMagenta1Repository magenta1Repository)
        {
            _magenta1Repository = magenta1Repository;
        }

        public async Task<IEnumerable<Magenta1Dto>> GetAllMagenta1()
        {
            IEnumerable<Magenta1> magenta1s = await _magenta1Repository.GetAllAsync();
            ICollection<Magenta1Dto> magenta1_dtos = new List<Magenta1Dto>();
           
            foreach (Magenta1 m in magenta1s)
            {
                int id = m.Id;
                string username = m.AppUser.UserName;
                decimal budget = m.Budget;
                string internet_package = m.InternetPackage.Name;
                
                ICollection<string> magenta1tariffs = new List<string>();
                foreach(Magenta1Tariff mt in m.Magenta1Tariffs)
                {
                    magenta1tariffs.Add(mt.Tariff.Name);
                }

                magenta1_dtos.Add(new Magenta1Dto(id, username, budget, internet_package,magenta1tariffs));

            }
            return magenta1_dtos;
        }
    }
}
