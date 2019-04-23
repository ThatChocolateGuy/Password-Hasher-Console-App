﻿using System;

namespace PasswordHasherConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter password to hash: ");
            //create hashed pw from user input
            string hashed_pw = SecurePasswordHasherHelper.Hash(Console.ReadLine());

            //extract iteration and Base64 string
            var splittedHashString = hashed_pw.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            //get hashbytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            //get salt
            var salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            //get hash
            var hash = new byte[20];
            Array.Copy(hashBytes, salt.Length, hash, 0, 20);

            Console.WriteLine("\nHashed PW: " + hashed_pw);
            Console.WriteLine("Salt: " + Convert.ToBase64String(salt));
            Console.WriteLine("Hash: " + Convert.ToBase64String(hash));
            Console.WriteLine("Salted Hash: " + Convert.ToBase64String(hashBytes));
            Console.WriteLine("\nConfirm PW: ");

            //verify user input matches decrypted hash
            if (SecurePasswordHasherHelper.Verify(Console.ReadLine(), hashed_pw))
            {
                Console.WriteLine("\nPassword Verified");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nPasswords do not match");
                Console.ReadKey();
            }
        }
    }
}
