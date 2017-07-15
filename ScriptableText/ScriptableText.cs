using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(Text))]
public class ScriptableText : MonoBehaviour {
    [SerializeField]
    List<ScriptableTextInfo> texts;

    [SerializeField]
    bool playOnStart = false;

    Text uiText;

    void Awake() {
        uiText = GetComponent<Text> ();
    }

    void Start() {
        if (playOnStart) {
            StartCoroutine (Play ());
        }
    }

    IEnumerator Play() {
        int textNum = 0;
        while (textNum < texts.Count) {
            ScriptableTextInfo currText = texts [textNum];
            uiText.text = currText.Text;
            yield return new WaitForSeconds (currText.Delay);
            textNum++;
        }
    }
}
          