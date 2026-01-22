namespace AssetTrackingWithEF.Helpers;

public static class Validators
{
    public static string ValidateInput(string question, string Field)
    {
        while (true)
        {
            Console.Write(question);
            string input = Console.ReadLine();

            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }
            else
            {
                ConsoleHelpers.WriteColoredText(ConsoleColor.Red, $"{Field} cannot be empty. Please try again");
            }
        }
    }

    public static DateTime ValidateDate(string question)
    {
        while (true)
        {
            Console.Write(question);
            string response = Console.ReadLine();
            DateTime PurchaseDate = new DateTime();

            if (DateTime.TryParse(response, out PurchaseDate))
            {
                return PurchaseDate;
            }
            else
            {
                ConsoleHelpers.WriteColoredText(ConsoleColor.Red, "Invalid date format. Please enter Date of purchase in the format YYYY-MM-DD");
            }
        }
    }

    public static double ValidateDouble(string question)
    {
        while (true)
        {
            Console.Write(question);
            string response = Console.ReadLine();
            double PricePaid = 0;

            if (double.TryParse(response, out PricePaid))
            {
                return PricePaid = double.Parse(response);
            }
            else
            {
                ConsoleHelpers.WriteColoredText(ConsoleColor.Red, "Invalid number. Please try again");
            }
        }
    }
}
