using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            m_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void InflateWheel(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd + m_CurrentAirPressure > r_MaxAirPressure)
            {
                //throw new ValueOutOfRangeException(r_MaxAirPressure, 0);
            }

            m_CurrentAirPressure += i_AirPressureToAdd;
        }
    }
}
