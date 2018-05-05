using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class UserDao
    {
        private OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }

        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Login(string userName, string passWord)
        {
            var rs = db.Users.Count(x=>x.UserName==userName && x.Password == passWord);
            if (rs > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
