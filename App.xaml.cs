using Diplom.Client.Model;
using Diplom.Client.View.Windows;
using Diplom.View.Windows;
using System.IO;
using System.Linq;
using System.Windows;
using System.Text;
using System;

namespace Diplom
{
    public partial class App : Application
    {
        private UserData user;
        private UserList db;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            user = new UserData();
            db = new UserList();


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
            File.WriteAllText(db.FileName,"");
            using (StreamWriter fs = new StreamWriter(db.FileName))
            {
                foreach (var user in db.Users)
                {
                    var str = user.UserName + "," + user.Password + "," + user.Status + "," + user.RulledPass.ToString() + "\n";
                    fs.Write(str);
                }
            }
            base.OnExit(e);
        }
    }
}
