using Common;
using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ContentDao
    {
        private OnlineShopDbContext db = null;
        public ContentDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Content> ListAll()
        {
            return db.Contents.ToList();
        }

        public IEnumerable<Content> Paging(string searchString, int pageNumber, int pageSize)
        {
            IOrderedQueryable<Content> model = db.Contents.OrderByDescending(x => x.CreatedDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString)).OrderByDescending(x => x.CreatedDate);
            }
            return model.ToPagedList(pageNumber, pageSize);
        }

        public IEnumerable<Content> Paging(int pageNumber, int pageSize)
        {
            IOrderedQueryable<Content> model = db.Contents.OrderByDescending(x => x.CreatedDate);
            return model.ToPagedList(pageNumber, pageSize);
        }

        /// <summary>
        /// Get all tag of a content
        /// </summary>
        /// <param name="contentId">the Content ID</param>
        /// <returns>List of tag</returns>
        public List<Tag> ListAllTag(long contentId)
        {
            var model = (from a in db.Tags
                         join b in db.ContentTags
                         on a.ID equals b.TagID
                         where b.ContentID == contentId
                         select new
                         {
                             ID = b.TagID,
                             Name = a.Name
                         }).AsEnumerable().Select(x => new Tag() {
                             ID = x.ID,
                             Name = x.Name
                         });
            return model.ToList();
        }

        /// <summary>
        /// Get all content related to the tag
        /// </summary>
        /// <param name="tag">the tag name</param>
        /// <param name="pageNumber">current page number</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public IEnumerable<Content> GetAllContentByTag(string tag, int page, int pageSize)
        {
            IOrderedEnumerable<Content> model = (from a in db.Contents
                        join b in db.ContentTags
                        on a.ID equals b.ContentID
                        where b.TagID == tag
                        select new
                        {
                            ID = a.ID,
                            Name = a.Name,
                            MetaTitle = a.MetaTitle,
                            Image = a.Image,
                            Description = a.Description,
                            Detail = a.Detail,
                            CreatedDate = a.CreatedDate,
                            CreatedBy = a.CreatedBy
                        }).AsEnumerable().Select(x=> new Content() {
                            ID = x.ID,
                            Name = x.Name,
                            MetaTitle = x.MetaTitle,
                            Image = x.Image,
                            Description = x.Description,
                            Detail = x.Detail,
                            CreatedDate = x.CreatedDate,
                            CreatedBy = x.CreatedBy
                        }).OrderByDescending(x=>x.CreatedDate);
            return model.ToPagedList(page, pageSize);
        }

        /// <summary>
        /// Get Tag by id
        /// </summary>
        /// <param name="tagId">the Tag id</param>
        /// <returns>Tag entity</returns>
        public Tag GetTagById(string tagId)
        {
            return db.Tags.Find(tagId);
        }

        public Content GetById(long id)
        {
            return db.Contents.Find(id);
        }

        public long Insert(Content entity)
        {
            // if MetaTitle is empty, add it using the content name
            if (string.IsNullOrEmpty(entity.MetaTitle))
            {
                entity.MetaTitle = StringHelper.ToUnsignString(entity.Name);
            }
            db.Contents.Add(entity);
            db.SaveChanges();

            // Insert tag to tag table and content tag table
            InsertTagAndContentTag(entity);
            return entity.ID;
        }

        // Check a tag is exist in the tag table or not
        private bool IsExistTag(string tagId)
        {
            return db.Tags.Count(x => x.ID == tagId) > 0;
        }

        // Check a content tag is exist in the content tag table or not
        private bool IsExistContentTag(long contentId, string tagId)
        {
            return db.ContentTags.Count(x => x.ContentID == contentId && x.TagID == tagId) > 0;
        }

        // Insert new tag to tag table
        private string InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.ID = id;
            tag.Name = name;
            db.Tags.Add(tag);
            db.SaveChanges();
            return tag.ID;
        }

        private void InsertTagAndContentTag(Content content)
        {
            if (!string.IsNullOrEmpty(content.Tags))
            {
                string[] tags = content.Tags.Split(',').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                foreach (var tag in tags)
                {
                    var tagId = StringHelper.ToUnsignString(tag.Trim());
                    if (!IsExistTag(tagId))
                    {
                        InsertTag(tagId, tag.Trim());
                    }

                    // Insert or update content tag
                    if (!IsExistContentTag(content.ID, tagId))
                    {
                        InsertContentTag(content.ID, tagId);
                    }
                    else
                    {
                        var contentTag = new ContentTag();
                        contentTag.ContentID = content.ID;
                        contentTag.TagID = tagId;
                        UpdateContentTag(contentTag);
                    }
                }
            }
        }

        // Insert new content tag
        private long InsertContentTag(long contentId, string tagId)
        {
            var contentTag = new ContentTag();
            contentTag.ContentID = contentId;
            contentTag.TagID = tagId;
            db.ContentTags.Add(contentTag);
            db.SaveChanges();
            return contentTag.ContentID;
        }

        // Update content tag
        private bool UpdateContentTag(ContentTag entity)
        {
            try
            {
                var contentTag = db.ContentTags.Find(entity);
                contentTag.ContentID = entity.ContentID;
                contentTag.TagID = entity.TagID;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Content entity)
        {
            try
            {
                var content = GetById(entity.ID);
                content.Name = entity.Name;
                content.Description = entity.Description;
                content.Detail = entity.Detail;
                content.Image = entity.Image;
                content.CategoryID = entity.CategoryID;
                content.Status = entity.Status;
                content.Tags = entity.Tags;
                // Insert tag to tag table and content tag table
                InsertTagAndContentTag(entity);

                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var user = GetById(id);
                db.Contents.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var content = GetById(id);
            content.Status = !content.Status;
            db.SaveChanges();
            return content.Status;
        }
    }
}
