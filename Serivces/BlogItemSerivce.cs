using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapi.Models;
using blogapi.Serivces.Context;

namespace blogapi.Serivces
{
    public class BlogItemSerivce
    {
        private readonly DataContext _context;

        public BlogItemSerivce(DataContext context)
        {
            _context = context;
        }

        internal bool AddBlogItems(BlogItemModel newBlogItem)
        {
            bool result;

            _context.Add(newBlogItem);
            result = _context.SaveChanges() != 0;

            return result;
        }

        internal IEnumerable<BlogItemModel> GetAllBlogItems()
        {
            return _context.BlogInfo.ToList();
        }

        internal IEnumerable<BlogItemModel> GetBloItemCatorgy(string category)
        {
            return _context.BlogInfo
                .Where(item => item.Category == category)
                .ToList();
        }

        internal IEnumerable<BlogItemModel> GetItemsByDate(string date)
        {
            return _context.BlogInfo
                .Where(item => item.Date == date)
                .ToList();
        }

        internal List<BlogItemModel> GetItemsByTag(string tag)
        {
            List<BlogItemModel> allBlogWithTag = new List<BlogItemModel>();
            var allItems = GetAllBlogItems().ToList();

            for (int i = 0; i < allItems.Count; i++)
            {
                BlogItemModel item = allItems[i];
                var itemArr = item.Tags.Split(',');

                for (int j = 0; j < itemArr.Length; j++)
                {
                    if (itemArr[j].Contains(tag))
                    {
                        allBlogWithTag.Add(item);
                        break;
                    }
                }
            }

            return allBlogWithTag;
        }

        internal bool UpdateBlogItem(BlogItemModel blogUpdate)
        {
            _context.Update(blogUpdate);
            return _context.SaveChanges() != 0;
        }

        public IEnumerable<BlogItemModel> GetPublished()
        {
            return _context.BlogInfo
                .Where(item => item.IsPublished)
                .ToList();
        }

        public bool DeleteBlogItem(BlogItemModel blogItemModel)
        {
            bool result = false;

            if (blogItemModel != null)
            {
                _context.Remove(blogItemModel);
                result = _context.SaveChanges() != 0;
            }

            return result;
        }

        internal bool DeleteBlogItem(string blogToDelete)
        {
            throw new NotImplementedException();
        }
    }
}
