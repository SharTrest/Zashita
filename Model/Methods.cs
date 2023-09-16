using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zashita.DAL.Context;
using Zashita.DAL.Entity;

namespace Diplom.Client.Model
{
    public static class Methods
    {
        public static bool IsValidUser(ZashitaDB db, UserData user)
        {
            var u = db.Users.FirstOrDefault(u=> u.UserName == user.UserName);
            if (u == null || u.Status =="B")
            {
                user.Authed = false;
                return user.Authed;
            }
            user.Status = u.Status;
            user.Authed = true;
            return user.Authed;
        }
        public static bool ChangePassword(ZashitaDB db, UserData user)
        {
            var u = db.Users.FirstOrDefault(u=> u.UserName == user.UserName);
            u.Password = user.Password;
            db.Users.Update(u);
            db.SaveChanges();
            return true;
        }
        public static bool BanUserByName(ZashitaDB db, string name)
        {
            var u = db.Users.FirstOrDefault(u=>u.UserName == name);
            if (u == null)
            {
                return false;
            }
            u.Status = "B";
            db.Users.Update(u);
            db.SaveChanges();
            return true;
        }
        public static List<User> ShowUsers(ZashitaDB db)
        {
            var u = db.Users.ToList();
            return u; 
        }
        public static bool ChangePasswordRules(ZashitaDB db)
        {
            var u = ShowUsers(db);
            foreach (var user in u) 
            {
                user.RulledPass = true;
                db.Users.Update(user);
                db.SaveChanges();
            }
            return true;
        }
        public static bool ChangePassRiulesByName(ZashitaDB db, string userName)
        {
            var u = db.Users.FirstOrDefault(u => u.UserName == userName);
            if (u == null) return false;
            u.RulledPass = true;
            db.Users.Update(u);
            db.SaveChanges();
            return true;
        }
    }
}
