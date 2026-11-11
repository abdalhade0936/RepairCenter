using System;

namespace RepairCenter
{
    public delegate void RepairStatusChangedHandler(
        Appliance appliance,
        RepairStatus oldStatus,
        RepairStatus newStatus
    );

    public abstract class Appliance : IRepairable
    {
        private string _customerName;
        private string _deliveryLocation;

        public string SerialNumber { get; }

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _customerName = value;
            }
        }

        public string DeliveryLocation
        {
            get { return _deliveryLocation; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _deliveryLocation = value;
            }
        }

        public DateTime ReceiveDate { get; }

        public RepairStatus Status { get; private set; }

        public event RepairStatusChangedHandler? StatusChanged;

        protected Appliance(
            string serialNumber,
            string customerName,
            string deliveryLocation)
        {
            SerialNumber = serialNumber;
            _customerName = customerName;
            _deliveryLocation = deliveryLocation;
            ReceiveDate = DateTime.Now;
            Status = RepairStatus.Received;
        }

        private void ChangeStatus(RepairStatus newStatus)
        {
            RepairStatus oldStatus = Status;
            Status = newStatus;

            StatusChanged?.Invoke(this, oldStatus, newStatus);
        }

        public void ReceiveDevice()
        {
            ChangeStatus(RepairStatus.Received);
        }

        public void StartRepair()
        {
            ChangeStatus(RepairStatus.UnderRepair);
        }

        public void CompleteRepair()
        {
            ChangeStatus(RepairStatus.Completed);
        }

        public void MarkAsDelayed()
        {
            ChangeStatus(RepairStatus.Delayed);
        }

        public abstract decimal CalculateRepairCost();
    }
}