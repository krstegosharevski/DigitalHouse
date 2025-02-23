using DigitalHouseSystemApi.Helpers;
using DigitalHouseSystemApi.Models;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IProblemRepository
    {
        Task<PagedList<Problem>> FindAllAsync(ProblemParams problemParams);
        Task<Problem> FindByIdAsync(int id);
        void Save(Problem problem);
        Task<bool> SaveChanges();
        void DeleteById(Problem problem);
    }
}
