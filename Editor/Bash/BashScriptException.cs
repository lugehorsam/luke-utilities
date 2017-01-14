using System;

public class BashScriptException : Exception {

    public BashScriptException(BashScript script) : base(script.StdError)
    {

    }
}
