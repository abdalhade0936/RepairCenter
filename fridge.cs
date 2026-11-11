namespace RepairCenter
{
    public class Fridge : Appliance
    {
        public Fridge(
            string serialNumber,
            string customerName,
            string deliveryLocation)
            : base(serialNumber, customerName, deliveryLocation)
        {
        }

        public override decimal CalculateRepairCost()
        {
            return 150m;
        }
    }
}