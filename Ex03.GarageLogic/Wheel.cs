using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        public enum eMaxAirPressure
        {
            Truck = 26,
            Motorcycle = 30,
            Car = 32
        }

        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }

            //set { m_CurrentAirPressure = value; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string Manufacturer
        {
            get { return m_ManufacturerName; }
        }

        public void InflateWheel(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd + m_CurrentAirPressure > r_MaxAirPressure)
            {
                //throw new ValueOutOfRangeException(r_MaxAirPressure, 0);
            }

            m_CurrentAirPressure += i_AirPressureToAdd;
        }

        public override string ToString()
        {
            string wheelData = string.Format(
                @"Manufacturer is {0}, current air pressure is {1} and max air pressure is {2}",
                m_ManufacturerName,
                m_CurrentAirPressure,
                r_MaxAirPressure);

            return wheelData;
        }


    }
}
