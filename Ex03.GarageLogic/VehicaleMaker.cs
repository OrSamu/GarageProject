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

        public static Vehicle CreateVehicle(string i_LicenseNumber, string i_Model,float i_CurrentEnergy,int i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch(i_VehicleType)
            {
                case (int)eVehicleTypes.FuelMotorcycle:
                    newVehicle = new Motorcycle(
                        i_LicenseNumber,
                        i_Model,
                        Engine.eEngineType.Fuel,
                        Motorcycle.k_MaxFuel,
                        i_CurrentEnergy);
                    break;
                case (int)eVehicleTypes.ElectricMotorcycle:
                    newVehicle = new Motorcycle(
                        i_LicenseNumber,
                        i_Model,
                        Engine.eEngineType.Electric,
                        Motorcycle.k_MaxEnergyBattery,
                        i_CurrentEnergy);
                    break;
                case (int)eVehicleTypes.FuelCar:
                    newVehicle = new Car(
                        i_LicenseNumber,
                        i_Model,
                        Engine.eEngineType.Fuel,
                        Car.k_MaxFuel,
                        i_CurrentEnergy);
                    break;
                case (int)eVehicleTypes.ElectricCar:
                    newVehicle = new Car(
                        i_LicenseNumber,
                        i_Model,
                        Engine.eEngineType.Electric,
                        Car.k_MaxEnergyBattery,
                        i_CurrentEnergy);
                    break;
                case (int)eVehicleTypes.Truck:
                    newVehicle = new Truck(
                        i_LicenseNumber,
                        i_Model,
                        Engine.eEngineType.Fuel,
                        Truck.k_MaxFuel,
                        i_CurrentEnergy);
                    break;
            }

            return newVehicle;
        }

    }
}
