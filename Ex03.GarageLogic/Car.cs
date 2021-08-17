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

        private enum eQualificationsIndexForCar
        {
            Color = 4,
            Doors = 5
        }

        private const int k_NumOfWheels = 4;
        private const int k_MaxAirPressureForTire = 32;
        private const float k_MaxFuel = 45;
        private const float k_MaxEnergyBattery = 3.2f;


        private eNumberOfDoors m_Doors;
        private eColor m_Color;

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
            m_Doors = i_DoorsNum;
            m_Color = i_CarColor;
            InitializeWheels(k_NumOfWheels, i_WheelsManufacturer, i_CurrentWheelAirPressure, k_MaxAirPressureForTire);
            InitializeEngine(i_EngineType, k_MaxEnergyBattery, k_MaxFuel, FuelEngine.eFuelType.Octan95);
        }

        public new virtual List<string> GetNeededQualifications()
        {
            List<string> neededQualifications = base.GetNeededQualifications();

            neededQualifications.Add(@"What is your car's color?
1-Red 2-White 3-Black 4-Silver: ");
            neededQualifications.Add(@"How many doors your car has?
1-Two 2-Three 3-Four 4-Five: ");

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
                    case (int)eQualificationsIndexForCar.Doors:
                        isValidInput = CheckIfEnumDefined<eNumberOfDoors>(i_NeededQualificationToCheck);
                        break;
                    case (int)eQualificationsIndexForCar.Color:
                        isValidInput = CheckIfEnumDefined<eColor>(i_NeededQualificationToCheck);
                        break;
                }
            }

            return isValidInput;
        }
        
        public new virtual void SetNeededQualifications(List<string> i_NeededQualifications)
        {
            string manufacturerName = i_NeededQualifications[(int)eQualificationsIndex.WheelManufacturerName];
            float currentAirPressure =
                float.Parse(i_NeededQualifications[(int)eQualificationsIndex.CurrentWheelAirPressure]);
            eNumberOfDoors doorsInput =
                (eNumberOfDoors)int.Parse(i_NeededQualifications[(int)eQualificationsIndexForCar.Doors]);
            eColor colorInput =
                (eColor)int.Parse(i_NeededQualifications[(int)eQualificationsIndexForCar.Color]);

            base.SetNeededQualifications(i_NeededQualifications);
            InitializeWheels(k_NumOfWheels, manufacturerName, currentAirPressure, k_MaxAirPressureForTire);
            m_Doors = doorsInput;
            m_Color = colorInput;
        }

        public override string ToString()
        {
            string carData = string.Format(
                @"{0}
There are {1} wheels, the car has '{2}' doors and the color is '{3}'.",
                GetVehicleData(),
                (int)eNumberOfWheels.Car,
                m_Doors.ToString(),
                m_Color.ToString());
            return carData;
        }
    }
}
