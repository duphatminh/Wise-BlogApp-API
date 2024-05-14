using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WiseBlogApp.API.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var readerRoleId = "70584391-f77b-4bd9-b85c-0128b4873912";
        var writerRoleId = "9b06b669-b5d4-4126-be86-ac19b33ea696";
        
        // Create Reader and Writer Role 
        var roles = new List<IdentityRole>
        {
            new IdentityRole()
            {
                Id = readerRoleId,
                Name = "Reader",
                NormalizedName = "READER",
                ConcurrencyStamp = readerRoleId
            },
            new IdentityRole()
            {
                Id = writerRoleId,
                Name = "Writer",
                NormalizedName = "WRITER",
                ConcurrencyStamp = writerRoleId
            },
        };

        // Seed the role
        modelBuilder.Entity<IdentityRole>().HasData(roles);
        
        // Create the Admin User
        var adminUserId = "ec6cbf8a-4880-4a78-b57f-9fbd8ce6e890";
        var admin = new IdentityUser()
        {
            Id = adminUserId,
            UserName = "admin@wiserc.com",
            Email = "admin@wiserc.com",
            NormalizedUserName = "ADMIN@WISERC.COM",
            NormalizedEmail = "ADMIN@WISERC.COM",
        };
        
        admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
        
        modelBuilder.Entity<IdentityUser>().HasData(admin);
        
        // Give a Roles to Admin 
        var adminRoles = new List<IdentityUserRole<string>>()
        {
            new()
            {
                UserId = adminUserId,
                RoleId = readerRoleId
            },
            new()
            {
                UserId = adminUserId,
                RoleId = writerRoleId
            }
        };
        
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
    }
}