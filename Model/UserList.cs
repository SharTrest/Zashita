using Diplom.Client.Database.Model;
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
        public string Creator { get; set; }

        public UserList()
        {
            Creator = "Снетков Никита. 19. Отсутствие повторяющихся символов.";
            FileName = "Database.txt";
            _users = new List<User>();
        }
    }
 }
