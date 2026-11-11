namespace RepairCenter;

/// <summary>Coordinates appliance registration, repair workflow, and summary reporting.</summary>
public sealed class RepairCenter
{
    private readonly List<Appliance> _appliances = new();
    public IReadOnlyList<Appliance> Appliances => _appliances.AsReadOnly();

    public bool Register(Appliance appliance)
    {
        ArgumentNullException.ThrowIfNull(appliance);
        if (_appliances.Any(x => string.Equals(x.SerialNumber, appliance.SerialNumber, StringComparison.OrdinalIgnoreCase)))
            return false;
        _appliances.Add(appliance);
        RepairCenterStats.RegisterDevice();
        appliance.StatusChanged += HandleStatusChanged;
        return true;
    }

    public Appliance? FindBySerialNumber(string serialNumber) =>
        _appliances.FirstOrDefault(x => string.Equals(x.SerialNumber, serialNumber, StringComparison.OrdinalIgnoreCase));

    public decimal GetTotalEstimatedCost() => _appliances.Sum(x => x.CalculateRepairCost());
    public IEnumerable<Appliance> GetDelayedDevices() => _appliances.Where(x => x.WasDelayed);
    public IEnumerable<Appliance> GetRepairedDevices() => _appliances.Where(x => x.Status == RepairStatus.Repaired);

    private static void HandleStatusChanged(Appliance appliance, RepairStatusChangedEventArgs e)
    {
        if (e.NewStatus == RepairStatus.Delayed) RepairCenterStats.RegisterDelay();
        if (e.NewStatus == RepairStatus.Repaired) RepairCenterStats.RegisterCompletion();
    }
}
