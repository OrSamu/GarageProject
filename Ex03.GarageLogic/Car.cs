using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        public enum eColor
        {
            Red = 1,
            White = 2,
            Black = 3,
            Silver = 4
        }

        public enum eNumberOfDoors
        {
            Two = 1,
            Three = 2,
            Four = 3,
            Five = 4
        }

        private const int k_NumOfWheels = 4;
        private const int k_MaxAirPressureForTire = 32;
        private const float k_MaxFuel = 45;
        private const float k_MaxEnergyBattery = 3.2f;


        private readonly eNumberOfDoors r_Doors;
        private readonly eColor r_Color;

        public Car(string i_LicenseNumber,
                   string i_Model, 
                   Engine.eEngineType i_EngineType, 
                   float i_CurrentEnergyPercentage, 
                   string i_WheelsManufacturer, 
                   float i_CurrentWheelAirPressure,
                   eNumberOfDoors i_DoorsNum,
                   eColor i_CarColor)
            : base(i_LicenseNumber, i_Model, i_CurrentEnergyPercentage)
        {
            r_Doors = i_DoorsNum;
            r_Color = i_CarColor;
            InitializeWheels(k_NumOfWheels, i_WheelsManufacturer, i_CurrentWheelAirPressure, k_MaxAirPressureForTire);
            InitializeEngine(i_EngineType, k_MaxEnergyBattery, k_MaxFuel, FuelEngine.eFuelType.Octan95);
        }

        public override string ToString()
        {
            string carData = string.Format(
                @"{0}
There are {1} wheels, the car has '{2}' doors and the color is '{3}'.",
                GetVehicleData(),
                (int)eNumberOfWheels.Car,
                r_Doors.ToString(),
                r_Color.ToString());
            return carData;
        }
    }
}
