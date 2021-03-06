using System;

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

        private eFuelType m_FuelType;

        public FuelEngine(float i_MaxFuelCapacity)
            : base(i_MaxFuelCapacity)
        {
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
            set
            {
                m_FuelType = value;
            }
        }

        public override void FillEnergy(float i_EnergyToAdd)
        {
            if(i_EnergyToAdd < 0)
            {
                throw new ArgumentException("Negative number entered");
            }

            fillFuelTank(i_EnergyToAdd);
        }

        private void fillFuelTank(float i_AmountOfFuelToAdd)
        {
            if(m_CurrentEnergy + i_AmountOfFuelToAdd > r_MaxEnergyCapacity)
            {
                throw new ValueOutOfRangeException(r_MaxEnergyCapacity, 0);
            }

            m_CurrentEnergy += i_AmountOfFuelToAdd;
        }

        public override string ToString()
        {
            string engineData = string.Format(
                @"Fuel type is {0}, current amount of fuel is {1} liters and the max fuel capacity is {2} liters.",
                m_FuelType.ToString(),
                CurrentEnergy,
                MaxEnergyCapacity);

            return engineData;
        }

        public override int getEngineType()
        {
            return (int)Engine.eEngineType.Fuel;
        }

        public void CheckFuelType(eFuelType i_GasType)
        {
            if(m_FuelType != i_GasType)
            {
                throw new ArgumentException("Incompatible gas type");
            }
        }
    }
}