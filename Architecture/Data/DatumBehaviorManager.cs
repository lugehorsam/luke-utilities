using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Datum
{
    public class DatumBehaviorManager<TDatum, TBehavior>
        where TBehavior : DatumBehavior<TDatum>
    {

        public ObservableList<TDatum> Data
        {
            get { return data; }
        }

        readonly ObservableList<TDatum> data = new ObservableList<TDatum>();

        public ReadOnlyCollection<TBehavior> Behaviors
        {
            get
            {
                return new ReadOnlyCollection<TBehavior>(behaviorPool.ReleasedObjects
                    .OrderBy(behavior => data.IndexOf(behavior.Datum))
                    .ToList());
            }
        }

        protected ObjectPool<TBehavior> behaviorPool;

        public event Action<TBehavior> OnBehaviorAdded = (behavior) => { };
        public event Action<TBehavior> OnBehaviorRemoved = (behavior) => { };


        public DatumBehaviorManager(Func<TBehavior> factory)
        {
            behaviorPool = new GameObjectPool<TBehavior>(factory, 10);
            data.OnAdd += HandleAddDatum;
            data.OnRemove += HandleRemoveDatum;
        }

        public void Observe(ObservableList<TDatum> otherData)
        {
            otherData.RegisterObserver(data);
        }

        protected virtual void HandleNewBehavior(TBehavior behavior) {}
        protected virtual void HandleRemovedBehavior(TBehavior behavior) {}
        protected virtual void AddHandlers (TBehavior behavior) {}
        protected virtual void RemoveHandlers (TBehavior behavior) {}

        protected virtual ObservableList<TDatum> GetOverrideData ()
        {
            return null;
        }

        void HandleAddDatum (TDatum newDatum)
        {
            TBehavior behavior = behaviorPool.Release ();
            behavior.Datum = newDatum;
            AddHandlers (behavior);
            HandleNewBehavior (behavior);
            OnBehaviorAdded(behavior);
        }

        void HandleRemoveDatum (TDatum oldDatum, int oldDatumIndex)
        {
            TBehavior behaviorToPool = Behaviors.First ((behavior) => behavior.Datum.Equals (oldDatum));
            RemoveHandlers (behaviorToPool);
            behaviorPool.Pool (behaviorToPool);
            HandleRemovedBehavior (behaviorToPool);
            OnBehaviorRemoved(behaviorToPool);

        }
    }
}
