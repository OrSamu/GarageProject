using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.GarageUI
{
    public class UIManager
    {
        private Garage m_Garage;

        public UIManager()
        {
            m_Garage = new Garage();
        }
        public enum eMenuCommands
        {
            AddNewVehicle = 1,
            ShowVehicleList = 2,
            ChangeVehicleState = 3,
            InflateVehicleWheels = 4,
            FillVehicleFuel = 5,
            ChargeVehicleEnergy = 6,
            ShowVehicleDetails = 7
        }

        private int getVehicleType()
        {
            int maxCommand = Enum.GetValues(typeof(VehicaleMaker.eVehicleTypes)).Cast<int>().Max();
            int minCommand = Enum.GetValues(typeof(VehicaleMaker.eVehicleTypes)).Cast<int>().Min();
            Console.WriteLine("please enter your vehicle's type");
            printVehicleTypesOptions();
            int vehicleType = getUserInput(maxCommand, minCommand);
            return vehicleType;
        }

        private string getStringFromUser(string i_OpeningMsg)
        {
            string userInput;
            Console.WriteLine(i_OpeningMsg);
            do
            {
                userInput = Console.ReadLine();

            } while (!InputValidation.IsNotEmptyInput(userInput));

            return userInput;
        }


        private void printVehicleTypesOptions()
        {
            Array vehicleTypes = Enum.GetValues(typeof(VehicaleMaker.eVehicleTypes));
            
            foreach (VehicaleMaker.eVehicleTypes type in vehicleTypes)
            {
               Console.WriteLine(@"Press ({1}) for {0}", type, (int)type);
            }

        }
        private void getVehicleInformation(out int i_VehicleType, out string i_VehicleModel, out float i_VehicleEnergy)
        {
            string energyInput;

            i_VehicleType = getVehicleType();
            i_VehicleModel = getStringFromUser("please enter a vehicle's model");

            Console.WriteLine("please enter our vehicle energy");
            do
            {
                energyInput = Console.ReadLine();

            }
            while(!InputValidation.isAFloat(energyInput));

            i_VehicleEnergy = float.Parse(energyInput);
        }

        private string getLicenseNumber()
        {
            string licsenseNumber;
            Console.WriteLine("please enter a license number");
            do
            {
                licsenseNumber = Console.ReadLine();

            } while (!InputValidation.IsNotEmptyInput(licsenseNumber));

            return licsenseNumber;
        }
        private void changeVehicleStateInGarage()
        {
            string licenseNumber = getLicenseNumber();
            if (m_Garage.IsVihicleExistInGarage(licenseNumber))
           {
               int state = getStateFromUser();
                m_Garage.SetVehicleStatus(licenseNumber, state);
           }
           else
           {
               Console.WriteLine("Vehicle doesn't exist in the garage");
           }

        }

        private void inflateVehiclesWheels()
        {
            Vehicle vehicleToInflate;
            string licenseNumber = getLicenseNumber();
            if (m_Garage.IsVihicleExistInGarage(licenseNumber))
            {
                vehicleToInflate = m_Garage.getVehicleByLicenseNumber(licenseNumber);
                vehicleToInflate.InflateWheelsToMax();
            }
            else
            {
                Console.WriteLine("Vehicle doesn't exist in the garage");
            }

        } 

        private int getStateFromUser()
        {
            int stateInput;
            int maxCommand = Enum.GetValues(typeof(VehicleDetails.eVehicleStates)).Cast<int>().Max();
            int minCommand = Enum.GetValues(typeof(VehicleDetails.eVehicleStates)).Cast<int>().Min() ;
            Array vehicleStates = Enum.GetValues(typeof(VehicleDetails.eVehicleStates));

            Console.WriteLine("please enter status:");
            foreach (VehicleDetails.eVehicleStates state in vehicleStates)
            {
                Console.WriteLine(@"Press ({1}) to change {0} to state", state, (int)state);
            }

            stateInput = getUserInput(maxCommand,minCommand);
            return stateInput;
        }
        private void showVehiclesInGarage()
        {
            int userFilterINput;
            List<string> vehicleLiscenseList;
            int maxCommand = Enum.GetValues(typeof(VehicleDetails.eVehicleStates)).Cast<int>().Max();
            int minCommand = Enum.GetValues(typeof(VehicleDetails.eVehicleStates)).Cast<int>().Min()-1;
            Array vehicleStates = Enum.GetValues(typeof(VehicleDetails.eVehicleStates));

            Console.WriteLine("Press (0) to show all vehicles ");
            foreach (VehicleDetails.eVehicleStates state in vehicleStates)
            {
                Console.WriteLine(@"Press ({1}) to show vehivles in {0} state", state, (int)state);
            }
            userFilterINput = getUserInput(maxCommand, minCommand);
            vehicleLiscenseList= m_Garage.GetVehiclesByFilter(userFilterINput);
            foreach(string vehicleNumber in vehicleLiscenseList)
            {
                Console.WriteLine(vehicleNumber);
            }

        }
        private void addNewVehicle()
        {
            string licsenseNumber;
            int vehicleType;
            string vehicleModel;
            float vehicleEnergy;
            string ownerNumber;
            string ownerName;
            Vehicle newVehicle;
            List<string> vehicleQualificationsOutput, vehicleQualificationsInput;
            Console.WriteLine("please enter a license number");
            do
            {
                 licsenseNumber = Console.ReadLine();

            }while(!InputValidation.IsNotEmptyInput(licsenseNumber));

            if(m_Garage.IsVihicleExistInGarage(licsenseNumber))
            {
                m_Garage.SetVehicleStatus(licsenseNumber, (int)VehicleDetails.eVehicleStates.Repairing);
                Console.WriteLine("your vehicle status has change to repairing");

            }
            else
            {
                getVehicleInformation(out vehicleType, out vehicleModel, out vehicleEnergy);

                newVehicle = VehicaleMaker.CreateVehicle(licsenseNumber, vehicleModel, vehicleEnergy, vehicleType);
                vehicleQualificationsOutput = newVehicle.GetNeededQualifications();
                vehicleQualificationsInput = getVehicleQualifactions(vehicleQualificationsOutput, newVehicle);
                newVehicle.SetNeededQualifications(vehicleQualificationsInput);
                getOwnerInfo(out ownerNumber, out ownerName);
                m_Garage.AddVehicle(licsenseNumber, ownerNumber, ownerName,newVehicle);
            }
        }

        private void getOwnerInfo(out string o_OwnerNumber, out string o_OwnerName)
        {
            o_OwnerName = getStringFromUser("please enter an owner name");
            o_OwnerNumber = getStringFromUser("please enter an owner number");
        }
        private List<string> getVehicleQualifactions(List<string> vehicleQualifactions,Vehicle i_NewVehicle)
        {
            List<string> userVehicleQualificationList = new List<string>();
            int qualificationsIndex = 1;
            string qualifcationToCheck;
            bool isValidInput = false;

            foreach (string qualifaction in vehicleQualifactions)
            {
                isValidInput = false;
                Console.WriteLine(qualifaction);
                
                do
                {
                    try
                    {
                        qualifcationToCheck = Console.ReadLine();

                        if (i_NewVehicle.CheckNeededQualifications(qualifcationToCheck, qualificationsIndex))
                        {
                            isValidInput = true;
                            userVehicleQualificationList.Add(qualifcationToCheck);
                        }
                    }
                    catch (FormatException formatException)
                    {
                        Console.Write("Invalid input, please try again: ");
                    }
                    catch (ValueOutOfRangeException valueOutOfRangeException)
                    {
                        Console.Write("You must enter a number between {0} and {1}: ", valueOutOfRangeException.MinValue, valueOutOfRangeException.MaxValue);
                    }
                }
                while (!isValidInput);

                qualificationsIndex++;
            }

            return userVehicleQualificationList;
        }

        public int getFuelType()
        {
            int userFilterInput;
            int maxCommand = Enum.GetValues(typeof(FuelEngine.eFuelType)).Cast<int>().Max();
            int minCommand = Enum.GetValues(typeof(FuelEngine.eFuelType)).Cast<int>().Min();
            Array fuelTypes = Enum.GetValues(typeof(FuelEngine.eFuelType));

            Console.WriteLine("please enter fuel type");
            foreach (FuelEngine.eFuelType fuelType in fuelTypes)
            {
                Console.WriteLine(@"Press ({1}) to fill {0} ", fuelType, (int)fuelType);
            }
            userFilterInput = getUserInput(maxCommand, minCommand);
            return userFilterInput;
        }
        public void FillVehiclesTank()
        {
            int fuelType;
            string licenseNumber;
            string fuelAmountString;
            float fuelAmount;
            licenseNumber = getLicenseNumber();
            if (m_Garage.IsVihicleExistInGarage(licenseNumber))
            {
                if(m_Garage.getVehicleByLicenseNumber(licenseNumber).engine.getEngineType()
                   == (int)Engine.eEngineType.Fuel)
                {
                    fuelType = getFuelType();
                    Console.WriteLine("please enter fuel amount");
                    do
                    {
                        fuelAmountString = Console.ReadLine();

                    }
                    while (!InputValidation.isAFloat(fuelAmountString));

                    fuelAmount = float.Parse(fuelAmountString);
                    m_Garage.getVehicleByLicenseNumber(licenseNumber).engine.FillEnergy(fuelAmount);
                }
            }
            else
            {
                Console.WriteLine("Vehicle doesn't exist in the garage");
            }
        }
        private void runCommand(int i_UserCommand)
        {
            switch(i_UserCommand)
            {
                case (int)eMenuCommands.AddNewVehicle:
                    addNewVehicle();
                    break;

                case (int)eMenuCommands.ShowVehicleList:
                    showVehiclesInGarage();
                    break;
                case (int)eMenuCommands.ChangeVehicleState:
                    changeVehicleStateInGarage();
                    break;
                case (int)eMenuCommands.InflateVehicleWheels:
                    inflateVehiclesWheels();
                    break;
                case (int)eMenuCommands.FillVehicleFuel:
                    FillVehiclesTank();
                    break;
                case (int)eMenuCommands.ChargeVehicleEnergy:
                    break;
                case (int)eMenuCommands.ShowVehicleDetails:
                    break;
            }
        }
        public void Run()
        {
            int maxCommand = Enum.GetValues(typeof(eMenuCommands)).Cast<int>().Max();
            int minCommand = Enum.GetValues(typeof(eMenuCommands)).Cast<int>().Min();
            int userCommand;

            Console.WriteLine("Welcome To Our Garage!");
            printUserMenu();

            userCommand = getUserInput(maxCommand, minCommand);
            runCommand(userCommand);

        }

        private int getUserInput(int maxCommand, int minCommand)
        {
            bool isValid = false;
            string userInput="";

            while(!isValid)
            {
                userInput = Console.ReadLine();
                isValid = isValidCommand(userInput, minCommand, maxCommand);
            }

            return int.Parse(userInput);
        }


        private bool isValidCommand(string i_UserInput, int min, int max)
        {
            int command;
            bool isInputANumber = int.TryParse(i_UserInput, out command);

            if(isInputANumber)
            {
                isInputANumber = (command>=min && command<=max);
            }

            if(!isInputANumber)
            {
                Console.WriteLine("invalid command, please try again");
            }

            return isInputANumber;
        }

        private void printUserMenu()
        {
            Console.WriteLine(@"Please Enter Command
Press ({0}) to add a new vehicle
Press ({1}) to show list of vehicle in garage
Press ({2}) to change a vehicle state
Press ({3}) to inflate vehicle's wheels
Press ({4}) to fill vehicle's tank
Press ({5}) to charge a vehicle
Press ({6}) to show vehicle's details",(int)eMenuCommands.AddNewVehicle, (int)eMenuCommands.ShowVehicleList, (int)eMenuCommands.ChangeVehicleState
            , (int)eMenuCommands.InflateVehicleWheels, (int)eMenuCommands.FillVehicleFuel, (int)eMenuCommands.ChargeVehicleEnergy, (int)eMenuCommands.ShowVehicleDetails);
        }
    }
    
}
