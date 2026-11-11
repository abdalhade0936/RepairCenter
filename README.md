# Home Appliance Repair Center

A C# console application demonstrating object-oriented programming for a small appliance repair center.

## Requirements
- .NET SDK 8.0

## Build and run
From this directory:

```bash
dotnet build
dotnet run
```

## Main concepts
- `IRepairable` defines the repair operations.
- `Appliance` is an abstract base class that encapsulates shared appliance data and repair status.
- `Fridge`, `Washer`, and `AirConditioner` override `CalculateRepairCost()` (polymorphism).
- `RepairCenter` registers appliances, prevents duplicate serial numbers, and provides reports.
- `RepairStatusChangedHandler` and `StatusChanged` demonstrate delegates and events.
- `RepairStatusChangedEventArgs` carries the old and new statuses and the event time.
- `RepairValidator` validates input data.

## Team contribution
Complete the contribution table with the real team members and their actual Git commit counts before submission. Do not invent names or commit totals.

| Team member | Contribution | Number of commits |
|---|---|---|
| [Name] | [Actual contribution] | [Actual count] |
