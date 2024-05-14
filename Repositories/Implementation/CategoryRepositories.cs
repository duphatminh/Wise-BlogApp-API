using Microsoft.EntityFrameworkCore;
using WiseBlogApp.API.Data;
using WiseBlogApp.API.Models.Domain;
using WiseBlogApp.API.Repositories.Interface;

namespace WiseBlogApp.API.Repositories.Implementation;

public class CategoryRepositories : ICategoryRepositories
{
    private readonly ApplicationDbContext dbContext;

    public CategoryRepositories(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
        
        return category;
    }
    
    public async Task<Category> DeleteAsync(Guid id)
    {
        var existingCategory =await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (existingCategory is null)
        {
            return null;
        }
        dbContext.Categories.Remove(existingCategory);
        await dbContext.SaveChangesAsync();
        return existingCategory;
    }
    
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await this.dbContext.Categories.ToListAsync();
    }

    public async Task<Category> GetById(Guid id)
    {
       return await this.dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Category> UpdateAsync(Category category)
    {
        var existingCategory  = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);
        
        if (existingCategory != null)    
        {
            dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        return null;
    }
    
    
}