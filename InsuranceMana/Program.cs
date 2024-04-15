using System;
using System.Collections.Generic;

namespace InsuranceManagementSystem
{
    class Program
    {
        public static List<InsuranceCoverage> basicCoverages = new List<InsuranceCoverage>
        {
            new InsuranceCoverage("Job", 1, 1000),
            new InsuranceCoverage("Self", 2, 2000),
            new InsuranceCoverage("Benefit", 3, 3000)
        };

        public static List<InsuranceCoverage> premiumCoverages = new List<InsuranceCoverage>
        {
            new InsuranceCoverage("AA", 5, 5000),
            new InsuranceCoverage("NN", 10, 10000)
        };

        static UserDatabase userDatabase = new UserDatabase();

        static void Main(string[] args)
        {
            // Welcome message and main menu options
            Console.WriteLine("Welcome to Insurance Management System!");

            bool exit = false;
            do
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. User");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        AdminLoginService.AdminLogin(userDatabase, basicCoverages, premiumCoverages); // Admin login functionality
                        break;
                    case 2:
                        UserMenuService.UserMenu(userDatabase, basicCoverages, premiumCoverages); // User menu options
                        break;
                    case 3:
                        exit = true; // Exit the program
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (!exit);

            Console.WriteLine("Thank you for using Insurance Management System!");
        }
    }

    // Admin login service class
    public class AdminLoginService
    {
        public static void AdminLogin(UserDatabase userDatabase, List<InsuranceCoverage> basicCoverages, List<InsuranceCoverage> premiumCoverages)
        {
            Admin obj = new Admin(basicCoverages, premiumCoverages);
            Console.WriteLine("Admin Login:");
            Console.Write("Enter admin username: ");
            string adminUsername = Console.ReadLine();
            Console.Write("Enter admin password: ");
            string adminPassword = Console.ReadLine();

            if (userDatabase.IsValidAdmin(adminUsername, adminPassword))
            {
                Console.WriteLine("Admin login successful!");
                // Implement admin functionalities here
                obj.AdminMenu();
            }
            else
            {
                Console.WriteLine("Invalid admin username or password.");
            }
        }
    }

    // User menu service class
    public class UserMenuService
    {
        public static void UserMenu(UserDatabase userDatabase, List<InsuranceCoverage> basicCoverages, List<InsuranceCoverage> premiumCoverages)
        {
            bool exit = false;
            do
            {
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        UserRegistrationService.RegisterUser(userDatabase); // User registration functionality
                        break;

                    case 2:
                        UserLoginService.LoginUser(userDatabase, basicCoverages, premiumCoverages);
                        break;
                    // User login functionality

                    case 3:
                        exit = true; // Exit the user menu
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (!exit);
        }
    }

    // User registration service class
    public class UserRegistrationService
    {
        public static void RegisterUser(UserDatabase userDatabase)
        {
            Console.WriteLine("User Registration:");
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter your address: ");
            string address = Console.ReadLine();
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (!userDatabase.IsUsernameTaken(username))
            {
                User newUser = new User(name, age, address, username, password);
                userDatabase.AddUser(newUser);
                Console.WriteLine("Registration successful!");
            }
            else
            {
                Console.WriteLine("Username already taken. Please choose another one.");
            }
        }
    }

    // User login service class
    // User login service class
    public class UserLoginService
    {
        public static void LoginUser(UserDatabase userDatabase, List<InsuranceCoverage> basicCoverages, List<InsuranceCoverage> premiumCoverages)
        {
            Console.WriteLine("User Login:");
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            if (userDatabase.IsValidUser(username, password))
            {
                Console.WriteLine("Login successful!");
                User loggedInUser = userDatabase.GetUserByUsername(username);
                UserDetailsService.UserMenu(loggedInUser, basicCoverages, premiumCoverages);
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }
    }

    // User details service class
    public class UserDetailsService
    {
        public static void UserMenu(User user, List<InsuranceCoverage> basicCoverages, List<InsuranceCoverage> premiumCoverages)
        {
            bool exit = false;
            do
            {
                Console.WriteLine("User Menu:");
                Console.WriteLine("1. View My Details");
                Console.WriteLine("2. Update My Details");
                Console.WriteLine("3. View Policy Details");
                Console.WriteLine("4. Logout");
                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        View.ViewUserDetails(user); // View user details functionality
                        break;
                    case 2:
                        Update.UpdateUserDetails(user); // Update user details functionality
                        break;
                    case 3:
                        Us u = new Us(basicCoverages, premiumCoverages);
                        u.DisplayUserMenu();// Display policy details
                        break;
                    case 4:
                        exit = true; // Logout
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (!exit);
        }
    }

    public class View
    {
        public static void ViewUserDetails(User user)
        {
            Console.WriteLine($"Name: {user.Name}");
            Console.WriteLine($"Age: {user.Age}");
            Console.WriteLine($"Address: {user.Address}");
            Console.WriteLine($"Username: {user.Username}");
        }
    }

    public class Update
    {
        public static void UpdateUserDetails(User user)
        {
            Console.WriteLine("Update your details:");
            Console.Write("Enter your name: ");
            user.Name = Console.ReadLine();
            Console.Write("Enter your age: ");
            user.Age = int.Parse(Console.ReadLine());
            Console.Write("Enter your address: ");
            user.Address = Console.ReadLine();
            Console.Write("Enter your new password: ");
            user.Password = Console.ReadLine();
            Console.WriteLine("Details updated successfully!");
        }
    }

    // User class representing user data structure
    public class User
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string name, int age, string address, string username, string password)
        {
            Name = name;
            Age = age;
            Address = address;
            Username = username;
            Password = password;
        }
    }

    // UserDatabase class representing the database of users
    public class UserDatabase
    {
        private User[] users = new User[100];
        private int userCount = 0;
        private string adminUsername = "admin";
        private string adminPassword = "admin123";

        // Method responsible for adding a user to the database
        public void AddUser(User user)
        {
            users[userCount++] = user;
        }

        // Method responsible for checking if a username is already taken
        public bool IsUsernameTaken(string username)
        {
            foreach (User user in users)
            {
                if (user != null && user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        // Method responsible for validating user credentials
        public bool IsValidUser(string username, string password)
        {
            foreach (User user in users)
            {
                if (user != null && user.Username == username && user.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        // Method responsible for retrieving a user by username
        public User GetUserByUsername(string username)
        {
            foreach (User user in users)
            {
                if (user != null && user.Username == username)
                {
                    return user;
                }
            }
            return null;
        }

        // Method responsible for validating admin credentials
        public bool IsValidAdmin(string username, string password)
        {
            return username == adminUsername && password == adminPassword;
        }
    }



}