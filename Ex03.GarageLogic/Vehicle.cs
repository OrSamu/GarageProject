using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        public enum eNumberOfWheels
        {
            Motorcycle = 2,
            Car = 4,
            Truck = 16
        }

        public enum eQualificationsIndex
        {
            ModelName = 0,
            CurrentEnergyAmount = 1,
            WheelManufacturerName = 2,
            CurrentWheelAirPressure = 3
        }

        protected readonly string r_LicenseNumber;
        protected string m_Model;
        protected float m_CurrentEnergyPercentage;
        protected List<Wheel> m_Wheels;
        protected Engine m_Engine;

        protected Vehicle(
            string i_LicenseNumber,
            string i_ModelName,
            float i_CurrentEnergyPercentage,
            Engine.eEngineType i_EngineType)
        {
            r_LicenseNumber = i_LicenseNumber;
            m_Model = i_ModelName;
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

        public void InflateWheelsToMax()
        {
            float missingAirPressure = 0;

            foreach (Wheel wheel in m_Wheels)
            {
                missingAirPressure = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                if (missingAirPressure != 0)
                {
                    wheel.InflateWheel(missingAirPressure);
                }
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

        public virtual List<string> GetNeededQualifications()
        {
            List<string> neededQualifications = new List<string>
                                                     {
                                                         "Model name: ",
                                                         "Current fuel/energy amount: ",
                                                         "Wheel manufacturer: ",
                                                         "Current wheel air pressure: "
                                                         //Engine type ?
                                                     };

            return neededQualifications;
        }

        public virtual bool CheckNeededQualifications(string i_NeededQualificationToCheck, int i_IndexOfString)
        {
            bool isQualificationValid = false;


            switch(i_IndexOfString)
            {
                case (int)eQualificationsIndex.ModelName:
                case (int)eQualificationsIndex.WheelManufacturerName:
                    isQualificationValid = checkStringNotEmpty(i_NeededQualificationToCheck);
                    break;
                case (int)eQualificationsIndex.CurrentEnergyAmount:
                    isQualificationValid = checkCurrentEnergy(i_NeededQualificationToCheck);
                    break;
                case (int)eQualificationsIndex.CurrentWheelAirPressure:
                    isQualificationValid = checkCurrentWheelAirPressures(i_NeededQualificationToCheck);
                    break;
            }

            return isQualificationValid;
        }

        private bool checkStringNotEmpty(string i_StringToCheck)
        {
            if (string.IsNullOrEmpty(i_StringToCheck))
            {
                throw new ArgumentException("You have entered an empty string - try again");
            }

            return true;
        }

        private bool checkCurrentWheelAirPressures(string i_StringToCheck)
        {
            float stringToFloat;
            bool isValidToParse = float.TryParse(i_StringToCheck, out stringToFloat);

            if (!isValidToParse)
            {
                throw new FormatException("Failed parse to float");
            }

            float maxAirPressure = this.m_Wheels[0].MaxAirPressure;

            if (stringToFloat > maxAirPressure || stringToFloat < 0)
            {
                //throw new ValueOutOfRangeException((float)maxAirPressure, 0);
            }

            return true;
        }

        private bool checkCurrentEnergy(string i_StringToCheck)
        {
            float stringToFloat;
            bool isValidToParse = float.TryParse(i_StringToCheck, out stringToFloat);

            if (!isValidToParse)
            {
                throw new FormatException("Failed parse to float");
            }

            float maxEnergy = this.m_Engine.MaxEnergyCapacity;

            if (stringToFloat > maxEnergy || stringToFloat < 0)
            {
                //throw new ValueOutOfRangeException((float)maxAirPressure, 0);
            }

            return true;
        }

        public virtual void SetNeededQualifications(List<string> i_NeededQualifications)
        {
            m_Model = i_NeededQualifications[(int)eQualificationsIndex.ModelName];
            m_Engine.CurrentEnergy = float.Parse(i_NeededQualifications[(int)eQualificationsIndex.CurrentEnergyAmount]);
            m_CurrentEnergyPercentage = m_Engine.CurrentEnergy / m_Engine.MaxEnergyCapacity * 100;
        }

        public bool CheckIfEnumDefined<T>(string i_EnumToCheck)
        {
            int enumInInt = int.Parse(i_EnumToCheck);
            bool isValidEnumm = !Enum.IsDefined(typeof(T), enumInInt);

            if(isValidEnumm)
            {
                throw new ValueOutOfRangeException(Enum.GetValues(typeof(T)).Length, 1);
            }

            return isValidEnumm;
        }


        protected string GetVehicleData()
        {
            string vehicleData = string.Format(
                @"License Number is {0}, model name is {1} and the energy percentage left is {2}%.
{3}
{4} ",
                r_LicenseNumber,
                m_Model,
                m_CurrentEnergyPercentage,
                m_Engine.ToString(),
                m_Wheels[0].ToString());

            
            return vehicleData;
        }
    }
}
