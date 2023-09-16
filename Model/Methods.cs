using System.Linq;
using System.Threading.Tasks;
using Zashita.DAL.Context;

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
    }
}
