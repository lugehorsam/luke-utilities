namespace Utilities.Cache
{
	using System.Collections.Generic;

	public class Entries<T> : Entry<List<T>>
	{
		public Entries(string key) : base(key) { }

		protected override List<T> GetDefaultValue()
		{
			return new List<T>();
		}
	}
}
