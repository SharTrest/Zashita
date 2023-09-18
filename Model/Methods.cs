using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;



namespace Diplom.Client.Model
{
    public static class Methods
    {
        public static bool IsValidUser(UserList db, UserData user)
        {
            var u = db.Users.Find(u => u.UserName == user.UserName);
            if (u == null)
            {
                user.Authed = false;
                return user.Authed;
            }
            if (user.Password != u.Password || u.Status == "B")
            {
                user.Status = u.Status;
                    return false;
                }
            user.Status = u.Status;
            user.Authed = true;
            user.Pass = u.RulledPass;
            return user.Authed;
        }
        public static bool ChangePassword(UserList db, UserData user)
        {
            var u = db.Users.FirstOrDefault(u=> u.UserName == user.UserName);
            u.Password = user.Password;
            
            return true;
        }
        public static string BanUserByName(UserList db, string name)
        {
            var u = db.Users.FirstOrDefault(u=>u.UserName == name);
            if (u == null || u.Status == "A")
            {
                return "";
            }
            if (u.Status == "B") 
            { 
                u.Status = "U";
            }
            else
                u.Status = "B";
            return u.Status;
        }
        public static ObservableCollection<User> ShowUsers(UserList db)
        {
            var u = db.Users.ToObservableCollection();
            return u; 
        }
        public static bool ChangePasswordRules(UserList db)
        {
            var u = ShowUsers(db);
            bool pr = db.Users[0].RulledPass;
            foreach (var us in db.Users)
            {
                us.RulledPass = !pr;
            }
            foreach (var user in u) 
            {
                user.RulledPass = !pr;
            }
            return true;
        }
        public static bool ChangePassRiulesByName(UserList db, string userName)
        {
            var u = db.Users.FirstOrDefault(u => u.UserName == userName);
            if (u == null) return false;
            u.RulledPass = !u.RulledPass;
            
            return true;
        }
        public static void AddNewUSer(UserList db, string userName)
        {
            db.Users.Add(new User { UserName = userName, Password = "", Status = "U", RulledPass = false});
            string newStr = userName + "," + "," + "U" + "," + "False";
            using (StreamWriter fs = File.CreateText(db.FileName))
            {
                fs.WriteLine(newStr);
            }
        }

    }
}
