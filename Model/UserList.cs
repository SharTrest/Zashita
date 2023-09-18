using MathCore.WPF.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Client.Model
{
    public class UserList
    {
        private List<User> _users;
        public List<User> Users 
        { 
            get => _users; 
            set
            {
                _users = value;
            }
        }
        public string FileName { get; set; }

        public UserList()
        {
            FileName = "Database.txt";
            _users = new List<User>();
            if (!File.Exists(this.FileName))
                using (FileStream fs = File.Create(this.FileName))
                {
                    byte[] buffer = new UTF8Encoding(true).GetBytes("ADMIN,,A,False\n");
                    fs.Write(buffer, 0, buffer.Length);
                }
            string[] lines = File.ReadAllLines(FileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                Users.Add(new User { UserName = parts[0], Password = parts[1], Status = parts[2], RulledPass = Convert.ToBoolean(parts[3])});
            }
        }
    }
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public bool RulledPass { get; set; }
    }

}
