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

        public bool DeleteBlogItem(BlogItemModel blogDelete)
        {
            _context.Update<BlogItemModel>(blogDelete);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            return _context.BlogInfo;
        }

        public IEnumerable<BlogItemModel> GetItemsByCategory(string Category)
        {
            return _context.BlogInfo.Where(item => item.Category == Category);
        }

        public IEnumerable<BlogItemModel> GetItemsByDate(string Date)
        {
            return _context.BlogInfo.Where(item => item.Date == Date);
        }

        public List<BlogItemModel> GetItemsByTag(string Tag)
        {
            List<BlogItemModel> AllBlogsWithTag = new List<BlogItemModel>(); //"Tag1, Tag2, Tag3, Tag4
            var allItems = GetAllBlogItems().ToList(); // {Tag: "Tag1", Tag: "Tag2", Tag: "Tag3"}
            for(int i = 0; i < allItems.Count; i++)
            {
                BlogItemModel Item = allItems[i];
                    var itemArray = Item.Tag.Split(","); //{"Tag1", "Tag2"}
                for(int x = 0; x < itemArray.Length; i++)
                {
                    if(itemArray[x].Contains(Tag))
                    {
                        AllBlogsWithTag.Add(Item);
                    }

                }
            }
            return AllBlogsWithTag;

        }

        public bool UpdateBlogItems(BlogItemModel blogUpdate)
        {
            _context.Update<BlogItemModel>(blogUpdate);
               return _context.SaveChanges() != 0;
        }

        public IEnumerable<BlogItemModel> GetPublishedItems()
        {
            return _context.BlogInfo.Where(item => item.IsPublished);
        }
    }
}