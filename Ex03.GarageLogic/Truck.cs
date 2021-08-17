﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private enum eQualificationsIndexForTruck
        {
            DangerousTruck = 4,
            MaximumWeight = 5
        }

        private const int k_NumOfWheels = 16;
        private const int k_MaxAirPressureForTire = 26;
        private const string k_Yes = "1";
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

        public new virtual List<string> GetNeededQualifications()
        {
            List<string> neededQualifications = base.GetNeededQualifications();

            neededQualifications.Add(@"Is your truck's goods dangerous? 1-Yes 2-No: ");
            neededQualifications.Add(@"Whats your truck's maximum load capacity? ");

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
                switch(i_IndexOfString)
                {
                    case (int)eQualificationsIndexForTruck.DangerousTruck:
                        isValidInput = checkIfTruckIsDangerous(i_NeededQualificationToCheck);
                        break;
                    case (int)eQualificationsIndexForTruck.MaximumWeight:
                        isValidInput = checkMaximumCapacity(i_NeededQualificationToCheck);
                        break;
                }
            }

            return isValidInput;

        }

        private bool checkIfTruckIsDangerous(string i_StringToCheck)
        {
            int stringToInt;
            bool isValidToParse = int.TryParse(i_StringToCheck, out stringToInt);

            if (!isValidToParse)
            {
                throw new FormatException("Failed parse to int");
            }

            if(stringToInt != 1 && stringToInt != 2)
            {
                //throw new ValueOutOfRangeException(2, 1);
            }

            return isValidToParse;
        }

        private bool checkMaximumCapacity(string i_StringToCheck)
        {
            float stringToFloat;
            bool isValidToParse = float.TryParse(i_StringToCheck, out stringToFloat);

            if (!isValidToParse)
            {
                throw new FormatException("Failed parse to float");
            }

            if (stringToFloat<0)
            {
                //throw new ValueOutOfRangeException(999999999, 0);
            }

            return isValidToParse;
        }

        public new virtual void SetNeededQualifications(List<string> i_NeededQualifications)
        {
            string manufacturerName = i_NeededQualifications[(int)eQualificationsIndex.WheelManufacturerName];
            float currentAirPressure =
                float.Parse(i_NeededQualifications[(int)eQualificationsIndex.CurrentWheelAirPressure]);

            base.SetNeededQualifications(i_NeededQualifications);
            InitializeWheels(k_NumOfWheels, manufacturerName, currentAirPressure, k_MaxAirPressureForTire);
            m_IsDangerous = i_NeededQualifications[(int)eQualificationsIndexForTruck.DangerousTruck] == k_Yes;
            m_MaxWeight = float.Parse(i_NeededQualifications[(int)eQualificationsIndexForTruck.MaximumWeight]);
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
