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
        public void AddVehicle(string i_LicenseNumber, string i_Owner_Name, string i_Owner_Number, Vehicle i_Vehicle){

                VehicleDetails vehicleDetails = new VehicleDetails(i_Owner_Name,i_Owner_Number, i_Vehicle);
                r_ExistingVehicles.Add(i_LicenseNumber, vehicleDetails);

        }

        public Vehicle getVehicleByLicenseNumber(string i_licenseNumber)
        {
            return r_ExistingVehicles[i_licenseNumber].Vehicle;
        }
        public bool IsVihicleExistInGarage(string i_LicenseNumber)
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
            if (IsVihicleExistInGarage(i_LicenseNumber))
            {
                r_ExistingVehicles[i_LicenseNumber].State=status; 
            }
        }

        public List<string> GetVehiclesByFilter(int filer)
        {
            List<string> vehicalsToSend = new List<string>();

            foreach (string vehicleNumber in r_ExistingVehicles.Keys)
                {

                if (filer == 0)
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

        public string GetVehicleData(string i_LicenseNumber)
        {
            VehicleDetails vehicleData;

            vehicleData = r_ExistingVehicles[i_LicenseNumber];

            return vehicleData.ToString();
        }
    }

    
}
