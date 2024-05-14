using Microsoft.EntityFrameworkCore;
using WiseBlogApp.API.Data;
using WiseBlogApp.API.Models.Domain;
using WiseBlogApp.API.Repositories.Interface;

namespace WiseBlogApp.API.Repositories.Implementation;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly ApplicationDbContext dbContext;
    
    public BlogPostRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
       await dbContext.BlogPosts.AddAsync(blogPost);
       await dbContext.SaveChangesAsync();
       return blogPost;
    }
    
    public async Task<BlogPost?> DeleteAsync(Guid id)
    {
        var exestingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        if (exestingBlogPost != null)
        {
            dbContext.BlogPosts.Remove(exestingBlogPost);
            await dbContext.SaveChangesAsync();
            return exestingBlogPost;
        }
        
        return null;
    }
    
    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await dbContext.BlogPosts.Include(x => x.Categories).ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(Guid id)
    {
        return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
    {
        return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
    }

    public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
    {
        var existingBlogPost = await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

        if (existingBlogPost == null)
        {
            return null;
        }
        // Update BlogPost 
        dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);
        
        // Update Categories
        existingBlogPost.Categories = blogPost.Categories;

        await dbContext.SaveChangesAsync();

        return blogPost;
    }

}