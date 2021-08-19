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
        private Vehicle m_Vehicle;

        public VehicleDetails(string i_Owner_Name, string i_Owner_Number, Vehicle i_Vehicle)
        {
            r_Owner_Name = i_Owner_Name;
            r_Owner_Number = i_Owner_Number;
            m_Vehicle = i_Vehicle;
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
        }
        public int State
        {
            get { return (int)m_State; }
            set { m_State = value; }
        }

        private string checkStatus()
        {
            string status = "";
            switch (m_State)
            {
                case (int)eVehicleStates.Repairing:
                    status = "Repairing";
                    break;
                case (int)eVehicleStates.Repaired:
                    status = "Repirad";
                    break;
                case (int)eVehicleStates.Paid:
                    status = "Paid";
                    break;
            }

            return status;
        }

        public string ToString()
        {
            string status;
            string stringToReturn;

            status = checkStatus();
            stringToReturn = string.Format(@"Owner name is {0}, Owner number is {1} and the status of the vehicle is {2}.
Vehicle details are:
{3}"
                , r_Owner_Name
                , r_Owner_Number
                , status
                , m_Vehicle.ToString());

            return stringToReturn;

        }
    }
}
