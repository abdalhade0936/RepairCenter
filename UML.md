# UML overview

```text
                 <<interface>> IRepairable
                 + ReceiveDevice()
                 + StartRepair()
                 + MarkAsDelayed()
                 + CompleteRepair()
                 + CalculateRepairCost()
                           ^
                           |
                    Appliance (abstract)
                    - SerialNumber
                    - CustomerName
                    - DeliveryLocation
                    - Status
                    + StatusChanged event
                      ^       ^        ^
                      |       |        |
                    Fridge  Washer  AirConditioner

RepairCenter ---- manages ----> Appliance (0..*)
RepairCenter ---- uses -------> RepairCenterStats
Appliance ------ raises ------> RepairStatusChangedEventArgs
Program -------- creates -----> RepairCenter and appliance objects
```
