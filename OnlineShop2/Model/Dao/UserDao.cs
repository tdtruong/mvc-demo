using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Common;

namespace Model.Dao
{
    public class UserDao
    {
        private OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }

        /// <summary>
        /// Insert new user to database
        /// </summary>
        /// <param name="entity">The user to insert</param>
        /// <returns>user id</returns>
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }
        }

        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User GetById(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }

        public List<string> GetUserCredential(string userName)
        {
            var user = GetByUserName(userName);
            var model = (from a in db.Credentials
                         join b in db.UserGroups
                         on a.UserGroupID equals b.ID
                         join c in db.Roles
                         on a.RoleID equals c.ID
                         where b.ID == user.GroupID
                         select new
                         {
                             RoleID = a.RoleID,
                             UserGroupID = a.UserGroupID
                         }).AsEnumerable().Select(x => new Credential() {
                             RoleID = x.RoleID,
                             UserGroupID = x.UserGroupID
                         });
            return model.Select(x => x.RoleID).ToList();
        }

        public int Login(string userName, string passWord, bool isAdmin = false)
        {
            var rs = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (rs == null)
            {
                return 0;
            }
            else
            {
                if (!isAdmin)
                {
                    if (!rs.Status)
                    {
                        return -2;
                    }
                    else if (rs.Password == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                // is Admin
                if (rs.GroupID == Constants.ADMIN_GROUP || rs.GroupID == Constants.MOD_GROUP)
                {
                    if (!rs.Status)
                    {
                        return -2;
                    }
                    else if (rs.Password == passWord)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -3;
                }
            }
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IEnumerable<User> Paging(string searchString, int pageNumber, int pageSize)
        {
            IOrderedQueryable<User> model = db.Users.OrderByDescending(x => x.CreatedDate);
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString)).OrderByDescending(x => x.CreatedDate);
            }
            return model.ToPagedList(pageNumber, pageSize);
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.UserName = entity.UserName;
                user.Name = entity.Name;
                user.GroupID = entity.GroupID;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var user = GetById(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0;
        }

        public bool CheckUserEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0;
        }
    }
}
