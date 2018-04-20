using Models.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AccountModel
    {
        private OnlineShopDbContext db = null;
        public AccountModel()
        {
            db = new OnlineShopDbContext();
        }
        public bool Login(string userName, string passWord)
        {
            object[] sParams = 
            {
                new SqlParameter("@UserName", userName),
                new SqlParameter("@Password", passWord)
            };
            var res = db.Database.SqlQuery<bool>("Sp_Account_Login @UserName, @Password", sParams).SingleOrDefault();
            return res;
        }
    }
}
