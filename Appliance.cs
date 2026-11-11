using System;

namespace RepairCenter;

public delegate void RepairStatusChangedHandler(Appliance appliance, RepairStatusChangedEventArgs e);

public abstract class Appliance : IRepairable
{
    private string _customerName;
    private string _deliveryLocation;

    public string SerialNumber { get; }
    public string CustomerName
    {
        get => _customerName;
        set
        {
            EnsureNotCompleted();
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Customer name is required.", nameof(value));
            _customerName = value.Trim();
        }
    }
    public string DeliveryLocation
    {
        get => _deliveryLocation;
        set
        {
            EnsureNotCompleted();
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Delivery location is required.", nameof(value));
            _deliveryLocation = value.Trim();
        }
    }
    public DateTime ReceiveDate { get; }
    public DateTime? CompletionDate { get; private set; }
    public RepairStatus Status { get; private set; }
    public bool WasDelayed { get; private set; }

    public event RepairStatusChangedHandler? StatusChanged;

    protected Appliance(string serialNumber, string customerName, string deliveryLocation)
    {
        if (string.IsNullOrWhiteSpace(serialNumber)) throw new ArgumentException("Serial number is required.", nameof(serialNumber));
        if (string.IsNullOrWhiteSpace(customerName)) throw new ArgumentException("Customer name is required.", nameof(customerName));
        if (string.IsNullOrWhiteSpace(deliveryLocation)) throw new ArgumentException("Delivery location is required.", nameof(deliveryLocation));
        SerialNumber = serialNumber.Trim();
        _customerName = customerName.Trim();
        _deliveryLocation = deliveryLocation.Trim();
        ReceiveDate = DateTime.Now;
        Status = RepairStatus.Received;
    }

    private void EnsureNotCompleted()
    {
        if (Status == RepairStatus.Completed) throw new InvalidOperationException("A completed repair record cannot be changed.");
    }

    private void ChangeStatus(RepairStatus newStatus)
    {
        EnsureNotCompleted();
        if (Status == newStatus) return;
        bool allowed = (Status == RepairStatus.Received && (newStatus == RepairStatus.UnderRepair || newStatus == RepairStatus.Delayed))
            || (Status == RepairStatus.UnderRepair && (newStatus == RepairStatus.Delayed || newStatus == RepairStatus.Completed))
            || (Status == RepairStatus.Delayed && (newStatus == RepairStatus.UnderRepair || newStatus == RepairStatus.Completed));
        if (!allowed) throw new InvalidOperationException($"Cannot change status from {Status} to {newStatus}.");
        if (newStatus == RepairStatus.Delayed) WasDelayed = true;
        if (newStatus == RepairStatus.Completed) CompletionDate = DateTime.Now;
        RepairStatus oldStatus = Status;
        Status = newStatus;
        StatusChanged?.Invoke(this, new RepairStatusChangedEventArgs(oldStatus, newStatus));
    }

    public void ReceiveDevice()
    {
        if (Status != RepairStatus.Received) throw new InvalidOperationException("Device has already moved beyond the received stage.");
    }
    public void StartRepair() => ChangeStatus(RepairStatus.UnderRepair);
    public void MarkAsDelayed() => ChangeStatus(RepairStatus.Delayed);
    public void CompleteRepair() => ChangeStatus(RepairStatus.Completed);
    public abstract decimal CalculateRepairCost();
}
