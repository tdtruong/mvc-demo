using Common;
using Model.EF;
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
            if(!string.IsNullOrEmpty(entity.Tags))
            {
                string[] tags = entity.Tags.Split(',');
                foreach(var tag in tags)
                {
                    if (string.IsNullOrEmpty(tag))
                    {
                        continue;
                    }
                    var tagId = StringHelper.ToUnsignString(tag.Trim());
                    if (!IsExistTag(tagId))
                    {
                        InsertTag(tagId, tag);
                    }

                    InsertContentTag(entity.ID, tagId);
                }
            }
            return entity.ID;
        }

        // Check a tag is exist in the tag table or not
        private bool IsExistTag(string tagId)
        {
            return db.Tags.Count(x => x.ID == tagId) > 0;
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
