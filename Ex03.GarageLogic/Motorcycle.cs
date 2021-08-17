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
            Aa = 2,
            B1 = 3,
            Bb = 4
        }

        private const int k_NumOfWheels = 2;
        private const int k_MaxAirPressureForTire = 30;
        private const float k_MaxFuel = 6;
        private const float k_MaxEnergyBattery = 1.8f;

        private  eLicenseType m_LicenseType;
        private int m_EngineCapacity;

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
                m_LicenseType = i_LicenseType;
                m_EngineCapacity = i_EngineCapacity;
                InitializeWheels(k_NumOfWheels, i_WheelsManufacturer, i_CurrentWheelAirPressure, k_MaxAirPressureForTire);
                InitializeEngine(i_EngineType, k_MaxEnergyBattery, k_MaxFuel, FuelEngine.eFuelType.Octan98);
            }

        public override string ToString()
        {
            string motorcycleData = string.Format(@"{0}
There are {1} wheels, the license Type is {2} and the engine capacity is {3}.",
                GetVehicleData(),
                (int)eNumberOfWheels.Motorcycle,
                m_LicenseType.ToString(),
                m_EngineCapacity);

            return motorcycleData;
        }
    }
}
