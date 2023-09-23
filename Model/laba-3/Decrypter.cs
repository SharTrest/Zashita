using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Client.Model.laba_3
{
    public class Decrypter
    {
        public string Password { get; set; }
        public string FileName { get; set; }

        public Decrypter()
        {
            FileName = "DeDataBase.txt";
        }
    }
}
