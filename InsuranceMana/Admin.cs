using System;
using System.Collections.Generic;

namespace InsuranceManagementSystem
{
    public interface IAddCoverage
    {
        void AddCoverage();
    }

    public interface IUpdateCoverage
    {
        void UpdateCoverage();
    }

    public interface IDeleteCoverage
    {
        void DeleteCoverage();
    }

    public interface IViewCoverages
    {
        void ViewCoverages();
    }

    public interface IIndex
    {
        int FindEmptyIndex(List<InsuranceCoverage> coverages);
    }

    public interface IFindCoverage
    {
        int FindCoverageIndex(List<InsuranceCoverage> coverages, string coverageType);
    }

    public class Index : IIndex
    {
        public int FindEmptyIndex(List<InsuranceCoverage> coverages)
        {
            for (int i = 0; i < coverages.Count; i++)
            {
                if (coverages[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public class FindCoverage : IFindCoverage
    {
        public int FindCoverageIndex(List<InsuranceCoverage> coverages, string coverageType)
        {
            for (int i = 0; i < coverages.Count; i++)
            {
                if (coverages[i] != null && coverages[i].Type.Equals(coverageType, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public class Add : IAddCoverage
    {
        private readonly List<InsuranceCoverage> basicCoverages;
        private readonly List<InsuranceCoverage> premiumCoverages;

        public Add(List<InsuranceCoverage> basicCoverages, List<InsuranceCoverage> premiumCoverages)
        {
            this.basicCoverages = basicCoverages;
            this.premiumCoverages = premiumCoverages;
        }

        public void AddCoverage()
        {
            Console.WriteLine("Enter insurance type (basic/premium):");
            string insuranceType = Console.ReadLine();
            Console.WriteLine("Enter coverage type:");
            string coverageType = Console.ReadLine();
            Console.WriteLine("Enter coverage timeline (years):");
            int timeline = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter coverage amount (dollars):");
            double amount = double.Parse(Console.ReadLine());

            List<InsuranceCoverage> selectedCoverages = insuranceType.ToLower() == "basic" ? basicCoverages : premiumCoverages;

            selectedCoverages.Add(new InsuranceCoverage(coverageType, timeline, amount));
            Console.WriteLine("Coverage added successfully!");
        }
    }

    public class aUpdate : IUpdateCoverage
    {
        private readonly List<InsuranceCoverage> basicCoverages;
        private readonly List<InsuranceCoverage> premiumCoverages;
        private readonly IFindCoverage findCoverage;

        public aUpdate(List<InsuranceCoverage> basicCoverages, List<InsuranceCoverage> premiumCoverages, IFindCoverage findCoverage)
        {
            this.basicCoverages = basicCoverages;
            this.premiumCoverages = premiumCoverages;
            this.findCoverage = findCoverage;
        }

        public void UpdateCoverage()
        {
            Console.WriteLine("Enter insurance type (basic/premium):");
            string insuranceType = Console.ReadLine();
            Console.WriteLine("Enter coverage type to update:");
            string coverageType = Console.ReadLine();

            List<InsuranceCoverage> selectedCoverages = insuranceType.ToLower() == "basic" ? basicCoverages : premiumCoverages;
            int index = findCoverage.FindCoverageIndex(selectedCoverages, coverageType);

            if (index != -1)
            {
                Console.WriteLine("Enter new timeline (years):");
                int timeline = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter new amount (dollars):");
                double amount = double.Parse(Console.ReadLine());

                selectedCoverages[index].Timeline = timeline;
                selectedCoverages[index].Amount = amount;

                Console.WriteLine("Coverage updated successfully!");
            }
            else
            {
                Console.WriteLine("Coverage type not found!");
            }
        }
    }

    public class Delete : IDeleteCoverage
    {
        private readonly List<InsuranceCoverage> basicCoverages;
        private readonly List<InsuranceCoverage> premiumCoverages;
        private readonly IFindCoverage findCoverage;

        public Delete(List<InsuranceCoverage> basicCoverages, List<InsuranceCoverage> premiumCoverages, IFindCoverage findCoverage)
        {
            this.basicCoverages = basicCoverages;
            this.premiumCoverages = premiumCoverages;
            this.findCoverage = findCoverage;
        }

        public void DeleteCoverage()
        {
            Console.WriteLine("Enter insurance type (basic/premium):");
            string insuranceType = Console.ReadLine();
            Console.WriteLine("Enter coverage type to delete:");
            string coverageType = Console.ReadLine();

            List<InsuranceCoverage> selectedCoverages = insuranceType.ToLower() == "basic" ? basicCoverages : premiumCoverages;
            int index = findCoverage.FindCoverageIndex(selectedCoverages, coverageType);

            if (index != -1)
            {
                selectedCoverages[index] = null;
                Console.WriteLine("Coverage deleted successfully!");
            }
            else
            {
                Console.WriteLine("Coverage type not found!");
            }
        }
    }

    public class D
    {
        private IDeleteCoverage _delete;

        public D(IDeleteCoverage d)
        {
            this._delete = d;
        }
        public void call()
        {
            this._delete.DeleteCoverage();
        }
    }


    public class aView : IViewCoverages
    {
        private readonly List<InsuranceCoverage> basicCoverages;
        private readonly List<InsuranceCoverage> premiumCoverages;

        public aView(List<InsuranceCoverage> basicCoverages, List<InsuranceCoverage> premiumCoverages)
        {
            this.basicCoverages = basicCoverages;
            this.premiumCoverages = premiumCoverages;
        }

        public void ViewCoverages()
        {
            Console.WriteLine("+------------------+------------------+------------------+------------------+");
            Console.WriteLine("| Insurance Type   | Coverage Type    | Timeline (Years) | Amount (Dollars) |");
            Console.WriteLine("+------------------+------------------+------------------+------------------+");

            PrintCoverages(basicCoverages, "Basic");
            PrintCoverages(premiumCoverages, "Premium");

            Console.WriteLine("+------------------+------------------+------------------+------------------+");
        }

        private void PrintCoverages(List<InsuranceCoverage> coverages, string insuranceType)
        {
            foreach (var coverage in coverages)
            {
                if (coverage != null)
                {
                    Console.WriteLine($"| {insuranceType,-17}| {coverage.Type,-17}| {coverage.Timeline,-17}| ${coverage.Amount,-17}|");
                }
            }
        }
    }


    public class Admin
    {
        private readonly List<InsuranceCoverage> basicCoverages;
        private readonly List<InsuranceCoverage> premiumCoverages;

        public Admin(List<InsuranceCoverage> basic, List<InsuranceCoverage> premium)
        {
            basicCoverages = basic;
            premiumCoverages = premium;
        }

        public void AdminMenu()
        {
            Index indexFinder = new Index();
            FindCoverage findCoverage = new FindCoverage();

            Add addCoverage = new Add(basicCoverages, premiumCoverages);
            aUpdate updateCoverage = new aUpdate(basicCoverages, premiumCoverages, findCoverage);

            Delete deleteCoverage = new Delete(basicCoverages, premiumCoverages, findCoverage);
            D del = new D(deleteCoverage);//passing dependency

            aView viewCoverages = new aView(basicCoverages, premiumCoverages);

            bool exit = false;
            do
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Add Coverage");
                Console.WriteLine("2. Update Coverage");
                Console.WriteLine("3. Delete Coverage");
                Console.WriteLine("4. View Coverages");
                Console.WriteLine("");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:

                        addCoverage.AddCoverage();
                        break;
                    case 2:
                        updateCoverage.UpdateCoverage();
                        break;
                    case 3:
                        del.call();
                        break;
                    case 4:
                        viewCoverages.ViewCoverages();
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            } while (!exit);
        }
    }

    public class InsuranceCoverage
    {
        public string Type { get; }
        public int Timeline { get; set; }
        public double Amount { get; set; }

        public InsuranceCoverage(string type, int timeline, double amount)
        {
            Type = type;
            Timeline = timeline;
            Amount = amount;
        }
    }


}