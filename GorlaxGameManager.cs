using UnityEngine;
using System.Collections;
using Datum;

public class GorlaxGameManager : MonoBehaviour {

    private CharacterSelectionScreen CharacterSelectionScreen
    {
        get
        {
            return characterSelectionPrefab.GetInstance<CharacterSelectionScreen>();
        }
    }

    public GameScreen GameScreen
    {
        get { return gameScreenPrefab.GetInstance<GameScreen>(); }
    }

    [SerializeField] private LazyPrefab characterSelectionPrefab;
    [SerializeField] private LazyPrefab gameScreenPrefab;

    IEnumerator Start()
    {
        var interactionRequest = new InteractionRequest();
        var nameRequest = new NameRequest();
        var characterRequest = new CharacterRequest();

        StartCoroutine(interactionRequest);
        yield return this.StartParallelCoroutines(
            nameRequest,
            characterRequest
        );


        yield return StartCoroutine(CharacterSelectionScreen.Show());
        yield return StartCoroutine(GameScreen.Show());
    }
}
