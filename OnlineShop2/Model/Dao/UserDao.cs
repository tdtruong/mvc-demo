using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

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

        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        public User GetById(int id)
        {
            return db.Users.SingleOrDefault(x => x.ID == id);
        }

        public int Login(string userName, string passWord)
        {
            var rs = db.Users.SingleOrDefault(x=>x.UserName == userName);
            if (rs == null)
            {
                return 0;
            }
            else
            {
                if (!rs.Status)
                {
                    return -2;
                }
                if (rs.Password == passWord)
                {
                    return 1;
                }
                else
                {
                    return -1;
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
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString)).OrderByDescending(x=>x.CreatedDate);
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
