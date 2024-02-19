using System;
using System.IO;
using System.Threading;

namespace BankConsole;
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                EmailService.SendMail();
            }
            else
            {
                showMenu();
            }
        }

        static void showMenu()
        {
            Console.Clear();
            Console.WriteLine("Selecciona una opción");
            Console.WriteLine("1 - Crear un nuevo usuario");
            Console.WriteLine("2 - Eliminar un usuario existente");
            Console.WriteLine("3 - Salir");

            int option = 0;

            do
            {
                string input = Console.ReadLine();

                if (!int.TryParse(input, out option))
                {
                    Console.WriteLine("Debes ingresar un número (1, 2 o 3)");
                }
                else if (option < 1 || option > 3)
                {
                    Console.WriteLine("Debes ingresar un número válido (1, 2 o 3)");
                }
            } while (option < 1 || option > 3);

            switch (option)
            {
                case 1:
                    CreateUser();
                    break;
                case 2:
                    DeleteUser();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
            }
        }

        static void CreateUser()
{
    Console.Clear();
    Console.WriteLine("Ingresa la información del usuario:");

    int ID;
    do
    {
        Console.Write("ID: ");
        while (!int.TryParse(Console.ReadLine(), out ID) || ID <= 0)
        {
            Console.WriteLine("Debes ingresar un ID válido (entero positivo).");
            Console.Write("ID: ");
        }

        if (Storage.IsExistingID(ID))
        {
            Console.WriteLine("¡El ID ya existe! Por favor, ingresa otro ID.");
            ID = 0;
        }

    } while (ID <= 0);
            Console.Write("Nombre: ");
            string Name = Console.ReadLine();

            string email;
            do
            {
                Console.Write("Email: ");
                email = Console.ReadLine();
            } while (!IsValidEmail(email));

            decimal balance;
            do
            {
                Console.Write("Saldo: ");
            } while (!decimal.TryParse(Console.ReadLine(), out balance) || balance < 0);

            char userType;
            do
            {
                Console.Write("Escribe 'c' si el usuario es cliente, 'e' si es empleado: ");
                userType = char.ToLower(Console.ReadKey().KeyChar);
                Console.WriteLine();
            } while (userType != 'c' && userType != 'e');

            User newUser;
            if (userType == 'c')
            {
                char taxRegime;
                do
                {
                    Console.Write("Regimen fiscal (M/S): ");
                    taxRegime = char.ToUpper(Console.ReadKey().KeyChar);
                    Console.WriteLine();
                } while (taxRegime != 'M' && taxRegime != 'S');

                newUser = new Client(ID, Name, email, balance, taxRegime);
            }
            else
            {
                Console.Write("Departamento: ");
                string department = Console.ReadLine();
                newUser = new Employee(ID, Name, email, balance, department);
            }

            Storage.AddUser(newUser);
            Console.WriteLine("Usuario creado");
            Thread.Sleep(2000);
            showMenu();
        }

        public static void DeleteUser()
{
    bool isValidID = false;
    do
    {
        Console.Clear();
        int ID;
        do
        {
            Console.Write("Ingresa el ID del usuario a eliminar: ");
        } while (!int.TryParse(Console.ReadLine(), out ID) || ID <= 0);

        if (!Storage.IsExistingID(ID))
        {
            Console.WriteLine("¡El ID ingresado no existe! Por favor, ingresa un ID válido.");
        }
        else
        {
            isValidID = true;
            string result = Storage.DeleteUser(ID);

            if (result.Equals("Success"))
            {
                Console.WriteLine("Usuario eliminado");
                Thread.Sleep(2000);
                showMenu();
            }
        }
    } while (!isValidID);
}

        static bool IsValidEmail(string email)
        {
            
            string emailPattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern);
        }
    }
