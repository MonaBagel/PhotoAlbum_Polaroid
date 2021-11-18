using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Polaroid_Proj.Models.Gallery;
using Polaroid_Proj.Models.GalleryAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroid_Proj.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

/*        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            

        }*/

        public DbSet<GalleryItem> GalleryItems { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<GalleryAccessRole> GalleryAccessRoles { get; set; }
        public DbSet<GalleryUserAccessRole> GalleryUserAccessRoles { get; set; }

    }
}
