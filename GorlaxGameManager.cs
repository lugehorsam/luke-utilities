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
        var tiles = new ResourcesRequest<ContentList<TileDatum>>(ResourcesConfig.TILES);
        var animals = new ResourcesRequest<ContentList<Animal>>(ResourcesConfig.ANIMALS);

        yield return this.StartParallelCoroutines
        (
            tiles,
            animals
        );

        yield return StartCoroutine(interactions);

        GameScreen.Datum = interactions.Datum.Array.First();
        StartCoroutine(GameScreen.Show());
    }
}
