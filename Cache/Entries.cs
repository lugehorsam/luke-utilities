using System.Collections.Generic;
using Utilities.Serializable;

namespace Utilities.Cache
{
	public class Entries<T> : Entry<JSONList<T>>
	{
		public Entries(string key) : base(key)
		{
		}

		protected override JSONList<T> GetDefaultValue()
		{
			return new JSONList<T>();
		}
		        
		public static implicit operator List<T>(Entries<T> thisProps)
		{
			return thisProps.Value;
		}
	}	
}