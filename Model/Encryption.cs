using MathCore.Logging;
using MathCore.WPF.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Ink;
using System.IO;
using System.Configuration;

namespace Diplom.Client.Model
{
    public static class Encryption
    {
        public static readonly string FileName = "vect.txt";
        public static string StringToMD2Hash(string str, List<string> shfr)
        {
            int num = 0;
            int l = 0;
            //переменные для шифрования и расшифровки

            string itog = ""; //искомый результат - зашифрованный открытый текст
            string shifr = GetNewShifr(str);
            byte[] log = Encoding.Default.GetBytes(str); //здесь хранится ascii-код символов логина.
            int[] kod = new int[str.Length]; //здесь хранится зашифрованный логин
            
            for (int j = 0; j < str.Length; j++) //обратная связь по шифротексту
            {
                kod[j] = Convert.ToInt32(log[j]) ^ Convert.ToInt32(shifr); //результат шифрования
            }

            for (int i = 0; i < str.Length; i++)
                itog += kod[i].ToString() + " ";
            shfr.Add(shifr);
            return itog;
        }

        public static string GenerateNewVektor()
        {
            string vekt = "";
            int v = 0;        //две переменные для задания
            Random r = new Random();//рандомно задаем вектор инициализации и приводим его к 2-х значному виду
            v = r.Next(0, 99);
            vekt = v.ToString();
            if (vekt.Length != 2)
                for (int i = 0; i < 2; i++)
                    if (i > vekt.Length)
                        vekt += '0';
            return vekt;
        }

        public static string GetNewShifr(string str)
        {
            int temp = 0; //временная переменная
            string L = ""; //левый подблок
            string R = ""; //правый подблок
            int[] key = new int[16]; //ключ
            string shifr = "";
            var vekt = GenerateNewVektor();
            var X = GetXMassive(str);  

            for (int i = 0; i < vekt.Length; i++) //задаем левый и правый блоки
            {
                if (i < vekt.Length / 2)
                    L = vekt[i].ToString();
                else
                    R = vekt[i].ToString();
            }
            for (int i = 0; i < 16; i++) //задаем ключ
                key[i] = X[i];

            for (int i = 0; i < 16; i++) //функция блочного шифрования (сеть Фейстеля)
            {
                temp = Convert.ToInt32(R) ^ (Convert.ToInt32(L) ^ key[i]);
                R = L;
                L = temp.ToString();
            }
            shifr = L + R; //результат функции блочного шифрования


            return shifr;
        }

        private static int[] GetXMassive(string str)
        {
            int[] S = new int[256]; //рандомный массив на 256 символов
            int[] C = new int[16]; //контрольная сумма
            int[] X = new int[48]; //48-битовый буфер
            int Nblock = 0; //кол-во 16-значных блоков
            string password = str;
            int c = 0;
            int l = 0;
            int t = 0;

            Random rand = new Random();//рандомим S
            for (int i = 0; i < S.Length; i++)
                S[i] = rand.Next(255);
            for (int i = 0; i < C.Length; i++) //заполняем С
                C[i] = 0;
            for (int i = 0; i < Nblock; i++) //вычисляем контрольную сумму
            {
                for (int j = 0; j < C.Length; j++)
                {
                    c = Convert.ToInt32(password[i * 16 + j]);
                    int b;
                    b = c ^ l;
                    C[j] = C[j] ^ S[b];
                    l = C[j];
                }
            }
            for (int i = 0; i < C.Length; i++) //и прибавляем ее к паролю
                password += C[i];


            for (int i = 0; i < X.Length; i++) //обработка 48-битного буфера
                X[i] = 0;
            Nblock = password.Length / 16;
            for (int i = 0; i < Nblock; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    X[16 + j] = password[i * 16 + j];
                    X[32 + j] = Convert.ToInt32(X[16 + j]) ^ Convert.ToInt32(X[j]);
                }
                for (int j = 0; j < 18; j++)
                {
                    for (int a = 0; a < X.Length; a++)
                    {
                        X[a] = X[a] ^ S[t];
                        t = X[a];
                    }
                    t = (t + j) % 256;
                }
            }
            return X;
        }

        public static string DecryptMD2ToString(string hash, string shifr)
        {
            string stroka = "";
            string text = "";
            int kod1 = 0;
            var kod = hash.Split(" ");
            for (int j = kod.Length - 2; j >= 0; j--)
            {
                var num = Convert.ToInt32(kod[j]);
                kod1 = num ^ Convert.ToInt32(shifr);
                stroka += Convert.ToChar(kod1);
            }
            for (int i = stroka.Length - 1; i >= 0; i--)
                text += stroka[i];
            return text;
        }
    }
}
