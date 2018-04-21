using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public int Insert(string name, string alias, int? parentId, DateTime? createdDate, int? order, bool? status)
        {
            object[] sParams = 
            {
                new SqlParameter("@Name", name),
                new SqlParameter("@Alias", alias),
                new SqlParameter("@ParentID", parentId),
                new SqlParameter("@CreatedDate", createdDate),
                new SqlParameter("@Order", order),
                new SqlParameter("@Status", status)
            };
            int rs = db.Database.ExecuteSqlCommand("Sp_Category_Create @Name, @Alias, @ParentID, @CreatedDate, @Order, @Status", sParams);
            return rs;
        }
    }
}
