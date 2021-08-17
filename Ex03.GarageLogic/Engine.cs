using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        public enum eEngineType
        {
            Fuel = 1,
            Electric = 2
        }

        protected readonly float r_MaxEnergyCapacity;
        protected float m_CurrentEnergy;


        protected Engine(float i_MaxEnergyCapacity)
        {
            r_MaxEnergyCapacity = i_MaxEnergyCapacity;
            m_CurrentEnergy = 0;
        }

        public float MaxEnergyCapacity
        {
            get { return r_MaxEnergyCapacity; }
        }

        public float CurrentEnergy
        {
            get { return m_CurrentEnergy; }
            //set { m_CurrentEnergy = value; }
        }

        public bool IsEngineEnergyFull()
        {
            bool result = m_CurrentEnergy == r_MaxEnergyCapacity;
            
            return result;
        }

        public abstract void FillEnergy(float i_EnergyToAdd);

        public abstract override string ToString();
    }
}
