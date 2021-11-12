using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Authentication
{
    class Program
    {
        public static List<User> LstUser = new List<User>();
        static bool userFound = false;
        static void Main(string[] args)
        {
           awal: 
            try
            {
                string PilihMenu;
                Console.Clear();
                Console.WriteLine("==BASIC AUTHENTICATION==");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. Show User");
                Console.WriteLine("3. Delete User");
                Console.WriteLine("4. Search");
                Console.WriteLine("5. Update User");
                Console.WriteLine("6. Login");
                Console.WriteLine("7. Exit");
                Console.Write("Input  : ");
                PilihMenu = RegexNumber(getInput());
                switch (PilihMenu)
                {
                    case "1":
                        Console.Clear();
                        CreateUser();
                        goto awal;
                        break;
                    case "2":
                        Console.Clear();
                        ShowUser();
                        goto awal;
                        break;
                    case "3":
                        Console.Clear();
                        DeleteUser();
                        goto awal;
                        break;
                    case "4":
                        Console.Clear();
                        SearchUser();
                        goto awal;
                        break;
                    case "5":
                        Console.Clear();
                        UpdateUser();
                        goto awal;
                        break;
                    case "6":
                        Console.Clear();
                        LoginUser();
                        goto awal;
                        break;
                    case "7":
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("======== Terima kasih =========");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Pilihan tidak ditemukan, Coba lagi !");
                        Console.ReadKey();
                        goto awal;
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Pilihan tidak sesuai, Coba lagi !");
                Console.ReadKey();
                goto awal;

            }
        }

        public static string getInput()
        {
            String inputan = "";
            inputan = Console.ReadLine();
            return inputan;
        }

        public static void CreateUser()
        {
            string FirstNameInput, LastNameInput, PasswordInput;
        ulangiFirst:
            Console.WriteLine("==CREATE USER==");
            Console.Write("Firstname : ");
            FirstNameInput = getInput();
            bool FirstnameValidate = NameValidation(FirstNameInput);
            //Console.WriteLine("hasil F :" + FirstnameValidate);
            if (!FirstnameValidate)
            {
                Console.WriteLine("Firstname harus memiliki minimal 2 huruf");
                Console.ReadKey();
                Console.Clear();
                goto ulangiFirst;
            }
        ulangiLast:
            Console.Write("Lastname : ");
            LastNameInput = getInput();
            bool LastNameValidate = NameValidation(LastNameInput);
            //Console.WriteLine("hasil L :" + LastNameValidate);
            if (!LastNameValidate)
            {
                Console.WriteLine("Lastname harus memiliki minimal 2 huruf");
                Console.ReadKey();
                Console.Clear();
                goto ulangiLast;
            }

        ulangiPass:
            Console.Write("Password : ");
            PasswordInput = getInput();
            bool passvalidate = PasswordValidation(PasswordInput);
            //Console.WriteLine("hasil P :" + FirstnameValidate);
            if (!passvalidate)
            {
                Console.WriteLine("Password harus mengandung angka, huruf besar,dan minimal 8 karakter");
                Console.ReadKey();
                Console.Clear();
                goto ulangiPass;
            }

            int RandName;
            string NameOne, NameTwo, UsernameFull;

            NameOne = FirstNameInput.Substring(0, 2);
            NameTwo = LastNameInput.Substring(0, 2);
            UsernameFull = NameOne + NameTwo;

            Random rnd = new Random();
            RandName = rnd.Next(00, 99);

            foreach (var obj in LstUser)
            {
                string xObjFirstName = obj.FirstnameAt;
                string xObjLastName = obj.LastnameAt;
                if (FirstNameInput == xObjFirstName && LastNameInput == xObjLastName)
                {
                    UsernameFull = UsernameFull + RandName;
                }
            }

            User myuser = new User(FirstNameInput, LastNameInput, UsernameFull, HashPassword(PasswordInput));
            LstUser.Add(myuser);
            Console.WriteLine("User Berhasil dibuat");
            Console.ReadKey();



        }
        public static void ShowUser()
        {
            Console.WriteLine("== SHOW USER ==");
            for (int i = 0; i < LstUser.Count; i++)
            {
                Console.WriteLine("============================");
                Console.WriteLine("NAMA : " + LstUser[i].FirstnameAt + " " + LstUser[i].LastnameAt);
                Console.WriteLine("USERNAME : " + LstUser[i].UserNameAt);
                Console.WriteLine("PASSWORD : " + LstUser[i].PasswordAt);
                Console.WriteLine("============================");
            }
            Console.ReadKey();
        }
        public static void SearchUser()
        {
            userFound = false;
            String NameSearch;
            Console.Write("Masukan username :");
            NameSearch = getInput();
            for (int i = 0; i < LstUser.Count; i++)
            {
                if(NameSearch == LstUser[i].UserNameAt)
                {
                    userFound = true;
                    Console.WriteLine("============================");
                    Console.WriteLine("NAMA : " + LstUser[i].FirstnameAt + " " + LstUser[i].LastnameAt);
                    Console.WriteLine("USERNAME : " + LstUser[i].UserNameAt);
                    Console.WriteLine("PASSWORD : " + LstUser[i].PasswordAt);
                    Console.WriteLine("============================");
                    Console.ReadKey();
                }
            }

            UserNotFound();
        }

        public static void LoginUser()
        {
          

            String InputUsername, InputPassword;
            Console.Write("Masukan Username : ");
            InputUsername = getInput();
            Console.Write("Masukan Password : ");
            InputPassword = getInput();
            for (int i = 0; i < LstUser.Count; i++)
            {
                if (LstUser[i].UserNameAt == InputUsername)
                {
                    //check password
                    if (BCrypt.Net.BCrypt.Verify(InputPassword, LstUser[i].PasswordAt))
                    {
                        Console.WriteLine("Message : Yeay, Login Telah Berhasil");
                    }
                    else
                    {
                        Console.WriteLine("Message : Opps, Password tidak ditemukan !");
                    }
                }
                else
                {
                    Console.WriteLine("Message : Opps, Username tidak ditemukan !");
                }
            }

            Console.ReadKey();
        }

        public static void DeleteUser()
        {
            int pilihuser;
            string validasilagi;
            Console.WriteLine("== PILIH USER ==");
            Console.WriteLine("No  | Nama | Username | Password");
            for (int i = 0; i < LstUser.Count; i++)
            {
                Console.WriteLine(i+ ". | " + LstUser[i].FirstnameAt + " " + LstUser[i].LastnameAt + " | " +
                    LstUser[i].UserNameAt + " | "  + LstUser[i].PasswordAt);
            }
            Console.Write("Pilih No User  : ");
            pilihuser = int.Parse(getInput());
            Console.Write("Yakin ingin dihapus [Y/T] ? ");
            validasilagi = getInput();
            if (validasilagi == "y" || validasilagi == "Y")
            {
                LstUser.RemoveAt(pilihuser);
                Console.WriteLine("User Berhasil dihapus");
            }
            else
            {
                Console.WriteLine("Delete user dibatalkan");
            }
            Console.ReadKey();
        }

        public static void UpdateUser()
        {

        pilihuser:
            Console.Clear();
            int pilihuser;
            Console.WriteLine("== PILIH USER ==");
            Console.WriteLine("No  | Nama | Username | Password");
            for (int i = 0; i < LstUser.Count; i++)
            {
                Console.WriteLine(i + ". | " + LstUser[i].FirstnameAt + " " + LstUser[i].LastnameAt + " | " +
                    LstUser[i].UserNameAt + " | " + LstUser[i].PasswordAt);
            }
            Console.Write("Pilih No User  : ");
            pilihuser = Convert.ToInt32(getInput());
            if(pilihuser >= LstUser.Count)
            {
                Console.WriteLine("Nomor User tidak ditemukan");
                Console.ReadKey();
                goto pilihuser;
            }
            else
            {
                Console.Clear();
            newFirst:
                Console.Write("New First Name : ");
                string newFirstname = getInput();
                bool FirstnameValidate = NameValidation(newFirstname);
                //Console.WriteLine("hasil F :" + FirstnameValidate);
                if (!FirstnameValidate)
                {
                    Console.WriteLine("Firstname harus memiliki minimal 2 huruf");
                    Console.ReadKey();
                    Console.Clear();
                    goto newFirst;
                }
                else
                {
                    LstUser[pilihuser].FirstnameAt = newFirstname;
                }
            newLast:
                Console.Write("New Last Name : ");
                string newLastname = getInput();
                bool LastnameValidate = NameValidation(newLastname);
                //Console.WriteLine("hasil F :" + LastnameValidate);
                if (!LastnameValidate)
                {
                    Console.WriteLine("Lastname harus memiliki minimal 2 huruf");
                    Console.ReadKey();
                    Console.Clear();
                    goto newLast;
                }
                else
                {
                    LstUser[pilihuser].LastnameAt = newLastname;
                }
            newUsername:
                Console.Write("New Username : ");
                string newUsername = getInput();
                bool UsernameValidate = NameValidation(newUsername);
                //Console.WriteLine("hasil F :" + UsernameValidate);
                if (!UsernameValidate)
                {
                    Console.WriteLine("Username harus memiliki minimal 2 huruf");
                    Console.ReadKey();
                    Console.Clear();
                    goto newUsername;
                }
                else
                {
                    LstUser[pilihuser].UserNameAt = newUsername;
                }

            newPass:
                Console.Write("New Password : ");
                string newPasswordInput = getInput();
                bool passvalidate = PasswordValidation(newPasswordInput);
                //Console.WriteLine("hasil P :" + FirstnameValidate);
                if (!passvalidate)
                {
                    Console.WriteLine("Password harus mengandung angka, huruf besar,dan minimal 8 karakter");
                    Console.ReadKey();
                    Console.Clear();
                    goto newPass;
                }
                else
                {
                    LstUser[pilihuser].PasswordAt = HashPassword(newPasswordInput);
                }
                Console.Write("Data user berhasil diperbarui");
                Console.ReadKey();
            }
        }

        public static string RegexNumber(string value)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var isValidated = hasNumber.IsMatch(value);
            if(isValidated == false)
            {
                Console.WriteLine("Input harus berupa angka, Coba Lagi!");
                Console.ReadKey();
            }
            return value;
        }

        static bool PasswordValidation(string password)
        {
            Console.Clear();
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
            if (password == "")
            {
                return false;
            }
            else if (!isValidated)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static bool NameValidation(string userName)
        {
            Console.Clear();
            var hasMinimum2Chars = new Regex(@".{2,}");
            var isValidated = hasMinimum2Chars.IsMatch(userName);
            if (userName == "")
            {
                return false;
            }
            else if (!isValidated)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        static void UserNotFound()
        {
            if (userFound == false)
            {
                Console.WriteLine("Data tidak ditemukan");
                Console.ReadKey();
            }
        }

        static string HashPassword(string value)
        {
            String hashChiper;
            hashChiper = BCrypt.Net.BCrypt.HashPassword(value);
            return hashChiper;
        }

    }
}
