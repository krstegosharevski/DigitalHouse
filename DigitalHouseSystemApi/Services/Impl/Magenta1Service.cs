using System.Collections.Generic;
using DigitalHouseSystemApi.DTOs;
using DigitalHouseSystemApi.Interfaces;
using DigitalHouseSystemApi.Models;
using DigitalHouseSystemApi.Models.Exceptions;

namespace DigitalHouseSystemApi.Services.Impl
{
    public class Magenta1Service : IMagenta1Service
    {
        private readonly IMagenta1Repository _magenta1Repository;
        private readonly IInternetPackageRepository _internetPackageRepository;
        private readonly ITariffRepository _tariffRepository;

        public Magenta1Service(IMagenta1Repository magenta1Repository,
            IInternetPackageRepository internetPackageRepository,
            ITariffRepository tariffRepository)
        {
            _magenta1Repository = magenta1Repository;
            _internetPackageRepository = internetPackageRepository;
            _tariffRepository = tariffRepository;
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

        public async Task<Magenta1Dto> UpdateStatusOnMagenta1(int id)
        {
            if (id < 0)
            {
                throw new Exception("The id can not be null!");
            }
            
            Magenta1 m = await _magenta1Repository.FindById(id);
            if (m == null)
            {
                throw new Magenta1NotFoundException(id);
            }

            if (await _magenta1Repository.ApproveMagenta1User(m))
            {
                int ids = m.Id;
                string username = m.AppUser.UserName;
                decimal budget = m.Budget;
                string internet_package = m.InternetPackage.Name;

                ICollection<string> magenta1tariffs = new List<string>();
                foreach (Magenta1Tariff mt in m.Magenta1Tariffs)
                {
                    magenta1tariffs.Add(mt.Tariff.Name);
                }
                return new Magenta1Dto(ids, username, budget, internet_package, magenta1tariffs);
            }

            throw new CantApproveNewMagenta1(m.AppUser.UserName);
        }

        public async Task<Magenta1Dto> CreateMagenta1(CreateMagenta1Dto createMagenta1)
        {
            InternetPackage? ip = await _internetPackageRepository.FindByIdAsync(createMagenta1.InternetPackageId);
            if (ip == null)
            {
                throw new Exception("ERROR");
            }

            Magenta1 magenta1 = new Magenta1(createMagenta1.UserId, createMagenta1.Budget, ip);

            foreach (int tariffId in createMagenta1.Magenta1TariffsId)
            {
                Tariff? tariff = await _tariffRepository.FindByIdAsync(tariffId);
                if (tariff == null)
                {
                    throw new Exception($"Tariff with ID {tariffId} not found");
                }

                magenta1.AddTariff(tariff); // Додај ја тарифата во Magenta1
            }

            await _magenta1Repository.AddAsync(magenta1);
            await _magenta1Repository.SaveChangesAsync();

            // Мапирај го ентитетот во DTO и врати го
            ICollection<string> magenta1tariffs = new List<string>();
            foreach (Magenta1Tariff mt in magenta1.Magenta1Tariffs)
            {
                magenta1tariffs.Add(mt.Tariff.Name);
            }
            return new Magenta1Dto(createMagenta1.UserId, "Username K", createMagenta1.Budget, "InternetPackage M", magenta1tariffs);
        }
    }
}
