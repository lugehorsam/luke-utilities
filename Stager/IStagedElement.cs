namespace Utilities
{
	public interface IStagedElement<T>
	{
		bool IsStaged { get; set; }
		
		T NewPlatform { get; set; }
	}
}

	
