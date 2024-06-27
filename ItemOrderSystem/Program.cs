
class Item
{
    public string Name { get; set; } = string.Empty;
    public string[] Qualities { get; set; }
    public string Size { get; set; } = string.Empty;
    public float Price { get; set; }
    public int Quantity { get; set; }

    public static void PrintItem(Item inputItem)
    {
        string name = inputItem.Name;
        float price = inputItem.Price;
        int quantity = inputItem.Quantity;

        // updating name to include qualities and size:
        if (!(inputItem.Qualities == null || inputItem.Qualities.Length == 0))
            foreach (string s in inputItem.Qualities) name += $", {s}";
        if (!string.IsNullOrEmpty(inputItem.Size)) name += $" ({inputItem.Size})";

        Console.WriteLine($"| {name} | {price} | {quantity} |");
    }
    public static void PrintItem(Item inputItem, int namePadding = 0, int pricePadding = 0, int quantityPadding = 0) // capability for padding to be passed into PrintItem
    {
        string name = inputItem.Name;
        string price = inputItem.Price.ToString().PadRight(pricePadding);
        string quantity = inputItem.Quantity.ToString().PadRight(quantityPadding);

        // updating name to include qualities and size:
        if (!(inputItem.Qualities == null || inputItem.Qualities.Length == 0))
            foreach (string s in inputItem.Qualities) name += $", {s}";
        if (!string.IsNullOrEmpty(inputItem.Size)) name += $" ({inputItem.Size})";
        name = name.PadRight(namePadding);

        Console.WriteLine($"| {name} | {price} | {quantity} |");
    }

    public static void PrintAllItems(List<Item> items)
    {
        // Setting column heading widths:
        int longestWordLength = 4; // length of column heading
        int highestPriceDigits = 5; // length of column heading
        int highestQuantityDigits = 8; // length of column heading
        foreach (Item item in items)
        {
            if (item.Name.Length + 1 + item.Size.Length > longestWordLength)
                longestWordLength = item.Name.Length + 3 + item.Size.Length; // + 3 accounts for space and brackets
            if (item.Price.ToString().Length > highestPriceDigits)
                highestPriceDigits = item.Price.ToString().Length;
        }

        // Outputing table:
        Console.WriteLine($"| {"Name".PadRight(longestWordLength)} | {"Price".PadRight(highestPriceDigits)} | {"Quantity".PadRight(highestQuantityDigits)} |");
        Console.WriteLine("|-" + new string('-', longestWordLength) + "-|-" + new string('-', highestPriceDigits) + "-|-" + new string('-', highestQuantityDigits) + "-|"); // + 7 accounts for spacing and vertical bar
        foreach (Item item in items)
        {
            PrintItem(item, longestWordLength, highestPriceDigits, highestQuantityDigits);
        }
    }
}




class ItemOrderSystem
{
    public static void Main(string[] args)
    {
        // defining allItems:
        var item1 = new Item { Name = "whole milk", Size = "2 litres", Price = 3.5f, Quantity = 10 };
        var item2 = new Item { Name = "white bread", Qualities = new string[] { "loaf" }, Price = 2.0f, Quantity = 10 };
        var item3 = new Item { Name = "wholemeal bread", Qualities = new string[] { "loaf" }, Price = 2.0f, Quantity = 10 };
        var item4 = new Item { Name = "eggs", Size = "dozen", Price = 2.5f, Quantity = 10 };
        var item5 = new Item { Name = "bananas", Size = "500 grams", Price = 0.6f, Quantity = 10 };
        var item6 = new Item { Name = "chicken breast", Size = "450 grams", Price = 3.0f, Quantity = 10 };
        var item7 = new Item { Name = "beef mince", Size = "450 grams", Price = 5.0f, Quantity = 10 };
        var item8 = new Item { Name = "cheddar cheese", Size = "250 grams", Price = 3.0f, Quantity = 10 };
        var item9 = new Item { Name = "apples", Size = "500 grams", Price = 1.5f, Quantity = 10 };
        var item10 = new Item { Name = "orange juice", Size = "1 litre", Price = 1.3f, Quantity = 10 };
        var item11 = new Item { Name = "spaghetti", Size = "450 grams", Price = 1.2f, Quantity = 10 };
        var item12 = new Item { Name = "tomato sauce", Size = "600 grams", Price = 1.5f, Quantity = 10 };
        var item13 = new Item { Name = "peas", Qualities = new string[] { "frozen" }, Size = "500 grams", Price = 1.0f, Quantity = 10 };
        var item14 = new Item { Name = "rice", Size = "1 kilogram", Price = 1.8f, Quantity = 10 };
        var item15 = new Item { Name = "peanut butter", Size = "500 grams", Price = 2.5f, Quantity = 10 };
        var item16 = new Item { Name = "yogurt", Size = "170 millilitres", Price = 0.8f, Quantity = 10 };

        List<Item> allItems = new Item[] { item1, item2, item3, item4, item5, item6, item7, item8, item9, item10, item11, item12, item13, item14, item15, item16 }.ToList();
        
        string choiceMessage = "Choose an item";

        // running Item Order System:
        while (true)
        {
            Console.Clear();
            Item.PrintAllItems(allItems);

            string[] itemNames = allItems.Select(C => C.Name).ToArray();
            string itemNamesStr = string.Join(", ", itemNames);
            Console.WriteLine(" "); // spacing
            Console.WriteLine(choiceMessage);
            string? chosenItemStr = Console.ReadLine();

            if (!itemNamesStr.Contains(chosenItemStr))
            {
                choiceMessage = "Please choose a valid item";
                continue;
            }
            choiceMessage = "Choose an item"; // resetting choiceMessage once a correct input is inputted

            // converting input to Item:
            int index = Array.IndexOf(itemNames, chosenItemStr);
            Item chosenItem = allItems[index];












            break;
        }

            



    }

}

