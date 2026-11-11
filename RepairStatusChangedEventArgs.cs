using System;

namespace RepairCenter;

public sealed class RepairStatusChangedEventArgs : EventArgs
{
    public RepairStatus OldStatus { get; }
    public RepairStatus NewStatus { get; }
    public DateTime ChangedAt { get; }

    public RepairStatusChangedEventArgs(RepairStatus oldStatus, RepairStatus newStatus)
    {
        OldStatus = oldStatus;
        NewStatus = newStatus;
        ChangedAt = DateTime.Now;
    }
}
