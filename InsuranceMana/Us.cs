using System;
using System.Collections.Generic;

namespace InsuranceManagementSystem
{
    public class Us
    {
        private readonly List<InsuranceCoverage> basicCoverages;
        private readonly List<InsuranceCoverage> premiumCoverages;

        public Us(List<InsuranceCoverage> basic, List<InsuranceCoverage> premium)
        {
            basicCoverages = basic;
            premiumCoverages = premium;
        }

        public void DisplayUserMenu()
        {
            Console.WriteLine("Choose insurance type (basic/premium):");
            string insuranceType = Console.ReadLine();

            if (insuranceType == "basic")
            {
                DisplayPolicies(basicCoverages);
            }
            else if (insuranceType == "premium")
            {
                DisplayPolicies(premiumCoverages);
            }
            else
            {
                Console.WriteLine("Invalid insurance type!");
            }
        }

        private void DisplayPolicies(List<InsuranceCoverage> coverages)
        {
            Console.WriteLine($"Available Coverages:");
            for (int i = 0; i < coverages.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {coverages[i].Type}");
            }

            Console.WriteLine("Choose a policy to view details:");
            int choice = int.Parse(Console.ReadLine());

            if (choice > 0 && choice <= coverages.Count)
            {
                ViewPolicyDetails(coverages[choice - 1]);
            }
            else
            {
                Console.WriteLine("Invalid choice!");
            }
        }

        private void ViewPolicyDetails(InsuranceCoverage coverage)
        {

            Console.WriteLine($"Policy Type: {coverage.Type}");
            Console.WriteLine($"Timeline: {coverage.Timeline} years");
            Console.WriteLine($"Amount: ${coverage.Amount}");

            Console.WriteLine("1. Go to payment page");
            Console.WriteLine("2. View policies again");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    // Call method to navigate to payment page
                    PaymentProcessor payment = new Payment();
                    bool success = payment.ProcessPayment(coverage);
                    if (!success)
                    {
                        DisplayUserMenu(); // Redirect to choose insurance type
                    }
                    break;
                case 2:
                    // Call method to display policies again
                    DisplayUserMenu();
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    public class UserInsuranceCoverage
    {
        public string Type { get; }
        public int Timeline { get; }
        public double Amount { get; }

        public UserInsuranceCoverage(string type, int timeline, double amount)
        {
            Type = type;
            Timeline = timeline;
            Amount = amount;
        }
    }
}