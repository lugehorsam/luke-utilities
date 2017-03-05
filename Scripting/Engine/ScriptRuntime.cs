using System.Collections.Generic;
using System.Collections;
using Scripting;
using System.Linq;
using Datum;

public class ScriptRuntime {
   
    public Dictionary<string, List<ScriptObject>> ScriptObjects
    {
        get
        {
            return scriptObjects;
        }
    }

    private readonly Dictionary<string, List<ScriptObject>> scriptObjects = new Dictionary<string, List<ScriptObject>>();    
    private readonly HashSet<Variable> variables = new HashSet<Variable>();
    private readonly HashSet<Variable> unresolvedVariables = new HashSet<Variable>();
 
    public void AddVariable(Variable variable)
    {
        if (!Variable.IsValidIdentifier(variable.Identifier))
        {
            throw new InvalidIdentifierException(variable.Identifier);
        }

        string newVal;

        if (Variable.IsValidIdentifier(variable.Value))
        {

            Variable referencedVariable = GetVariableWithIdentifier(variable.Value);
            if (referencedVariable == null)
            {
                unresolvedVariables.Add(variable);
            }
            else
            {
                variable.Value = referencedVariable.Value;
                variables.Add(variable);
                TryResolveVariables(variable);
            }
        }
        else
        {

            string resolvedValue;
            if (variable.Query.TryGetResolvedString(this, out resolvedValue))
            {
                
            }
        }


    }




    public void AddScriptObject(string contentId, ScriptObject scriptObject)
    {
        if (!scriptObjects.ContainsKey(contentId))
        {
            scriptObjects[contentId] = new List<ScriptObject>();
        }     
        
        scriptObject.RegisterRuntime(this);
        scriptObjects[contentId].Add(scriptObject);
    }

    Variable GetVariableWithIdentifier(string identifier)
    {
        Diagnostics.Log("Trying to get variable with identifier: {0}", identifier);
        if (!Variable.IsValidIdentifier(identifier))
        {
            throw new InvalidIdentifierException(identifier);
        }

        return variables.FirstOrDefault((variable) => variable.Identifier == identifier);
    }
    
    void TryResolveVariables(Variable newVariable)
    {
        foreach (var variable in unresolvedVariables)
        {
            if (variable.Value == newVariable.Identifier)
            {
                variable.Value = newVariable.Value;
            }
        }
    }
}
