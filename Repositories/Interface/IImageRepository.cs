using WiseBlogApp.API.Models.Domain;

namespace WiseBlogApp.API.Repositories.Interface;

public interface IImageRepository
{
    Task<BlogImage> Upload(IFormFile file, BlogImage blogImage);
    Task<IEnumerable<BlogImage>> GetAll();
}