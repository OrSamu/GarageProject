using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleDetails> r_ExistingVehicles;
        public Garage()
        {
            r_ExistingVehicles = new Dictionary<string, VehicleDetails>();
        }
        public bool AddVehicle(string i_LicenseNumber, string i_Owner_Name, string i_Owner_Number){
            bool isThisVehicleExist = isVihicleExistInGarage(i_LicenseNumber);
            if (isThisVehicleExist)
            {
                VehicleDetails vehicleDetails = new VehicleDetails(i_Owner_Name,i_Owner_Number);
                r_ExistingVehicles.Add(i_LicenseNumber, vehicleDetails);
            }
            return isThisVehicleExist;

        }
        private bool isVihicleExistInGarage(string i_LicenseNumber)
        {
            bool isVehicleExist = false;
            foreach (string vehicleNumber in r_ExistingVehicles.Keys)
            {
                if (vehicleNumber.Equals(i_LicenseNumber)){
                    isVehicleExist = true;
                    break;
                }
            }
            return isVehicleExist;
        }

        public void SetVehicleStatus(string i_LicenseNumber, int status)
        {
            if (isVihicleExistInGarage(i_LicenseNumber))
            {
                r_ExistingVehicles[i_LicenseNumber].State=status; 
            }
        }

        public List<string> GetVehiclesByFilter(int filer)
        {
            List<string> vehicalsToSend = new List<string>();

            foreach (string vehicleNumber in r_ExistingVehicles.Keys)
                {

                if (filer == -1)
                {
                vehicalsToSend.Add(vehicleNumber);
                }
                else if (filer == (int)VehicleDetails.eVehicleStates.Paid)
                {
                    if (r_ExistingVehicles[vehicleNumber].State == (int)VehicleDetails.eVehicleStates.Paid)
                    {
                        vehicalsToSend.Add(vehicleNumber);
                    }
                }
                else if (filer == (int)VehicleDetails.eVehicleStates.Repaired)
                {
                    if (r_ExistingVehicles[vehicleNumber].State == (int)VehicleDetails.eVehicleStates.Repaired)
                    {
                        vehicalsToSend.Add(vehicleNumber);
                    }
                }
                else if (filer == (int)VehicleDetails.eVehicleStates.Repairing)
                {
                    if (r_ExistingVehicles[vehicleNumber].State == (int)VehicleDetails.eVehicleStates.Repairing)
                    {
                        vehicalsToSend.Add(vehicleNumber);
                    }
                }


            }
            return vehicalsToSend;
        }
    }
    
}
