using System;

namespace Datum
{
    public class CompositeBehavior<TCompositeDatum, TDatum, TBehavior> : DatumBehavior<TCompositeDatum> 
        where TCompositeDatum : IComposite<TDatum>
        where TBehavior : DatumBehavior<TDatum>

    {
        protected readonly DatumBehaviorManager<TDatum, TBehavior> datumBehaviorManager;
        
        public CompositeBehavior(Func<TBehavior> factory)
        {
            datumBehaviorManager = new DatumBehaviorManager<TDatum, TBehavior>(factory);
            datumBehaviorManager.OnBehaviorAdded += HandleNewBehavior;
            datumBehaviorManager.OnBehaviorRemoved += HandleRemovedBehavior;
        }

        protected sealed override void HandleAfterDatumUpdate(TCompositeDatum oldData, TCompositeDatum newData)
        {
            datumBehaviorManager.Data.RemoveAll();
            datumBehaviorManager.Data.AddRange(newData.GetCompositeData());
            HandleCompositeDatumChanged();
        }

        protected virtual void HandleCompositeDatumChanged()
        {
        }
        
        protected virtual void HandleNewBehavior(TBehavior behavior)
        {            
        }
        
        protected virtual void HandleRemovedBehavior(TBehavior behavior)
        {            
        }       
    }
}
