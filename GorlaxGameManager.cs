using System.Linq;
using System.Collections;
using UnityEngine;
using Utilities;
using Datum;
using Scripting;

public class GorlaxGameManager : MonoBehaviour
{
    private GameScreen GameScreen
    {
        get { return gameScreen.Instantiate<GameScreen>(); }
    }

    [SerializeField]
    private LazyPrefab gameScreen;

    private IEnumerator Start()
    {
        var interactions = new InteractionRequest();

        var tilesRequest = ScriptManager.FetchContent<TileDatum>(ResourcesConfig.TILES);
        var animalsRequest = ScriptManager.FetchContent<Animal>(ResourcesConfig.ANIMALS);

        yield return this.StartParallelCoroutines
        (
            tilesRequest,
            animalsRequest
        );

        yield return StartCoroutine(interactions);

        GameScreen.Datum = interactions.Datum.Array.First();
        StartCoroutine(GameScreen.Show());
    }
}
