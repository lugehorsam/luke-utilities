using System.Collections;
using System.Diagnostics;
/**

public abstract class InputScreen<TDatum, TBehavior> : Container<TDatum, TBehavior>
    where TDatum : struct
    where TBehavior : DatumBehavior<TDatum>, IInput
{
    public TBehavior SelectedInput
    {
        get;
        private set;
    }

    public IEnumerator Show ()
    {
        Diagnostics.Log("Data is " + Data.ToFormattedString());
        gameObject.SetActive (true);
        WaitForInput:
            while (SelectedInput == null) {
                yield return null;
            }
            if (IsValidInput (SelectedInput))
            {
                yield return StartCoroutine (Hide ());
            }
            else
            {
                SelectedInput = null;
                goto WaitForInput;
            }
    }

    void HandleInput(IInput behavior)
    {
        SelectedInput = (TBehavior) behavior;
    }

    IEnumerator Hide ()
    {
        yield return StartCoroutine(HideTransition());
        gameObject.SetActive (false);
    }

    protected sealed override void HandleRemovedBehaviorPreLayout(TBehavior behavior)
    {
        Diagnostics.Log("attaching handler");
        behavior.OnInput -= HandleInput;
    }

    protected sealed override void HandleNewBehaviorPreLayout(TBehavior behavior)
    {
        behavior.OnInput += HandleInput;
    }

    protected virtual bool IsValidInput (TBehavior behavior)
    {
        return true;
    }

    protected virtual IEnumerator ShowTransition () 
    {
        yield return null;
    }

    protected virtual IEnumerator HideTransition () 
    {
        yield return null;
    }
}
**/
