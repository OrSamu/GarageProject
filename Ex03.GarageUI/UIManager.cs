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
            ShowVehicleDetails = 7,
            Quit = 8
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
            }
            while(!InputValidation.IsNotEmptyInput(userInput));

            return userInput;
        }

        private void printVehicleTypesOptions()
        {
            Array vehicleTypes = Enum.GetValues(typeof(VehicaleMaker.eVehicleTypes));

            foreach(VehicaleMaker.eVehicleTypes type in vehicleTypes)
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
            while(!InputValidation.IsAFloat(energyInput));

            i_VehicleEnergy = float.Parse(energyInput);
        }

        private string getLicenseNumber()
        {
            string licsenseNumber;

            Console.WriteLine("please enter a license number");

            do
            {
                licsenseNumber = Console.ReadLine();
            }
            while(!InputValidation.IsNotEmptyInput(licsenseNumber));

            return licsenseNumber;
        }

        private void changeVehicleStateInGarage()
        {
            string licenseNumber = getLicenseNumber();

            if(m_Garage.IsVehicleExistInGarage(licenseNumber))
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
            if(m_Garage.IsVehicleExistInGarage(licenseNumber))
            {
                vehicleToInflate = m_Garage.GetVehicleByLicenseNumber(licenseNumber);
                Console.WriteLine(vehicleToInflate.InflateWheelsToMax());
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
            int minCommand = Enum.GetValues(typeof(VehicleDetails.eVehicleStates)).Cast<int>().Min();
            Array vehicleStates = Enum.GetValues(typeof(VehicleDetails.eVehicleStates));

            Console.WriteLine("please enter status:");
            foreach(VehicleDetails.eVehicleStates state in vehicleStates)
            {
                Console.WriteLine(@"Press ({1}) to change {0} to state", state, (int)state);
            }

            stateInput = getUserInput(maxCommand, minCommand);
            return stateInput;
        }

        private void showVehiclesInGarage()
        {
            int userFilterINput;
            List<string> vehicleLiscenseList;
            int maxCommand = Enum.GetValues(typeof(VehicleDetails.eVehicleStates)).Cast<int>().Max();
            int minCommand = Enum.GetValues(typeof(VehicleDetails.eVehicleStates)).Cast<int>().Min() - 1;
            Array vehicleStates = Enum.GetValues(typeof(VehicleDetails.eVehicleStates));

            Console.WriteLine("Press (0) to show all vehicles ");
            foreach(VehicleDetails.eVehicleStates state in vehicleStates)
            {
                Console.WriteLine(@"Press ({1}) to show vehivles in {0} state", state, (int)state);
            }

            userFilterINput = getUserInput(maxCommand, minCommand);
            vehicleLiscenseList = m_Garage.GetVehiclesByFilter(userFilterINput);
            foreach(string vehicleNumber in vehicleLiscenseList)
            {
                Console.WriteLine(vehicleNumber);
            }
        }

        private void addNewVehicle()
        {
            string licenseNumber;
            int vehicleType;
            string vehicleModel;
            float vehicleEnergy;
            string ownerNumber;
            string ownerName;
            Vehicle newVehicle;
            List<string> vehicleQualificationsOutput, vehicleQualificationsInput;

            licenseNumber = getLicenseNumber();

            if(m_Garage.IsVehicleExistInGarage(licenseNumber))
            {
                m_Garage.SetVehicleStatus(licenseNumber, (int)VehicleDetails.eVehicleStates.Repairing);
                Console.WriteLine("Welcome back, your vehicle status has changed to repairing");
            }
            else
            {
                vehicleType = getVehicleType();
                newVehicle = VehicaleMaker.CreateVehicle(licenseNumber, vehicleType);

                vehicleQualificationsOutput = newVehicle.GetNeededQualifications();
                vehicleQualificationsInput = getVehicleQualifactions(vehicleQualificationsOutput, newVehicle);
                newVehicle.SetNeededQualifications(vehicleQualificationsInput);
                getOwnerInfo(out ownerNumber, out ownerName);
                m_Garage.AddVehicle(licenseNumber, ownerName, ownerNumber, newVehicle);
                Console.WriteLine("The vehicle has been added successfully");
            }
        }

        private void getOwnerInfo(out string o_OwnerNumber, out string o_OwnerName)
        {
            o_OwnerName = getStringFromUser("please enter an owner name");
            o_OwnerNumber = getStringFromUser("please enter an owner number");
        }

        private List<string> getVehicleQualifactions(List<string> vehicleQualifactions, Vehicle i_NewVehicle)
        {
            List<string> userVehicleQualificationList = new List<string>();
            int qualificationsIndex = 0;
            string qualifcationToCheck;
            bool isValidInput = false;

            foreach(string qualifaction in vehicleQualifactions)
            {
                isValidInput = false;
                Console.WriteLine(qualifaction);

                do
                {
                    try
                    {
                        qualifcationToCheck = Console.ReadLine();

                        if(i_NewVehicle.CheckNeededQualifications(qualifcationToCheck, qualificationsIndex))
                        {
                            isValidInput = true;
                            userVehicleQualificationList.Add(qualifcationToCheck);
                        }
                    }
                    catch(FormatException formatException)
                    {
                        Console.Write("Invalid input, please try again: ");
                    }
                    catch(ValueOutOfRangeException valueOutOfRangeException)
                    {
                        Console.Write(
                            "You must enter a number between {0} and {1}: ",
                            valueOutOfRangeException.MinValue,
                            valueOutOfRangeException.MaxValue);
                    }
                }
                while(!isValidInput);

                qualificationsIndex++;
            }

            return userVehicleQualificationList;
        }

        public int GetFuelType()
        {
            int userFilterInput;
            int maxCommand = Enum.GetValues(typeof(FuelEngine.eFuelType)).Cast<int>().Max();
            int minCommand = Enum.GetValues(typeof(FuelEngine.eFuelType)).Cast<int>().Min();
            Array fuelTypes = Enum.GetValues(typeof(FuelEngine.eFuelType));

            Console.WriteLine("please enter fuel type");

            foreach(FuelEngine.eFuelType fuelType in fuelTypes)
            {
                Console.WriteLine(@"Press ({1}) to fill {0} ", fuelType, (int)fuelType);
            }

            userFilterInput = getUserInput(maxCommand, minCommand);

            return userFilterInput;
        }

        private float getEnergyAmount(string i_TypeOfEnergy)
        {
            string energyFromUser;
            float energyInFloat;

            Console.WriteLine(@"Please enter {0} you would like to add: ", i_TypeOfEnergy);
            do
            {
                energyFromUser = Console.ReadLine();
            }
            while(!InputValidation.IsNotEmptyInput(energyFromUser));

            energyInFloat = float.Parse(energyFromUser);

            return energyInFloat;
        }

        public void FillVehiclesTank()
        {
            string licenseNumber = getLicenseNumber();
            bool isExistInGarage = m_Garage.IsVehicleExistInGarage(licenseNumber);
            FuelEngine.eFuelType fuelType;
            float amountOfFuelToFill;
            string msgForUser;

            if(isExistInGarage)
            {
                fuelType = (FuelEngine.eFuelType)GetFuelType();
                amountOfFuelToFill = getEnergyAmount("fuel liters");
                msgForUser = m_Garage.FillVehicleTank(licenseNumber, fuelType, amountOfFuelToFill);
            }
            else
            {
                msgForUser = "Vehicle doesn't exist in the garage";
            }

            Console.WriteLine(msgForUser);
        }

        public void ChargeVehicleBattery()
        {
            string licenseNumber = getLicenseNumber();
            bool isExistInGarage = m_Garage.IsVehicleExistInGarage(licenseNumber);
            float amountOfMinutesToCharge;
            string msgForUser;

            if(isExistInGarage)
            {
                amountOfMinutesToCharge = getEnergyAmount("minutes to charge");
                msgForUser = m_Garage.ChargeVehicle(licenseNumber, amountOfMinutesToCharge);
            }
            else
            {
                msgForUser = "Vehicle doesn't exist in the garage";
            }

            Console.WriteLine(msgForUser);
        }

        private void showVehicleDetails()
        {
            string licsenseNumber;
            bool isExist = false;

            licsenseNumber = getLicenseNumber();
            isExist = m_Garage.IsVehicleExistInGarage(licsenseNumber);

            if(isExist)
            {
                Console.Write(m_Garage.GetVehicleData(licsenseNumber));
            }
            else
            {
                Console.WriteLine("License number isn't exist in garage");
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
                    ChargeVehicleBattery();
                    break;
                case (int)eMenuCommands.ShowVehicleDetails:
                    showVehicleDetails();
                    break;
                case (int)eMenuCommands.Quit:
                    exitMessage();
                    break;
            }
        }

        private void returnToMenuWaiter()
        {
            Console.Write("\nPress enter to return to the menu...");
            Console.ReadLine();
            Console.Clear();
        }

        private void exitMessage()
        {
            Console.Write("\nShutting down, good bye..");
        }

        public void Run()
        {
            int maxCommand = Enum.GetValues(typeof(eMenuCommands)).Cast<int>().Max();
            int minCommand = Enum.GetValues(typeof(eMenuCommands)).Cast<int>().Min();
            int userCommand;
            bool quitProgram = false;

            Console.WriteLine("Welcome To Our Garage!");

            while(!quitProgram)
            {
                printUserMenu();
                userCommand = getUserInput(maxCommand, minCommand);
                quitProgram = userCommand == (int)eMenuCommands.Quit;
                runCommand(userCommand);

                if(!quitProgram)
                {
                    returnToMenuWaiter();
                }
            }
        }

        private int getUserInput(int maxCommand, int minCommand)
        {
            bool isValid = false;
            string userInput = "";

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
                isInputANumber = (command >= min && command <= max);
            }

            if(!isInputANumber)
            {
                Console.WriteLine("invalid command, please try again");
            }

            return isInputANumber;
        }

        private void printUserMenu()
        {
            Console.WriteLine(
                @"Please Enter Command
Press ({0}) to add a new vehicle
Press ({1}) to show list of vehicle in garage
Press ({2}) to change a vehicle state
Press ({3}) to inflate vehicle's wheels
Press ({4}) to fill vehicle's tank
Press ({5}) to charge a vehicle
Press ({6}) to show vehicle's details
Press ({7}) to quit",
                (int)eMenuCommands.AddNewVehicle,
                (int)eMenuCommands.ShowVehicleList,
                (int)eMenuCommands.ChangeVehicleState,
                (int)eMenuCommands.InflateVehicleWheels,
                (int)eMenuCommands.FillVehicleFuel,
                (int)eMenuCommands.ChargeVehicleEnergy,
                (int)eMenuCommands.ShowVehicleDetails,
                (int)eMenuCommands.Quit);
        }
    }
}