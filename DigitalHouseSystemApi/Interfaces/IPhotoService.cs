using CloudinaryDotNet.Actions;

namespace DigitalHouseSystemApi.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(int productId);
    }
}
