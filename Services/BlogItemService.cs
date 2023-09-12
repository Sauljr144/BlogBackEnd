using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogBackEnd.Models;
using BlogBackEnd.Services.Context;

namespace BlogBackEnd.Services
{
    public class BlogItemService
    {
        private readonly DataContext _context;

        public BlogItemService(DataContext context)
        {
            _context = context;
        }

        public bool AddBlogItem(BlogItemModel newBlogItem)
        {
            bool result = false;
            _context.Add(newBlogItem);

            result = _context.SaveChanges() != 0;
            return result;
        }

        internal bool DeleteBlogItem(BlogItemModel blogDelete)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<BlogItemModel> GetItemsByCategory(string category)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<BlogItemModel> GetItemsByDate(string date)
        {
            throw new NotImplementedException();
        }

        internal List<BlogItemModel> GetItemsByTag(string tag)
        {
            throw new NotImplementedException();
        }

        internal bool UpdateBlogItems(BlogItemModel blogUpdate)
        {
            throw new NotImplementedException();
        }
    }
}