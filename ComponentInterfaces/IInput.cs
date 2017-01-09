using System;

public interface IInput {

     event Action<IInput> OnInput;
}
