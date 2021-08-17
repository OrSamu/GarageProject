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

        public static Vehicle CreateVehicle(eVehicleTypes i_VehicleType)
        {
            Vehicle newVehicle = null;

            switch(i_VehicleType)
            {
                case eVehicleTypes.FuelMotorcycle:
                    //newVehicle=new Motorcycle()
                    break;
                case eVehicleTypes.ElectricMotorcycle:
                    //newVehicle=new Motorcycle()
                    break;
                case eVehicleTypes.FuelCar:
                    //newVehicle=new Car()
                    break;
                case eVehicleTypes.ElectricCar:
                    //newVehicle=new Car()
                    break;
                case eVehicleTypes.Truck:
                    //newVehicle=new Truck()
                    break;
            }

            return newVehicle;
        }

    }
}
