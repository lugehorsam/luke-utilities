namespace Utilities.Bindings
{	
	public interface IPropertyComponent
	{
		BindType BindType { get; }
		void SetPropertyObject(PropertyObject obj);
	}	
}
