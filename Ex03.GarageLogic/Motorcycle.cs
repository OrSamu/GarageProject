using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {

        public enum eLicenseType
        {
            A = 1,
            AA = 2,
            B1 = 3,
            BB = 4
        }

        private const int k_NumOfWheels = 2;
        private const int k_MaxAirPressureForTire = 30;
        private const float k_MaxFuel = 6;
        private const float k_MaxEnergyBattery = 1.8f;

        private readonly eLicenseType r_LicenseType;
        private readonly int r_EngineCapacity;

        public Motorcycle(string i_LicenseNumber,
                       string i_Model,
                       Engine.eEngineType i_EngineType,
                       float i_CurrentEnergyPercentage,
                       string i_WheelsManufacturer,
                       float i_CurrentWheelAirPressure,
                       eLicenseType i_LicenseType,
                       int i_EngineCapacity)
                : base(i_LicenseNumber, i_Model, i_CurrentEnergyPercentage)
            {
                r_LicenseType = i_LicenseType;
                r_EngineCapacity = i_EngineCapacity;
                InitializeWheels(k_NumOfWheels, i_WheelsManufacturer, i_CurrentWheelAirPressure, k_MaxAirPressureForTire);
                InitializeEngine(i_EngineType, k_MaxEnergyBattery, k_MaxFuel, FuelEngine.eFuelType.Octan98);
            }

        public override string ToString()
        {
            string motorcycleData = string.Format(@"{0}
There are {1} wheels, the license Type is {2} and the engine capacity is {3}.",
                GetVehicleData(),
                (int)eNumberOfWheels.Motorcycle,
                r_LicenseType.ToString(),
                r_EngineCapacity);

            return motorcycleData;
        }
    }
}
