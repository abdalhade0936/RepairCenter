namespace RepairCenter;

public interface IRepairable
{
    void ReceiveDevice();
    void StartRepair();
    void MarkAsDelayed();
    void CompleteRepair();
    decimal CalculateRepairCost();
}
