namespace RepairCenter
{
    public static class RepairCenterStats
    {
        public static int TotalDevices { get; private set; }

        public static decimal TotalRepairCost { get; private set; }

        public static void RegisterDevice()
        {
            TotalDevices++;
        }

        public static void AddRepairCost(decimal cost)
        {
            if (cost >= 0)
            {
                TotalRepairCost += cost;
            }
        }
    }
}