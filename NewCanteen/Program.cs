// Data and Menu

using System;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;

CultureInfo culture = new CultureInfo("id-ID");
string[] productName = { "Fried Rice", "Fried Noodles", "Smashed Fried Chicken", "Ice Tea", "Minerale Water" };
int[] productPrice = { 15000, 12000, 18000, 5000, 4000 };
int[] salesAmount = new int[5];
int chooseCode, salesAmountIndex, amount;
string buyAgain;
decimal total = 0, subtotal, discountPercentage;
decimal discount, totalPay, cashPay, change;
long totalUnit = 0;
int largest = 0, smallest = int.MaxValue;

Console.WriteLine("List Menu");
for (int i = 0; i < productName.Length; i++)
{
    Console.WriteLine($"{i + 1}. {productName[i]} : Rp " + productPrice[i].ToString("No", culture));
}

// Validate products code

do
{
    Console.Write("Input Product Code: ");
    if (!int.TryParse(Console.ReadLine(), out chooseCode));
    {
        Console.WriteLine("Code must be a number!");
        continue;
    }

    switch (chooseCode)
    {
        case 1: salesAmountIndex = 0; break;
        case 2: salesAmountIndex = 1; break;
        case 3: salesAmountIndex = 2; break;
        case 4: salesAmountIndex = 3; break;
        case 5: salesAmountIndex = 4; break;
        default: salesAmountIndex = -1; break;
    }
    if (salesAmountIndex == -1)
    {
        Console.WriteLine("Option not Available. Code 1-5!");
        continue;
    }

    // Request and Store The Amount

    while (true)
    {
        Console.Write("Input the quantity: ");
        if (!int.TryParse(Console.ReadLine(), out amount) || amount < 1)
        {
            Console.WriteLine("Quantity must a whole number, minimal 1!");
            continue;
        }
        if (amount > int.MaxValue - salesAmount[salesAmountIndex])
        {
            Console.WriteLine("Quantity is too large. Reduce Quantity!");
            continue;
        }
        break;
    }
    salesAmount[salesAmountIndex] += amount;

    // Repeat Purchase

    do
    {
        Console.Write("add more item? (Yes/No): ");
        buyAgain = (Console.ReadLine() ?? "").Trim().ToUpper();
        if (buyAgain != "Yes" && buyAgain != "No")
            Console.WriteLine("Answer Yes or No");
    } while (buyAgain != "Yes" && buyAgain != "No");

    if (buyAgain == "No")
        break;
} while (true);

// Calculating total and Purchase data

for (int i = 0; i < salesAmount.Length; i++)
{
    if (salesAmount[i] > 0)
    {
        subtotal = (decimal)productPrice[i] * salesAmount[i];
        total += subtotal;
        totalUnit = salesAmount[i];
        if (salesAmount[i] > largest) largest = salesAmount[i];
        if (salesAmount[i] < smallest) smallest = salesAmount[i];
    }
}

// Determine Discount

if (total >= 100000m) discountPercentage = 0.10m;
else if (total >= 50000m) discountPercentage = 0.05m;
else discountPercentage = 0m;

discount = total * discountPercentage;
totalPay = total - discount;
Console.WriteLine("Pay total: Rp" + totalPay.ToString("NO", culture));

// Validate Purchases

while (true)
{
    
Console.Write("Payment Nominal (Number without . and ,): Rp");
string payInput = (Console.ReadLine() ?? "").Trim();
if (!decimal.TryParse(payInput, NumberStyles.None, CultureInfo.InvariantCulture, out cashPay))
{
    Console.WriteLine("Input whole Rupiah, example 50000!");
        continue;
}
if (cashPay < totalPay)
{
    Console.WriteLine("Need more Payment Amount");
    continue;
}
break;
}
change = cashPay -  totalPay;

// Print the receipt

Console.WriteLine("=================================");
Console.WriteLine("          SCHOOL CANTEEN");
Console.WriteLine("=================================");
for (int i = 0; i < salesAmount.Length; i++)
{
    if (salesAmount[i] > 0)
    {
        subtotal = (decimal)productPrice[i] * salesAmount[i];
        Console.WriteLine($"{productName[i]} {salesAmount[i]} x" + $"{productPrice[i]} = Rp {subtotal.ToString("NO", culture)}");
    }
}
Console.WriteLine("---------------------------------");
Console.WriteLine($"Total Buy = Rp {total.ToString("NO", culture)}");
Console.WriteLine($"Discount {discountPercentage * 100:0}% = Rp " + discount.ToString("NO", culture));
Console.WriteLine($"Total Pay = Rp {totalPay.ToString("NO", culture)}");
Console.WriteLine($"Pay Amount = Rp {cashPay.ToString("NO", culture)}");
Console.WriteLine($"Change = Rp {change.ToString("NO", culture)}");
Console.WriteLine($"Unit Total = {totalUnit}");
Console.WriteLine($"Largest Product {largest} unit): ");
for (int i = 0; i < salesAmount.Length; i++)
    if (salesAmount[i] == largest) Console.WriteLine("- " + productName[i]);
Console.WriteLine("==================================");
Console.WriteLine("          THANK YOU");