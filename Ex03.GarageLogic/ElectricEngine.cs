using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {

        private const int k_MinutesInHour = 60;
        public ElectricEngine(float i_MaxBatteryCapacity) : base(i_MaxBatteryCapacity)
        {
        }

        public override void FillEnergy(float i_MinutesToCharge)
        {
            if (i_MinutesToCharge < 0)
            {
                //throw new ArgumentException("Negative number entered");
            }

            fillBatery(i_MinutesToCharge / k_MinutesInHour);
        }

        private void fillBatery(float i_HoursToAdd)
        {
            if (m_CurrentEnergy + i_HoursToAdd > r_MaxEnergyCapacity)
            {
                //throw new ValueOutOfRangeException(r_MaxEnergyCapacity, 0);
            }

            m_CurrentEnergy += i_HoursToAdd;
        }

        public override string ToString()
        {
            string engineData = string.Format(@"Current amount of energy is {0} hours and the max energy capacity is {1} hours.",
                CurrentEnergy,
                MaxEnergyCapacity);

            return engineData;
        }

    }
}
