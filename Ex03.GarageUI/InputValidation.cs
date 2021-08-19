using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageUI
{
    public static class InputValidation
    {
        public static bool IsNotEmptyInput(string i_IsValidLicense)
        {
            return i_IsValidLicense.Length >= 1 && (i_IsValidLicense[0]!=' ');
        }

        public static bool IsAFloat(string userInput)
        {
            float inputNumber;
            bool isInputFloat = float.TryParse(userInput, out inputNumber);

            isInputFloat = isInputFloat && inputNumber > 0;

            return isInputFloat;
        }
    }


}
