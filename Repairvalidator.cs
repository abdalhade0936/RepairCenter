namespace RepairCenter
{
    public static class RepairValidator
    {
        public static bool IsValidSerialNumber(string serialNumber)
        {
            return !string.IsNullOrWhiteSpace(serialNumber);
        }

        public static bool IsValidCustomerName(string customerName)
        {
            return !string.IsNullOrWhiteSpace(customerName);
        }

        public static bool IsValidDeliveryLocation(string location)
        {
            return !string.IsNullOrWhiteSpace(location);
        }

        public static bool IsValidAppliance(
            string serialNumber,
            string customerName,
            string location)
        {
            return IsValidSerialNumber(serialNumber)
                && IsValidCustomerName(customerName)
                && IsValidDeliveryLocation(location);
        }
    }
    }