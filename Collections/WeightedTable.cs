using System.Collections.Generic;
using System.Linq;

public class WeightedTable<T> {
    Dictionary<T, float> elementsAndWeights = new Dictionary<T, float>();
    float totalWeightValue;

    public WeightedTable( Dictionary<T, float> elementsAndWeights ) {
        if ( elementsAndWeights == null ) {
            return;
        }        
        AddWeightedElements( elementsAndWeights );
    }

    public WeightedTable( T element, float weight ) {
        if ( elementsAndWeights == null ) {
            return;
        }
        AddWeightedElement( element, weight );
    }

    public WeightedTable() {

    }

    public T GetWeightedElement() {
        float randValue = UnityEngine.Random.Range( 0, totalWeightValue );

        T[] elements = GetElementsSortedByWeight();

        for ( int i = 0; i < elements.Length; i++ ) {            
            T element = elements[i];

            bool isLastElement = i == elements.Length - 1;
            if ( isLastElement ) {
                return element;
            }

            T nextElement = elements[i + 1];

            bool valueFallsWithinInterval = randValue > elementsAndWeights[element] && randValue < elementsAndWeights[nextElement];
            if ( valueFallsWithinInterval ) {
                return element;
            }           
        }
        return default(T);
    }

    public void AddWeightedElement( T element, float weight ) {
        elementsAndWeights[element] = totalWeightValue;
        totalWeightValue += weight;
    }

    public void AddWeightedElements( Dictionary<T, float> elementsAndWeights ) {
        foreach ( KeyValuePair<T, float> elementWeightPair in elementsAndWeights ) {
            T element = elementWeightPair.Key;
            float elementWeighting = elementWeightPair.Value;
            AddWeightedElement( element, elementWeighting );
        }
    }

    T[] GetElementsSortedByWeight() {
        T[] elements = elementsAndWeights.Keys.ToArray();
        T[] sortedElements = elements.OrderBy( (element ) => elementsAndWeights[element] ).ToArray();
        return sortedElements;
    }

    float GetElementValue( T element ) {
        return elementsAndWeights[element];
    }
}