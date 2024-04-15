using System;

namespace InsuranceManagementSystem
{
    public class PaymentProcessor
    {
        public virtual bool ProcessPayment(InsuranceCoverage selectedCoverage)
        {
            Console.WriteLine("Payment Details:");
            Console.WriteLine("+------------------+------------------+------------------+------------------+");
            Console.WriteLine("| Insurance Type   | Coverage Type    | Timeline (Years) | Amount (Dollars) |");
            Console.WriteLine("+------------------+------------------+------------------+------------------+");
            Console.WriteLine($"| {selectedCoverage.Type,-17}| {selectedCoverage.Type,-17}| {selectedCoverage.Timeline,-17}| ${selectedCoverage.Amount,-17}|");
            Console.WriteLine("+------------------+------------------+------------------+------------------+");

            Console.WriteLine("Enter amount: ");
            int amt = Convert.ToInt32(Console.ReadLine());

            if (amt > selectedCoverage.Amount)
            {
                Console.WriteLine("Transaction successful");
                // Implement payment gateway integration here
                return true;
            }
            else
            {
                Console.WriteLine("Transaction unsuccessful....Redirecting to User Menu");
                // Redirect to choose insurance type
                return false;
            }
        }
    }

    public class Payment : PaymentProcessor
    {
        public override bool ProcessPayment(InsuranceCoverage selectedCoverage)
        {
            Console.WriteLine("Payment Details:");
            Console.WriteLine("+------------------+------------------+------------------+------------------+");
            Console.WriteLine("| Insurance Type   | Coverage Type    | Timeline (Years) | Amount (Dollars) |");
            Console.WriteLine("+------------------+------------------+------------------+------------------+");
            Console.WriteLine($"| {selectedCoverage.Type,-17}| {selectedCoverage.Type,-17}| {selectedCoverage.Timeline,-17}| ${selectedCoverage.Amount,-16}|");
            Console.WriteLine("+------------------+------------------+------------------+------------------+");

            Console.WriteLine("Enter account balance : ");
            int amt = Convert.ToInt32(Console.ReadLine());

            if (amt > selectedCoverage.Amount)
            {
                Console.WriteLine("Transaction successful");
                double sum = amt - selectedCoverage.Amount;
                Console.WriteLine("Account balance after Transaction = " + sum);
                // Implement payment gateway integration here
                return true;
            }
            else
            {
                Console.WriteLine("Insufficient balance \n Transaction unsuccessful.....Redirecting to User Menu");
                // Redirect to choose insurance type
                return false;
            }
        }
    }
}