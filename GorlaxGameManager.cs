using UnityEngine;
using System.Collections;
using System.Linq;

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

        yield return this.StartParallelCoroutines(
            interactionRequest,
            nameRequest,
            characterRequest
        );

        CharacterSelectionScreen.Observe(characterRequest.Data);
        GameScreen.Datum = interactionRequest.Data.First();
        //yield return StartCoroutine(CharacterSelectionScreen.Show());
        yield return StartCoroutine(GameScreen.Show());
    }
}
