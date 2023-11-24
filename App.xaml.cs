using Diplom.Client.Model;
using Diplom.Client.View.Windows;
using Diplom.View.Windows;
using System.IO;
using System.Linq;
using System.Windows;
using System.Text;
using System;
using Diplom.Client.Database.Context;
using System.Windows.Documents;
using Diplom.Client.Database.Model;

namespace Diplom
{
    public partial class App : Application
    {
        private UserData user;
        private UserList db;
        private ZashitaInformationContext database;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            db = new UserList();
            database = new ZashitaInformationContext();
            var r = database.Users.ToList();
            db.Users = database.Users.ToList();

            user = new UserData();
            


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
            database.SaveChanges();
            base.OnExit(e);
        }
    }
}
