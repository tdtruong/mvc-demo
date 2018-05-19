using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class ProductDao
    {
        private OnlineShopDbContext db = null;

        public ProductDao()
        {
            db = new OnlineShopDbContext();
        }

        /// <summary>
        /// Get all new products
        /// </summary>
        /// <param name="top">number of product to get</param>
        /// <returns>List of new product</returns>
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// Get all feature product
        /// </summary>
        /// <param name="top">number of product to get</param>
        /// <returns>List of feature product</returns>
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.Status == true && x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public Product Detail(long id)
        {
            return db.Products.Find(id);
        }
    }
}
