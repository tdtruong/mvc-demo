using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CategoryModel
    {
        private OnlineShopDbContext db = null;

        public CategoryModel()
        {
            db = new OnlineShopDbContext();
        }

        public List<Category> ListAll()
        {
            var rs = db.Database.SqlQuery<Category>("Sp_Category_ListAll").ToList();
            return rs;
        }
    }
}
