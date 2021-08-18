using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleDetails
    {
        public enum eVehicleStates
        {
            Repairing = 1,
            Repaired = 2,
            Paid = 3
        }
        private readonly string r_Owner_Name;
        private readonly string r_Owner_Number;
        private int m_State= (int)eVehicleStates.Repairing;
        public VehicleDetails(string i_Owner_Name, string i_Owner_Number)
        {
            r_Owner_Name = i_Owner_Name;
            r_Owner_Number = i_Owner_Number;
        }
        public int State
        {
            get { return (int)m_State; }
            set { m_State = value; }
        }
    }
}
