namespace RepairCenter
{
    public class Washer : Appliance
    {
        public Washer(
            string serialNumber,
            string customerName,
            string deliveryLocation)
            : base(serialNumber, customerName, deliveryLocation)
        {
        }

        public override decimal CalculateRepairCost()
        {
            return 120m;
        }
    }
}