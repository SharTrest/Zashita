using Diplom.Client.Model;
using Diplom.Client.View.Windows;
using Diplom.View.Windows;
using System.Linq;
using System.Windows;
using Zashita.DAL.Context;

namespace Diplom
{
    public partial class App : Application
    {
        private UserData user;
        private ZashitaDB db;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            user = new UserData();
            db = new ZashitaDB();
            if (!db.Users.Any())
            {
                db.Users.AddAsync(new Zashita.DAL.Entity.User { UserName = "ADMIN", Password = "", Status = "A", RulledPass = false });
                db.SaveChangesAsync();
            }
            var loginView = new LoginWindow(user, db);
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                if (user.Authed == true && loginView.IsLoaded && user.Status == "U")
                {
                    var mainView = new UserMainWindow(user, db);
                    mainView.Show();
                    loginView.Close();
                }
                if (user.Authed == true && loginView.IsLoaded && user.Status == "A")
                {
                    var mainView = new AdminMainWindow(user, db);
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
