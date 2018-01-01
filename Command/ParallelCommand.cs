namespace Utilities.Command
{
    using System;

    public class ParallelCommand : Command
    {
        public override object Current { get; }

        protected override bool MoveNextInternal()
        {
            var anyCommandsRan = false;

            foreach (CommandStep commandStep in _commandSteps)
            {
                anyCommandsRan = commandStep.MoveNext() || anyCommandsRan;
            }

            return anyCommandsRan;
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
