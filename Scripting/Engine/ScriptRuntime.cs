using System.Collections.Generic;
using Scripting;
using System.Linq;

public class ScriptRuntime {
    
    private readonly Dictionary<string, List<ScriptObject>> scriptObjects = new Dictionary<string, List<ScriptObject>>(); 
        
    private readonly HashSet<Variable> variables = new HashSet<Variable>();
    private readonly HashSet<Variable> unresolvedVariables = new HashSet<Variable>();
 
    public void AddVariable(Variable variable)
    {
        if (!Variable.IsValidIdentifier(variable.Identifier))
        {
            throw new MalformedVariableException(variable.Identifier);
        }

        string newVal;

        if (TryResolveValue(variable.Value, out newVal))
        {
            variable.Value = newVal;
            variables.Add(variable);
            TryResolveVariables(variable);
        }
        else
        {
            unresolvedVariables.Add(variable);
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
        if (!Variable.IsValidIdentifier(identifier))
        {
            throw new MalformedVariableException(identifier);
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

    bool TryResolveValue(string rawValue, out string resolvedValue)
    {
        if (Variable.IsValidIdentifier(rawValue))
        {
            var identifiedVar = GetVariableWithIdentifier(rawValue);

            if (identifiedVar == null)
            {
                resolvedValue = rawValue;
                return false;
            }

            resolvedValue = identifiedVar.Value;
            return true;
        }

        resolvedValue = rawValue;
        return true;
    }
}
