using System.Linq;
using System.Collections;
using UnityEngine;
using Utilities;

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
        yield return StartCoroutine(interactions);
        GameScreen.Datum = interactions.Data.First();
        StartCoroutine(GameScreen.Show());
    }
}
