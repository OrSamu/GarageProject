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

        private enum eQualificationsIndexForMotorcycle
        {
            License = 4,
            EngineCapacity = 5
        }

        private const int k_NumOfWheels = 2;
        private const int k_MaxAirPressureForTire = 30;
        internal const float k_MaxFuel = 6;
        internal const float k_MaxEnergyBattery = 1.8f;

        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

       
        public Motorcycle(string i_LicenseNumber,
                                    string i_Model,
                                    Engine.eEngineType i_EngineType,
                                    float i_MaxEnergy,
                                    float i_CurrentEnergy)
            : base(i_LicenseNumber,i_Model,i_CurrentEnergy, i_MaxEnergy, i_EngineType)
        {
        }
        

        public new virtual List<string> GetNeededQualifications()
        {
            List<string> neededQualifications = base.GetNeededQualifications();

            neededQualifications.Add(@"What is your motorcycle license type?
1-A 2-AA 3-B1 4-BB: ");
            neededQualifications.Add(@"What is your engine capacity? ");

            return neededQualifications;
        }

        public new virtual bool CheckNeededQualifications(string i_NeededQualificationToCheck, int i_IndexOfString)
        {
            bool isValidInput = false;

            if (i_IndexOfString < 4)
            {
                isValidInput = base.CheckNeededQualifications(i_NeededQualificationToCheck, i_IndexOfString);
            }
            else
            {
                switch (i_IndexOfString)
                {
                    case (int)eQualificationsIndexForMotorcycle.License:
                        isValidInput = CheckIfEnumDefined<eLicenseType>(i_NeededQualificationToCheck);
                        break;
                    case (int)eQualificationsIndexForMotorcycle.EngineCapacity:
                        isValidInput = checkEngineCapacity(i_NeededQualificationToCheck);
                        break;
                }
            }

            return isValidInput;
        }

        private bool checkEngineCapacity(string i_StringToCheck)
        {
            int stringToInt;
            bool isValidToParse = int.TryParse(i_StringToCheck, out stringToInt);

            if (!isValidToParse)
            {
                throw new FormatException("Failed parse to int");
            }

            if (stringToInt < 0)
            {
                throw new ValueOutOfRangeException(999999999, 0);
            }

            return isValidToParse;
        }

        public new virtual void SetNeededQualifications(List<string> i_NeededQualifications)
        {
            FuelEngine fuelEngine = m_Engine as FuelEngine;
            string manufacturerName = i_NeededQualifications[(int)eQualificationsIndex.WheelManufacturerName];
            float currentAirPressure =
                float.Parse(i_NeededQualifications[(int)eQualificationsIndex.CurrentWheelAirPressure]);
            eLicenseType licenseInput =
                (eLicenseType)int.Parse(i_NeededQualifications[(int)eQualificationsIndexForMotorcycle.License]);
            int engineCapacityInput =
                int.Parse(i_NeededQualifications[(int)eQualificationsIndexForMotorcycle.EngineCapacity]);
            
            if (fuelEngine != null)
            {
                fuelEngine.FuelType = FuelEngine.eFuelType.Octan98;
            }

            

            base.SetNeededQualifications(i_NeededQualifications);
            InitializeWheels(k_NumOfWheels, manufacturerName, currentAirPressure, k_MaxAirPressureForTire);
            m_LicenseType = licenseInput;
            m_EngineCapacity = engineCapacityInput;
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
