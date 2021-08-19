using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public static class VehicaleMaker
    {
        public enum eVehicleTypes
        {
            FuelMotorcycle = 1,
            ElectricMotorcycle = 2,
            FuelCar = 3,
            ElectricCar = 4,
            Truck = 5
        }

        public static Vehicle CreateVehicle(string i_LicenseNumber,int i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch(i_VehicleType)
            {
                case (int)eVehicleTypes.FuelMotorcycle:
                    newVehicle = new Motorcycle(
                        i_LicenseNumber,
                        Engine.eEngineType.Fuel,
                        Motorcycle.k_MaxFuel);
                    break;
                case (int)eVehicleTypes.ElectricMotorcycle:
                    newVehicle = new Motorcycle(
                        i_LicenseNumber,
                        Engine.eEngineType.Electric,
                        Motorcycle.k_MaxEnergyBattery);
                    break;
                case (int)eVehicleTypes.FuelCar:
                    newVehicle = new Car(
                        i_LicenseNumber,
                        Engine.eEngineType.Fuel,
                        Car.k_MaxFuel);
                    break;
                case (int)eVehicleTypes.ElectricCar:
                    newVehicle = new Car(
                        i_LicenseNumber,
                        Engine.eEngineType.Electric,
                        Car.k_MaxEnergyBattery);
                    break;
                case (int)eVehicleTypes.Truck:
                    newVehicle = new Truck(
                        i_LicenseNumber,
                        Engine.eEngineType.Fuel,
                        Truck.k_MaxFuel);
                    break;
            }

            return newVehicle;
        }

    }
}
