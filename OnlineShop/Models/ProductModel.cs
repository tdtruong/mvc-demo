using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductModel
    {
        private OnlineShopDbContext db = null;
        public ProductModel()
        {
            db = new OnlineShopDbContext();
        }

        public List<Product> ListAll()
        {
            var rs = db.Database.SqlQuery<Product>("Sp_Product_ListAll").ToList();
            return rs;
        }
    }
}
