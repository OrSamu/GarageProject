using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {

        public enum eFuelType
        {
            Soler = 1,
            Octan95 = 2,
            Octan96 = 3,
            Octan98 = 4
        }

        private readonly eFuelType m_FuelType;

        public FuelEngine(float i_MaxFuelCapacity, eFuelType i_FuelType) : base(i_MaxFuelCapacity)
        {
            m_FuelType = i_FuelType;
        }

        public override void FillEnergy(float i_EnergyToAdd)
        {
            if (i_EnergyToAdd < 0)
            {
                //throw new ArgumentException("Negative number entered");
            }

            fillFuelTank(i_EnergyToAdd);
        }

        private void fillFuelTank(float i_AmountOfFuelToAdd)
        {
            if (m_CurrentEnergy + i_AmountOfFuelToAdd > r_MaxEnergyCapacity)
            {
                //throw new ValueOutOfRangeException(r_MaxEnergyCapacity, 0);
            }

            m_CurrentEnergy += i_AmountOfFuelToAdd;
        }

        public override string ToString()
        {
            string engineData = string.Format(@"Fuel type is {0}, current amount of fuel is {1} liters and the max fuel capacity is {2} liters.",
                m_FuelType.ToString(),
                CurrentEnergy,
            MaxEnergyCapacity
            );

            return engineData;
        }
    }
}