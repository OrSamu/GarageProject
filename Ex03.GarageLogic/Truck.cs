using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {

        private const int k_NumOfWheels = 16;
        private const int k_MaxAirPressureForTire = 26;
        private const float k_MaxFuel = 120;
        private const float k_MaxEnergyBattery = -1f;

        private bool m_IsDangerous;
        private float m_MaxWeight;

        public Truck(string i_LicenseNumber,
                     string i_Model,
                     float i_CurrentEnergyPercentage,
                     string i_WheelsManufacturer,
                     float i_CurrentWheelAirPressure,
                     bool i_IsDangerousTruck,
                     float i_MaxWeightToDeliver
                     )
            : base(i_LicenseNumber, i_Model, i_CurrentEnergyPercentage)
        {
            m_IsDangerous = i_IsDangerousTruck;
            m_MaxWeight = i_MaxWeightToDeliver;
            InitializeWheels(k_NumOfWheels, i_WheelsManufacturer, i_CurrentWheelAirPressure, k_MaxAirPressureForTire);
            InitializeEngine(Engine.eEngineType.Fuel, k_MaxEnergyBattery, k_MaxFuel, FuelEngine.eFuelType.Soler);
        }

        public override string ToString()
        {
            string isDangerousTruck = m_IsDangerous ? "are" : "aren't";
            string truckData = string.Format(@"{0}
There are {1} wheels, the truck's goods {2} dangerous and the load Capacity is {3}.",
                GetVehicleData(),
                (int)eNumberOfWheels.Truck,
                isDangerousTruck,
                m_MaxWeight);

            return truckData;
        }
    }
}
