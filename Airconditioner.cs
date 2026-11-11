namespace RepairCenter;

public class AirConditioner : Appliance
{
    public AirConditioner(string serialNumber, string customerName, string deliveryLocation)
        : base(serialNumber, customerName, deliveryLocation) { }
    public override decimal CalculateRepairCost() => 200m;
}
