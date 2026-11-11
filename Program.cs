using System;
using System.Collections.Generic;

namespace RepairCenter
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Appliance> appliances = new List<Appliance>();

            Console.WriteLine("=== Home Appliance Repair Center ===");

            // Create appliances
            Appliance fridge = new Fridge(
                "F101", "Ahmad", "Damascus");

            Appliance washer = new Washer(
                "W102", "Sara", "Aleppo");

            Appliance airConditioner = new AirConditioner(
                "A103", "Omar", "Homs");

            // Validate and register appliances
            Appliance[] devices =
            {
                fridge,
                washer,
                airConditioner
            };

            foreach (Appliance device in devices)
            {
                if (RepairValidator.IsValidAppliance(
                    device.SerialNumber,
                    device.CustomerName,
                    device.DeliveryLocation))
                {
                    appliances.Add(device);
                    RepairCenterStats.RegisterDevice();

                    device.StatusChanged += OnStatusChanged;
                }
            }

            // Start and complete repairs
            foreach (Appliance device in appliances)
            {
                Console.WriteLine("\nDevice: " + device.SerialNumber);

                device.StartRepair();

                decimal cost = device.CalculateRepairCost();

                RepairCenterStats.AddRepairCost(cost);

                Console.WriteLine("Customer: " + device.CustomerName);
                Console.WriteLine("Location: " + device.DeliveryLocation);
                Console.WriteLine("Repair Cost: " + cost);

                device.CompleteRepair();
            }

            // Display statistics
            Console.WriteLine("\n=== Repair Center Statistics ===");

            Console.WriteLine(
                "Total Devices: " +
                RepairCenterStats.TotalDevices);

            Console.WriteLine(
                "Total Repair Cost: " +
                RepairCenterStats.TotalRepairCost);

            Console.WriteLine("\nProgram Finished Successfully!");
        }

        static void OnStatusChanged(
            Appliance appliance,
            RepairStatus oldStatus,
            RepairStatus newStatus)
        {
            Console.WriteLine(
                "Status Changed: " +
                appliance.SerialNumber +
                " | " + oldStatus +
                " -> " + newStatus);
        }
    }
}
