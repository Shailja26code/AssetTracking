using System;
using System.Collections.Generic;
using System.Linq;
public class Program
{
    public static void Main()
    {
        Asset asset = new Asset("", "", "", "", DateTime.Now, 0.0); // Constructor initialization
        List<Asset> asset_list = new List<Asset>(); // list of Asset
        asset.main_Menu(); // Calling main menu for user interaction
        int number = asset.Readline();
        while (true)
        {
            switch (number)
            {
                case -1:
                    Console.WriteLine("Invalid entry. Try again.");
                    number = asset.Readline();
                    break;
                case 0:
                    asset.main_Menu();
                    number = asset.Readline();
                    break;
                case 1:
                    Console.WriteLine("Please choose number of assets to be added to tracking system (1-10) :");
                    number = asset.Readline();
                    Console.WriteLine("You have selected " + number + "  each assets to be tracked");
                    // run the loop for n times per user request to fetch the asset details
                    for (int i = 0; i < number; i++)
                    {
                        asset.AddComputerAssetsToList();
                        asset_list.Add(new Asset(asset.AssetName, asset.Brand, asset.Model, asset.Office, asset.PurchaseDate, asset.Price));
                        asset.AddPhoneAssetsToList();
                        asset_list.Add(new Asset(asset.AssetName, asset.Brand, asset.Model, asset.Office, asset.PurchaseDate, asset.Price));
                    }
                    Console.WriteLine("Asset details have been added to the system. Press 2 to see the sorted list or Press 0 to return to the main menu");
                    number = asset.Readline();
                    break;
                case 2:
                    // Short asset list with Location and then Purchase date
                    var sortedAsset = asset_list.OrderBy(sort => sort.Office).ThenBy(sort => sort.PurchaseDate).ToList();
                    Console.WriteLine(String.Format("Asset".PadRight(15) + "|" + "Brand".PadRight(15) + "|" + "Model".PadRight(15) + "|" + "Office".PadRight(15) + "|" + "PurchaseDate".PadRight(15) + "|" + "Price in USD".PadRight(15) + "|" + "Local Price"));
                    Console.WriteLine("------------------------------------------------------------------------------------------");
                    foreach (Asset list in sortedAsset)
                    {
                        // Check for EOL logic
                        DateTime CurrentDate = DateTime.Now;
                        if (asset.IsEndOfLifeTo3Month(list.PurchaseDate, CurrentDate))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (asset.IsEndOfLifeTo3Month(list.PurchaseDate, CurrentDate))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        // Check if currency is defined to identify local exchange rates
                        if (list.Office.ToUpper() == "SWEDEN")
                        {
                            double exchange_rate = 10.37;
                            list.localCurrency = exchange_rate * list.Price;
                        }
                        else if (list.Office.ToUpper() == "DENMARK")
                        {
                            double exchange_rate = 6.89;
                            list.localCurrency = exchange_rate * list.Price;
                        }
                        else
                        {
                            list.localCurrency = list.Price;
                        }
                        Console.WriteLine(list.AssetName.PadRight(15) + "|" + list.Brand.PadRight(15) + "|" + list.Model.PadRight(15) + "|" + list.Office.PadRight(15) + "|" + list.PurchaseDate.ToString("dd-MM-yyyy").PadRight(15) + "|" + list.Price.ToString().PadRight(15) + "|" + list.localCurrency);
                        Console.ResetColor();
                    }
                    Console.WriteLine("------------------------------------------------------------------------------------------");
                    Console.WriteLine("Press 0 to return to the main menu or Press 3 to exit");
                    number = asset.Readline();
                    break;
                case 3:
                    Console.WriteLine("Thank you");
                    break;
                default:
                    Console.WriteLine("Please enter a valid number");
                    number = asset.Readline();
                    break;
            }
            if (number == 3)
                break;
        }
    }
}
class Asset
{
    public Asset(string name, string brand, string model, string office, DateTime purchaseDate, double price)
    {
        AssetName = name;
        Brand = brand;
        Model = model;
        Office = office;
        PurchaseDate = purchaseDate;
        Price = price;
    }
    public string AssetName { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public DateTime PurchaseDate { get; set; }
    public double Price { get; set; }
    public string Office { get; set; }
    public double localCurrency { get; set; }
    public void main_Menu()
    {
        Console.WriteLine(" >> Welcome to Assets Tracking System << ");
        Console.WriteLine(" >> Press 1 to add assets to tracking system << ");
        Console.WriteLine(" >> Press 2 to view assets and track << ");
        Console.WriteLine(" >> Press 3 to exit << ");
    }
    // Read user selection and return integer
    public int Readline()
    {
        try
        {
            return Convert.ToInt32(Console.ReadLine());
        }
        catch (Exception)
        {
            return -1;
        }
    }
    public void AddComputerAssetsToList()
    {
        // Take user inputs for Computer details
        AssetName = "Computer";
        Console.Write("Press enter the details for Computer : ");
        Console.Write("Brand : ");
        string input;
        input = Console.ReadLine();
        Brand = input;

        Console.Write("Model : ");
        input = Console.ReadLine();
        Model = input;

        Console.Write("Office location (Sweden/ Denmark/ US or other) : ");
        input = Console.ReadLine();
        Office = input;
    date:
        Console.Write("Purchased date ");
        input = Console.ReadLine();
        // Validate if date format is correct
        try { PurchaseDate = DateTime.Parse(input); }
        catch (FormatException)
        {
            Console.WriteLine("Date format is not valid, Please enter in YYYY/MM/DD format");
            goto date;
        };
    GetPrice:
        double price1;
        Console.Write("Price (In USD): ");
        input = Console.ReadLine();
        // Validate Price if it is in right format
        if (double.TryParse(input, out price1))
        {
            Price = price1;
        }
        else
        {
            Console.WriteLine("Please enter a valid price");
            goto GetPrice;
        }
    }
    public void AddPhoneAssetsToList()
    {
        // Take user inputs for Phone details
        AssetName = "Phone";
        Console.Write("Press enter the details for Phones : ");
        Console.Write("Brand : ");
        string input;
        input = Console.ReadLine();
        Brand = input;

        Console.Write("Model : ");
        input = Console.ReadLine();
        Model = input;

        Console.Write("Office location (Sweden/ Denmark/ US or other) : ");
        input = Console.ReadLine();
        Office = input;

    date:
        Console.Write("Purchased date ");
        input = Console.ReadLine();
        // Validate if date format is correct
        try { PurchaseDate = DateTime.Parse(input); }
        catch (FormatException)
        {
            Console.WriteLine("Date format is not valid, Please enter in YYYY/MM/DD format");
            goto date;
        };

    GetPrice:
        double price1;
        Console.Write("Price : ");
        input = Console.ReadLine();
        // Validate Price if it is in right format
        if (double.TryParse(input, out price1))
        {
            Price = price1;
        }
        else
        {
            Console.WriteLine("Please enter a valid price");
            goto GetPrice;
        }
    }
    public bool IsEndOfLifeTo3Month(DateTime PurchaseDate, DateTime CurrentDate)
    {
        // Derive the EOL date from purchase date to 3 month
        DateTime endOfLife = PurchaseDate.AddYears(3);
        DateTime threeMonthsFromNow = CurrentDate.AddMonths(3);
        return endOfLife <= threeMonthsFromNow;
    }
    public bool IsEndOfLifeTo6Month(DateTime PurchaseDate, DateTime CurrentDate)
    {
        // Derive the EOL date from purchase date to 6 month
        DateTime endOfLife = PurchaseDate.AddYears(3);
        DateTime sixMonthsFromNow = CurrentDate.AddMonths(6);
        return endOfLife <= sixMonthsFromNow;
    }
}
class LaptopComputers : Asset
{
    public LaptopComputers(string name, string brand, string model, string office, DateTime purchaseDate, double price) :
    base(name, brand, model, office, purchaseDate, price)
    { }
}
class Phones : Asset
{
    public Phones(string name, string brand, string model, string office, DateTime purchaseDate, double price) :
    base(name, brand, model, office, purchaseDate, price)
    { }
}

