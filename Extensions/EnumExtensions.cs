using System;

public static class EnumExtensions {

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
	
}
