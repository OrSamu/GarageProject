using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleDetails> r_ExistingVehicles;

        public Garage()
        {
            r_ExistingVehicles = new Dictionary<string, VehicleDetails>();
        }

        public void AddVehicle(string i_LicenseNumber, string i_Owner_Name, string i_Owner_Number, Vehicle i_Vehicle)
        {
            VehicleDetails vehicleDetails = new VehicleDetails(i_Owner_Name, i_Owner_Number, i_Vehicle);
            r_ExistingVehicles.Add(i_LicenseNumber, vehicleDetails);
        }

        public Vehicle GetVehicleByLicenseNumber(string i_licenseNumber)
        {
            return r_ExistingVehicles[i_licenseNumber].Vehicle;
        }

        public bool IsVehicleExistInGarage(string i_LicenseNumber)
        {
            bool isVehicleExist = false;
            foreach(string vehicleNumber in r_ExistingVehicles.Keys)
            {
                if(vehicleNumber.Equals(i_LicenseNumber))
                {
                    isVehicleExist = true;
                    break;
                }
            }

            return isVehicleExist;
        }

        public string FillVehicleTank(string i_LicenseNumber, FuelEngine.eFuelType i_FuelType, float i_AmountOfFuel)
        {
            string operationMsg = " ";
            Vehicle vehicleToFill = this.GetVehicleByLicenseNumber(i_LicenseNumber);
            FuelEngine engineToFill = vehicleToFill.Engine as FuelEngine;

            if(engineToFill != null)
            {
                bool isTheSameFuel = i_FuelType == engineToFill.FuelType;

                if(isTheSameFuel)
                {
                    try
                    {
                        engineToFill.FillEnergy(i_AmountOfFuel);
                        operationMsg = "Fuel has been added successfully";
                    }
                    catch(ArgumentException argumentException)
                    {
                        operationMsg = "Value Error";
                    }
                    catch(ValueOutOfRangeException valueOutOfRangeException)
                    {
                        operationMsg = "That is too much fuel";
                    }
                }
                else
                {
                    operationMsg = "Fuel type isn't a match";
                }
            }
            else
            {
                operationMsg = "This vehicle doesn't have a fuel engine";
            }

            return operationMsg;
        }

        public string ChargeVehicle(string i_LicenseNumber, float i_AmountOfMinutesToCharge)
        {
            string operationMsg = " ";
            Vehicle vehicleToCharge = this.GetVehicleByLicenseNumber(i_LicenseNumber);
            ElectricEngine engineToCharge = vehicleToCharge.Engine as ElectricEngine;

            if(engineToCharge != null)
            {
                try
                {
                    engineToCharge.FillEnergy(i_AmountOfMinutesToCharge);
                    operationMsg = "Charging operation has been done";
                }
                catch(ArgumentException argumentException)
                {
                    operationMsg = "Value Error";
                }
                catch(ValueOutOfRangeException valueOutOfRangeException)
                {
                    operationMsg = "That is too much fuel";
                }
            }
            else
            {
                operationMsg = "This vehicle doesn't have an electric engine";
            }

            return operationMsg;
        }

        public void SetVehicleStatus(string i_LicenseNumber, int status)
        {
            if(IsVehicleExistInGarage(i_LicenseNumber))
            {
                r_ExistingVehicles[i_LicenseNumber].State = status;
            }
        }

        public List<string> GetVehiclesByFilter(int filer)
        {
            List<string> vehicleToSend = new List<string>();

            foreach(string vehicleNumber in r_ExistingVehicles.Keys)
            {
                if(filer == 0)
                {
                    vehicleToSend.Add(vehicleNumber);
                }
                else if(filer == (int)VehicleDetails.eVehicleStates.Paid)
                {
                    if(r_ExistingVehicles[vehicleNumber].State == (int)VehicleDetails.eVehicleStates.Paid)
                    {
                        vehicleToSend.Add(vehicleNumber);
                    }
                }
                else if(filer == (int)VehicleDetails.eVehicleStates.Repaired)
                {
                    if(r_ExistingVehicles[vehicleNumber].State == (int)VehicleDetails.eVehicleStates.Repaired)
                    {
                        vehicleToSend.Add(vehicleNumber);
                    }
                }
                else if(filer == (int)VehicleDetails.eVehicleStates.Repairing)
                {
                    if(r_ExistingVehicles[vehicleNumber].State == (int)VehicleDetails.eVehicleStates.Repairing)
                    {
                        vehicleToSend.Add(vehicleNumber);
                    }
                }
            }

            return vehicleToSend;
        }

        public string GetVehicleData(string i_LicenseNumber)
        {
            VehicleDetails vehicleData = r_ExistingVehicles[i_LicenseNumber];

            return vehicleData.ToString();
        }
    }
}