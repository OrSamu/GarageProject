using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(float i_MaxValue, float i_MinValue)
            : base(string.Format("Entered value out of given range. Max:{0} Min:{1}", i_MaxValue, i_MinValue))
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }

        public float MaxValue
        {
            get
            {
                return r_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return r_MinValue;
            }
        }
    }
}