using System.Threading.Tasks;

namespace DigitalHouseSystemApi.Services
{

    public interface ILemonSqueezyService
    {
        public Task<string> CreateCheckoutAsync(string email, decimal amount, string username);
    }

}
