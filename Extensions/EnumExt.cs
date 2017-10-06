using System;
using System.Linq;

namespace Utilities
{
 
    public static class EnumExt {

        //cyclic
        public static T GetNextValue<T>(this Enum currentValue)
        {
            Array values = Enum.GetValues(typeof(T));
            for (int i = 0; i < values.Length; i++)
            {                        
                T typedValue = (T) values.GetValue(i);            
            
                if (typedValue.Equals(currentValue))
                {
                    if (i == values.Length - 1)
                    {
                        return (T) values.GetValue(0);
                    }
                
                    return (T) values.GetValue(i + 1);                
                }
            }        
        
            throw new Exception("Could not find value " + currentValue + " in enum type " + typeof(T));
        }

        public static T[] GetValues<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            return values.Cast<T>().ToArray();
        }

        public static T GetAtIndex<T>(int i)
        {
            return GetValues<T>()[i];
        }
    }
   

}