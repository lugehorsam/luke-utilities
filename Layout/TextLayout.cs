using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class TextLayout : MonoBehaviour {

    public int Count => words.Count;

    [SerializeField]
    private List<GameObject> words = new List<GameObject>();
    [SerializeField]
    private float spacing = .2f;
    [SerializeField]
    private float lineWidth = 3f;
    [SerializeField]
    private float lineHeight = 1f;

    public void AddBefore(GameObject toAdd, GameObject existing) {
        InsertWordAtIndex(toAdd, words.IndexOf(existing));
    }

    public void AddAfter(GameObject toAdd, GameObject existing) {
        InsertWordAtIndex(toAdd, words.IndexOf(existing) + 1); 
    }

    public void Add(GameObject toAdd) {
        InsertWordAtIndex(toAdd, Count - 1);
    }
        
    public void AddToEnd(GameObject toAdd) {
        InsertWordAtIndex(toAdd, words.Count - 1); 
    }

    public bool Contains (GameObject word) {
        return words.Contains(word);
    }

    public void Remove(GameObject word) {
        words.Remove (word);
        ArrangeWords();
    }

    void InsertWordAtIndex(GameObject word, int index) {
        if (words.Contains(word)) {
            words.Remove(word);
        }

        if (index >= Count - 1) {
            words.Add(word);
        } else {
            words.Insert(index, word);
        }
        ArrangeWords();
    }        

    void ArrangeWords() {
        words.TrimExcess();
        GameObject lastWord = null;
        int lineNumber = 0;
        foreach (GameObject word in words) {
            word.transform.SetParent(transform, false);

            if (wordIsWrapped(lastWord)) {
                lineNumber++;
                lastWord = null;
            }
            float wordX = GetNewWordX (lastWord);
            word.transform.localPosition = new Vector2(wordX, lineNumber * -lineHeight);
            lastWord = word;
        }
    }

    float GetNewWordX(GameObject previousWord) {
        float wordX = GetNewWordUnwrappedX(previousWord) % lineWidth;
        return wordX;
    }

    float GetNewWordUnwrappedX(GameObject previousWord) {
        if (previousWord == null) {
            return 0;
        }
        float previousWordWidth = previousWord.GetComponent<MeshRenderer>().bounds.extents.x * 2;
        return previousWord.transform.localPosition.x + previousWordWidth + spacing;
    }

    bool wordIsWrapped(GameObject previousWord) {
        return GetNewWordUnwrappedX(previousWord) > lineWidth; 
    }

    void Start() {
        ArrangeWords ();
    }
}
