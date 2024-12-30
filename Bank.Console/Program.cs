using System;
using System.IO;
using Banking.Domain;

namespace BankApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start of the app. Welcomes the user and show the main menu
            Console.WriteLine("Welcome to the Banking Application!");
            Console.WriteLine("Are you a:");
            Console.WriteLine("1. Bank Employee");
            Console.WriteLine("2. Customer");
            Console.WriteLine("Enter your choice:");

            // Get the user's choice and decide what to do
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                EmployeeLogin(); // Handle employee stuff
            }
            else if (choice == "2")
            {
                CustomerLogin(); // Handle customer stuff
            }
            else
            {
                // Invalid choice, so just exit
                Console.WriteLine("Invalid choice. Exiting...");
            }
        }

        // This is where employees log in
        static void EmployeeLogin()
        {
            var employee = new Employee(); // Pretend this is a real employee object

            Console.WriteLine("Enter the employee PIN:");
            string pin = Console.ReadLine();

            // Check if the PIN matches
            if (pin == employee.EmployeePin)
            {
                Console.WriteLine("Employee login successful!");
                EmployeeMenu(); // Go to the employee menu
            }
            else
            {
                Console.WriteLine("Invalid PIN. Access denied."); // No access for you!
            }
        }

        // Employee menu with some basic options
        static void EmployeeMenu()
        {
            string choice;
            do
            {
                // Show the menu options for the employee
                Console.WriteLine("Welcome, Employee!");
                Console.WriteLine("1. Create a new customer");
                Console.WriteLine("2. Delete a customer");
                Console.WriteLine("3. List all customers");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");
                choice = Console.ReadLine();

                // Handle the employee's choice
                switch (choice)
                {
                    case "1":
                        CreateCustomer(); // Add a new customer
                        break;
                    case "2":
                        DeleteCustomer(); // Remove a customer
                        break;
                    case "3":
                        ListCustomers(); // Show all customers
                        break;
                    case "4":
                        Console.WriteLine("Exiting employee menu."); // Done!
                        break;
                    default:
                        Console.WriteLine("Invalid choice."); // Whoops
                        break;
                }
            } while (choice != "4"); // Keep looping until they exit
        }

        // Stub for listing customers (not finished yet)
        private static void ListCustomers()
        {
            throw new NotImplementedException();
        }

        // Delete a customer after checking balances
        static void DeleteCustomer()
        {
            Console.WriteLine("Enter the account number of the customer to delete:");
            string accountNumber = Console.ReadLine();

            // Generate the file names for the customer's accounts
            string savingsFile = $"{accountNumber}-savings.txt";
            string currentFile = $"{accountNumber}-current.txt";

            // Make sure the files exist before proceeding
            if (!File.Exists(savingsFile) || !File.Exists(currentFile))
            {
                Console.WriteLine("Account files not found. Ensure the account number is correct.");
                return;
            }

            // Check the balances before deleting
            double savingsBalance = CalculateBalance(savingsFile);
            double currentBalance = CalculateBalance(currentFile);

            if (savingsBalance != 0 || currentBalance != 0)
            {
                Console.WriteLine("Cannot delete customer. Both account balances must be zero.");
                return;
            }

            // Read the customer file and finds out the one to delete
            string[] customers = File.ReadAllLines("customers.txt");
            using (StreamWriter writer = new StreamWriter("customers.txt"))
            {
                foreach (string customer in customers)
                {
                    if (!customer.StartsWith(accountNumber + "\t"))
                    {
                        writer.WriteLine(customer);
                    }
                }
            }

            // Delete the customer's account file
            File.Delete(savingsFile);
            File.Delete(currentFile);

            Console.WriteLine("Customer deleted successfully.");
        }

        // Calculate the balance from a file
        static double CalculateBalance(string filePath)
        {
            double balance = 0;

            if (!File.Exists(filePath))
                return balance; // No file no balance, no pain no gain

            // Read through the file and grab the last balance
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('\t');
                if (parts.Length == 4 && double.TryParse(parts[3], out double transactionBalance))
                {
                    balance = transactionBalance; // Update to the last balance
                }
            }

            return balance;
        }

        // Create a new customer and generate account numbers
        static void CreateCustomer()
        {
            Console.WriteLine("Enter customer's first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter customer's last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter customer's email:");
            string email = Console.ReadLine();

            // Generate a cool account number
            string initials = firstName[0].ToString().ToLower() + lastName[0].ToString().ToLower();
            int nameLength = (firstName + lastName).Length;
            int firstInitialPos = firstName[0] - 'A' + 1;
            int lastInitialPos = lastName[0] - 'A' + 1;
            string accountNumber = $"{initials}-{nameLength}-{firstInitialPos}-{lastInitialPos}";

            // Create empty files for savings and current accounts
            string savingsFile = $"{accountNumber}-savings.txt";
            string currentFile = $"{accountNumber}-current.txt";
            File.WriteAllText(savingsFile, "");
            File.WriteAllText(currentFile, "");

            // Save the customer info in a file
            string customerDetails = $"{accountNumber}\t{firstName} {lastName}\t{email}";
            File.AppendAllText("customers.txt", customerDetails + Environment.NewLine);

            Console.WriteLine($"Customer created successfully!");
            Console.WriteLine($"Account Number: {accountNumber}");
        }

        // Customer login process
        static void CustomerLogin()
        {
            Console.WriteLine("Enter your account number:");
            string accountNumber = Console.ReadLine();

            Console.WriteLine("Enter your PIN:");
            string pin = Console.ReadLine();

            // Validate the account and pin
            string[] customers = File.ReadAllLines("customers.txt");
            bool isValidCustomer = false;

            foreach (string customer in customers)
            {
                string[] details = customer.Split('\t');
                if (details.Length >= 3 && details[0] == accountNumber)
                {
                    string expectedPin = GeneratePin(details[0]);
                    if (pin == expectedPin)
                    {
                        isValidCustomer = true;
                        Console.WriteLine($"Welcome, {details[1]}!");
                        CustomerMenu(accountNumber); // Go to the customer menu
                        break;
                    }
                }
            }

            if (!isValidCustomer)
            {
                Console.WriteLine("Invalid account number or PIN. Please try again.");
            }
        }

        // Generate a pin from the account number
        static string GeneratePin(string accountNumber)
        {
            string[] parts = accountNumber.Split('-');
            if (parts.Length == 4)
            {
                return parts[2] + parts[3];
            }
            return "0000"; // Default fallback pin
        }

        // Menu for customers
        static void CustomerMenu(string accountNumber)
        {
            string choice;
            do
            {
                // Show customer options
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("1. View transaction history");
                Console.WriteLine("2. Deposit money");
                Console.WriteLine("3. Withdraw money");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice:");
                choice = Console.ReadLine();

                // Handle what the customer wants to do
                switch (choice)
                {
                    case "1":
                        ViewTransactionHistory(accountNumber);
                        break;
                    case "2":
                        ProcessTransaction(accountNumber, "Deposit");
                        break;
                    case "3":
                        ProcessTransaction(accountNumber, "Withdraw");
                        break;
                    case "4":
                        Console.WriteLine("Exiting customer menu.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            } while (choice != "4");
        }

        // Process a deposit or withdrawl
        static void ProcessTransaction(string accountNumber, string transactionType)
        {
            Console.WriteLine($"Select the account type for the {transactionType}:");
            Console.WriteLine("1. Savings");
            Console.WriteLine("2. Current");
            string accountChoice = Console.ReadLine();

            // Figure out which file to use
            string filePath = accountChoice switch
            {
                "1" => $"{accountNumber}-savings.txt",
                "2" => $"{accountNumber}-current.txt",
                _ => null
            };

            if (filePath == null)
            {
                Console.WriteLine("Invalid account type. Transaction canceled.");
                return;
            }

            Console.WriteLine($"Enter the amount to {transactionType.ToLower()}:");
            if (!double.TryParse(Console.ReadLine(), out double amount) || amount <= 0)
            {
                Console.WriteLine("Invalid amount. Transaction canceled.");
                return;
            }

            double currentBalance = CalculateBalance(filePath);

            // Check for sufficient funds if its an withdrawal
            if (transactionType == "Withdraw" && amount > currentBalance)
            {
                Console.WriteLine("Insufficient funds. Transaction canceled.");
                return;
            }

            // Update the balance based on the transactiom
            double newBalance = transactionType == "Deposit" ? currentBalance + amount : currentBalance - amount;

            // save the transaction to the file
            string transactionRecord = $"{DateTime.Now:dd-MM-yyyy}\t{transactionType}\t{amount:F2}\t{newBalance:F2}";
            File.AppendAllText(filePath, transactionRecord + Environment.NewLine);

            Console.WriteLine($"{transactionType} successful! New balance: {newBalance:F2}");
        }

        // Not done, but this would show transaction history
        static void ViewTransactionHistory(string accountNumber)
        {
            Console.WriteLine("ViewTransactionHistory not implemented yet.");
        }
    }
    //at this point I'm ok with all I have done 73348
}
