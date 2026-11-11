namespace RepairCenter;

public static class RepairValidator
{
    public static bool IsValidSerialNumber(string? serialNumber) => !string.IsNullOrWhiteSpace(serialNumber) && serialNumber.Trim().Length >= 3;
    public static bool IsValidCustomerName(string? customerName) => !string.IsNullOrWhiteSpace(customerName) && customerName.Trim().Length >= 2;
    public static bool IsValidDeliveryLocation(string? location) => !string.IsNullOrWhiteSpace(location);

    public static bool IsValidAppliance(string? serialNumber, string? customerName, string? location) =>
        IsValidSerialNumber(serialNumber) && IsValidCustomerName(customerName) && IsValidDeliveryLocation(location);
}
