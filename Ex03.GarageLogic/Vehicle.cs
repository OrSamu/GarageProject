using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Vehicle
    {
        public enum eNumberOfWheels
        {
            Motorcycle = 2,
            Car = 4,
            Truck = 16
        }

        protected readonly string r_LicenseNumber;
        protected readonly string r_Model;
        protected float m_CurrentEnergyPercentage;
        protected List<Wheel> m_Wheels;
        protected Engine m_Engine;

        protected Vehicle(
            string i_LicenseNumber,
            string i_ModelName,
            float i_CurrentEnergyPercentage)
        {
            r_LicenseNumber = i_LicenseNumber;
            r_Model = i_ModelName;
            m_CurrentEnergyPercentage = i_CurrentEnergyPercentage;
        }

        protected void InitializeWheels(int i_NumOfWheels, string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_Wheels = new List<Wheel>(i_NumOfWheels);
            Wheel wheelToAdd;

            for(int i = 0; i < i_NumOfWheels; i++)
            {
                wheelToAdd = new Wheel(i_ManufacturerName, i_CurrentAirPressure, i_MaxAirPressure);
                m_Wheels.Add(wheelToAdd);
            }
        }

        protected void InitializeEngine(Engine.eEngineType i_EngineType, float i_MaxEnergy,float i_MaxFuel,FuelEngine.eFuelType i_FuelType)
        {
            if (i_EngineType == Engine.eEngineType.Electric)
            {
                m_Engine = new ElectricEngine(i_MaxEnergy);
            }
            else
            {
                m_Engine = new FuelEngine(i_MaxFuel, i_FuelType);
            }
        }


    }
}
