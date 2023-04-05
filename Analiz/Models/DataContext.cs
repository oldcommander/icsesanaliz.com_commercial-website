using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Kullanici> kullanicis { get; set; }
        public DbSet<Admin> admins { get; set; }
        public DbSet<Analiz> analizs { get; set; }
        public DbSet<Contact> contacts { get; set; }
        public DbSet<MainBlog> mainblogs { get; set; }
    }
}
