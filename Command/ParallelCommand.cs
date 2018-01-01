namespace Utilities.Command
{
    using System;

    public class ParallelCommand : Command
    {
        public override object Current { get; }

        protected override bool MoveNextInternal()
        {
            var anyCommandsRan = false;
            _commandSteps.RemoveAll(step => !step.MoveNext());
            return _commandSteps.Count > 0;
        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
