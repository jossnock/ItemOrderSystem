
class Item
{
    public int Number { get; set; }
    public string Name { get; set; }
    public string Size { get; set; }
    public float Price { get; set; }
    public string[]? Qualities { get; set; }

    public Item(int number, string name, string size, float price)
    {
        Number = number; Name = name; Size = size; Price = price;
    }
    public Item(int number, string name, string size, float price, string[] qualities)
    {
        Number = number; Name = name; Size = size; Price = price; Qualities = qualities;
    }


    public static void WriteItem(Item inputItem)
    {
        int number = inputItem.Number;
        string name = inputItem.Name;
        float price = inputItem.Price;

        // updating name to include qualities and size:
        if (!(inputItem.Qualities == null || inputItem.Qualities.Length == 0))
            foreach (string s in inputItem.Qualities) name += $", {s}";
        if (!string.IsNullOrEmpty(inputItem.Size)) name += $" ({inputItem.Size})";

        Console.Write($"| {number} | {name} | {price} |");
    }
    public static void WriteItem(Item inputItem, int namePadding = 0, int pricePadding = 0) // capability for padding to be passed into WriteItem
    {
        int number = inputItem.Number;
        string name = inputItem.Name;
        string price = inputItem.Price.ToString().PadRight(pricePadding);

        // updating name to include qualities and size:
        if (!(inputItem.Qualities == null || inputItem.Qualities.Length == 0))
            foreach (string s in inputItem.Qualities) name += $", {s}";
        if (!string.IsNullOrEmpty(inputItem.Size)) name += $" ({inputItem.Size})";
        name = name.PadRight(namePadding);

        Console.Write($"| {number} dea| {name} | {price} |");
    }

    public static void WriteAllItems(Dictionary<Item, int> items)
    {
        // Setting column heading widths:
        int longestWordLength = 4; // length of column heading
        int highestPriceDigits = 5; // length of column heading
        int highestQuantityDigits = 8; // length of column heading
        foreach (KeyValuePair<Item, int> item in items)
        {
            if (item.Key.Name.Length + 1 + item.Key.Size.Length > longestWordLength)
                longestWordLength = item.Key.Name.Length + 3 + item.Key.Size.Length; // + 3 accounts for space and brackets
            if (item.Key.Price.ToString().Length > highestPriceDigits)
                highestPriceDigits = item.Key.Price.ToString().Length;
        }

        // Outputing table:
        Console.WriteLine($"| {"Name".PadRight(longestWordLength)} | {"Price".PadRight(highestPriceDigits)} | {"Quantity".PadRight(highestQuantityDigits)} |");
        Console.WriteLine("|-" + new string('-', longestWordLength) + "-|-" + new string('-', highestPriceDigits) + "-|-" + new string('-', highestQuantityDigits) + "-|"); // + 7 accounts for spacing and vertical bar
        foreach (KeyValuePair<Item, int> item in items)
        {
            WriteItem(item.Key, longestWordLength, highestPriceDigits);
            Console.WriteLine($" | { Convert.ToString(item.Value).PadRight(highestQuantityDigits)} |");
        }
    }
}

class Account
{
    public float Funds { get; set; }
    public Dictionary<Item, int> Inventory { get; set; }

    public Account(float funds, Dictionary<Item, int> inventory)
    {
        Funds = funds; Inventory = inventory;
    }
    public Account(float funds)
    {
        Funds = funds; Inventory = new Dictionary<Item, int>();
    }
}

class EndUser : Account
{
    public string Name { get; set; }
    public string Password { get; set; }
    public EndUser(string name, string password, float funds, Dictionary<Item, int> inventory) : base(funds, inventory)
    {
        Name = name; Password = password; Funds = funds; Inventory = inventory;
    }
    public EndUser(string name, string password, float funds) : base(funds)
    {
        Name = name; Password = password; Funds = funds;
    }

    public static void RunInventoryWindow(EndUser user)
    {
        string choiceMessage = "Type 'return' to return to the shop window"; // set before so it can be changed in the loop
        while (true)
        {
            Console.Clear();
            Item.WriteAllItems(user.Inventory);

            Console.WriteLine(" "); // spacing
            Console.WriteLine(choiceMessage);
            string? inputStr = Console.ReadLine();

            if (inputStr == null || inputStr == string.Empty)
            {
                choiceMessage = "Please type a valid command";
                continue;
            }
            if (inputStr == "return")
            {
                Console.Clear();
                break;
            }
        }
    }
}

class ShopSystem : Account
{
    public ShopSystem(float funds, Dictionary<Item, int> inventory) : base(funds, inventory)
    {
        Funds = funds; Inventory = inventory;
    }

    public static void RunShopWindow(ShopSystem shop, EndUser user)
    {
        string choiceMessage = "type the name of an item, or type 'inventory' for your inventory"; // set before so it can be changed in the loop
        while (true)
        {
            Console.Clear();
            Item.WriteAllItems(shop.Inventory);

            string[] itemNames = shop.Inventory.Select(x => x.Key.Name).ToArray();
            string itemNamesStr = string.Join(", ", itemNames);
            Console.WriteLine(" "); // spacing
            Console.WriteLine(choiceMessage);
            string? chosenItemStr = Console.ReadLine();

            if (chosenItemStr == null || chosenItemStr == string.Empty)
            {
                choiceMessage = "Please choose a valid item";
                continue;
            }
            else if (chosenItemStr == "inventory")
            {
                EndUser.RunInventoryWindow(user);
                continue;
            }
            else if (!itemNamesStr.Contains(chosenItemStr))
            {
                choiceMessage = "Please choose a valid item";
                continue;
            }
            choiceMessage = "Choose an item"; // resets choiceMessage once an input is accepted

            // converting input to Item:
            int index = Array.IndexOf(itemNames, chosenItemStr);
            Item chosenItem = shop.Inventory[index];
            user.Inventory.Add(chosenItem, 1);
        }
    }
}

class ItemOrderSystem
{
    public static void Main(string[] args)
    {
        // defining shop:
        Dictionary<Item, int> allItems = new Dictionary<Item, int>();
        allItems.Add(new Item(1, "whole milk", "2 litres", 3.5f), 10);
        allItems.Add(new Item(2, "white bread", "1 loaf", 2), 10);
        allItems.Add(new Item(3, "wholemeal bread", "1 loaf", 2), 10);
        allItems.Add(new Item(4, "eggs", "dozen", 2.5f), 10);
        allItems.Add(new Item(5, "bananas", "500 grams", 0.6f), 10);
        allItems.Add(new Item(6, "chicken breast", "450 grams", 3), 10);
        allItems.Add(new Item(7, "beef mince", "450 grams", 5), 10);
        allItems.Add(new Item(8, "cheddar cheese", "250 grams", 3), 10);
        allItems.Add(new Item(9, "apples", "500 grams", 1.5f), 10);
        allItems.Add(new Item(10, "orange juice", "1 litre", 1.3f), 10);
        allItems.Add(new Item(11, "spaghetti", "450 grams", 1.2f), 10);
        allItems.Add(new Item(12, "tomato sauce", "600 grams", 1.5f), 10);
        allItems.Add(new Item(13, "peas, frozen", "500 grams", 1), 10);
        allItems.Add(new Item(14, "rice", "1 kilogram", 1.8f), 10);
        allItems.Add(new Item(15, "peanut butter", "500 grams", 2.5f), 10);
        allItems.Add(new Item(16, "yogurt", "170 millilitres", 0.8f), 10);
        var shop = new ShopSystem(1000, allItems);

        // defining user:
        var user = new EndUser("Tester", "asdf1234!", 100f);

        // running Item Order System:
        ShopSystem.RunShopWindow(shop, user);
    }
}

