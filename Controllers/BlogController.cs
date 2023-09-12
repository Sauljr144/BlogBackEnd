using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogBackEnd.Models;
using BlogBackEnd.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogBackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        //Create a variable to hold our data
        private readonly BlogItemService _data;

        //constructor to be able to create instances
        public BlogController(BlogItemService dataFromService)
        {
            _data = dataFromService;
        }

        //AddBlogItems
         [HttpPost("AddBlogItems")]
        public bool AddBlogItems(BlogItemModel newBlogItem)
        {
            return _data.AddBlogItem(newBlogItem);
        }

        //GetAllBlogItems
        [HttpGet("GetBlogItems")]
        public IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            return _data.GetAllBlogItems();
        }

        //GetBlogItemsByCategory
        [HttpGet("GetItemsByCategory/{Category}")]
         public IEnumerable<BlogItemModel> GetItemsByCategory(string Category)
        {
            return _data.GetItemsByCategory(Category);
        }

        //GetBlogItemsByTags
        [HttpGet("GetItemsByTag/{Tag}")]
         public List<BlogItemModel> GetItemsByTag(string Tag)
        {
            return _data.GetItemsByTag(Tag);
        }

        //GetBlogItemsByDate
        [HttpGet("GetItemsByDate/{Date}")]
         public IEnumerable<BlogItemModel> GetItemsByDate(string Date)
        {
            return _data.GetItemsByDate(Date);
        }

        //UpdateBlogItems
        [HttpPost("UpdateBlogItem")]
        public bool UpdateBlogItems(BlogItemModel BlogUpdate)
        {
            return _data.UpdateBlogItems(BlogUpdate);
        }
        
        //DeleteBlogItems
        [HttpPost("DeleteBlogItem/{BlogItemToDelete}")]

        public bool DeleteBlogItem(BlogItemModel BlogDelete)
        {
            return _data.DeleteBlogItem(BlogDelete);
        }

    }
}