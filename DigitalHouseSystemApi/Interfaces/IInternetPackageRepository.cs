﻿using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IInternetPackageRepository
    {
        Task<IEnumerable<InternetPackage>> SelectAllInternetPackagesAsync();
        Task<InternetPackage?> FindByIdAsync(int id);
    }
}
