using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Analiz.Web.Models
{
    public interface IMainBlogsRepository
    {
        IQueryable<MainBlog> MainBlogs { get; }
        MainBlog GetById(int MainBlogId);
        IEnumerable<MainBlog> GetMainBlog();
        void CreateMainBlog(MainBlog newMainBlog);
        void UpdateMainBlog(MainBlog updatedMainBlog);
        void DeleteMainBlog(int MainBlogId);
    }
}
