using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        private OnlineShopDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Menu> GetByGroupId(int menuTypeId)
        {
            return db.Menus.Where(x => x.TypeID == menuTypeId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
