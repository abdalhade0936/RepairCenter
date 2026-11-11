using System;
using System.Linq;

namespace RepairCenter;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("=== Home Appliance Repair Center ===");
        var center = new RepairCenter();
        Appliance[] devices =
        {
            new Fridge("F101", "Ahmad", "Damascus"),
            new Fridge("F102", "Maya", "Homs"),
            new Washer("W101", "Sara", "Aleppo"),
            new Washer("W102", "Omar", "Latakia"),
            new AirConditioner("A101", "Lina", "Tartus"),
            new AirConditioner("A102", "Khaled", "Damascus")
        };

        foreach (Appliance device in devices)
        {
            if (!RepairValidator.IsValidAppliance(device.SerialNumber, device.CustomerName, device.DeliveryLocation))
            {
                Console.WriteLine($"Invalid device data: {device.SerialNumber}");
                continue;
            }
            if (center.Register(device)) Console.WriteLine($"Registered: {device.SerialNumber} ({device.GetType().Name})");
        }

        foreach (Appliance device in center.Appliances)
        {
            device.StatusChanged += OnStatusChanged;
            device.StartRepair();
            if (device.SerialNumber == "W102") device.MarkAsDelayed();
            Console.WriteLine($"{device.SerialNumber} repair cost: {device.CalculateRepairCost():C}");
            device.CompleteRepair();
        }

        Console.WriteLine("\n=== Reports ===");
        Console.WriteLine($"Registered devices: {center.Appliances.Count}");
        Console.WriteLine($"Repaired devices: {center.GetRepairedDevices().Count()}");
        Console.WriteLine($"Devices delayed at least once: {center.GetDelayedDevices().Count()}");
        Console.WriteLine($"Total repair cost: {center.Appliances.Sum(x => x.CalculateRepairCost()):C}");

        Console.WriteLine("\n=== Validation check ===");
        Console.WriteLine($"Empty serial accepted: {RepairValidator.IsValidSerialNumber("")}");
        try
        {
            center.Appliances[0].CustomerName = "Changed after completion";
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Protected Repaired record: {ex.Message}");
        }
        Console.WriteLine("\nProgram finished.");
    }

    private static void OnStatusChanged(Appliance appliance, RepairStatusChangedEventArgs e)
    {
        Console.WriteLine($"Status event: {appliance.SerialNumber}: {e.OldStatus} -> {e.NewStatus} at {e.ChangedAt:T}");
    }
}
