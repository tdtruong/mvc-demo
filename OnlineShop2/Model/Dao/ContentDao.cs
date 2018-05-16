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
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
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
    }
}
