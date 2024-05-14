using WiseBlogApp.API.Models.Domain;

namespace WiseBlogApp.API.Repositories.Interface;

public interface ICategoryRepositories
{
    Task<Category> CreateAsync(Category category); 
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetById(Guid id);
    Task<Category?> UpdateAsync(Category category);
    Task<Category?> DeleteAsync(Guid id);
}