using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Serivces;
using Microsoft.AspNetCore.Mvc;

namespace blogapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogItemSerivce _data;

        public BlogController(BlogItemSerivce blogItemSerivce)
        {
            _data = blogItemSerivce;
        }

        [HttpPost("AddBlogItems")]
        public bool AllBlogitems(BlogItemModel newBlogItem)
        {
            return _data.AddBlogItems(newBlogItem);
        }

        [HttpGet("getBlogItem")]
        public IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            return _data.GetAllBlogItems();
        }

        [HttpGet("GetBlogItemByCaterogry/{category}")]
        public IEnumerable<BlogItemModel> GetItemsByCatrogy(string category)
        {
            return _data.GetBloItemCatorgy(category);
        }

        [HttpGet("GetItemsByTag/{tag}")]
        public List<BlogItemModel> GetItemsByidTag(string tag)
        {
            return _data.GetItemsByTag(tag);
        }

        [HttpGet("itemItemByDate/{date}")]
        public IEnumerable<BlogItemModel> GetItemsByDate(string date)
        {
            return _data.GetItemsByDate(date);
        }

        [HttpPut("UpdateBlogItems")]
        public bool UpdateBlogItems(BlogItemModel blogUpdate)
        {
            return _data.UpdateBlogItem(blogUpdate);
        }

        [HttpDelete("BlogItemDelete/{BlogToDelete}")]
        public bool DeleteBlogItem(string BlogToDelete)
        {
            return _data.DeleteBlogItem(BlogToDelete); 
        }

        [HttpGet("GetPublishedItems")]
        public IEnumerable<BlogItemModel> GetPublishedItems()
        {
            return _data.GetPublished();
        }
    }
}
