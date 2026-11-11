namespace RepairCenter;

public static class RepairCenterStats
{
    public static int TotalDevices { get; private set; }
    public static int CompletedDevices { get; private set; }
    public static int DelayedDevices { get; private set; }
    public static decimal TotalRepairCost { get; private set; }

    public static void RegisterDevice() => TotalDevices++;
    public static void RegisterCompletion() => CompletedDevices++;
    public static void RegisterDelay() => DelayedDevices++;
    public static void AddRepairCost(decimal cost)
    {
        if (cost < 0) throw new ArgumentOutOfRangeException(nameof(cost), "Repair cost cannot be negative.");
        TotalRepairCost += cost;
    }
}
