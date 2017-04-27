using System.Collections.ObjectModel;

public interface INode<T> {
    
    T Value { get; set; }
    ReadOnlyCollection<T> TargetNodes { get; }
    ReadOnlyCollection<T> SourceNodes { get; }
}
