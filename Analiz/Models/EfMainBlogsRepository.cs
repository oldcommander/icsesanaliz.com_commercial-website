using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public class EfMainBlogsRepository : IMainBlogsRepository
    {
        DataContext context;
        public EfMainBlogsRepository(DataContext _context)
        {
            context = _context;
        }
        public IEnumerable<MainBlog> MainBlogs => context.mainblogs;
        IQueryable<MainBlog> IMainBlogsRepository.MainBlogs => context.mainblogs;

        public void CreateMainBlog(MainBlog newMainBlog)
        {
            context.mainblogs.Add(newMainBlog);
            context.SaveChanges();
        }

        public void DeleteMainBlog(int MainBlogId)
        {
            var entity = context.mainblogs.Find(MainBlogId);
            context.mainblogs.Remove(entity);
            context.SaveChanges();
        }

        public MainBlog GetById(int MainBlogId)
        {
            return context.mainblogs.Find(MainBlogId);
        }

        public IEnumerable<MainBlog> GetMainBlog()
        {
            return context.mainblogs.ToList();
        }

        public void UpdateMainBlog(MainBlog updatedMainBlog)
        {
            context.mainblogs.Update(updatedMainBlog);
            context.SaveChanges();
        }
    }
}
