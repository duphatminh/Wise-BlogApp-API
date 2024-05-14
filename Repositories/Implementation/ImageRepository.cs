using Microsoft.EntityFrameworkCore;
using WiseBlogApp.API.Data;
using WiseBlogApp.API.Models.Domain;
using WiseBlogApp.API.Repositories.Interface;

namespace WiseBlogApp.API.Repositories.Implementation;

public class ImageRepository : IImageRepository
{
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ApplicationDbContext dbContext;
    
    public ImageRepository(IWebHostEnvironment webHostEnvironment,
                           IHttpContextAccessor httpContextAccessor,
                           ApplicationDbContext dbContext)
    {
        this.webHostEnvironment = webHostEnvironment;
        this.httpContextAccessor = httpContextAccessor;
        this.dbContext = dbContext;
    }
    
    public async Task<BlogImage> Upload(IFormFile file, BlogImage blogImage)
    {
        // - Upload th Image to API/Images
        var localPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
            $"{blogImage.FileName}{blogImage.FileExtension}");

        using var stream = new FileStream(localPath, FileMode.Create);
        await file.CopyToAsync(stream);
        
        // - Update the database 
        var httpRequest = httpContextAccessor.HttpContext.Request;
        var urlPath = $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{blogImage.FileName}{blogImage.FileExtension}";
        
        blogImage.Url = urlPath;
        
        await dbContext.BlogImages.AddAsync(blogImage);
        await dbContext.SaveChangesAsync();
        
        return blogImage;
    }

    public async Task<IEnumerable<BlogImage>> GetAll()
    {
        return await dbContext.BlogImages.ToListAsync();
    }
}