using UnityEngine;
using System.Collections;

public class GorlaxGameManager : GameBehavior {

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

    [SerializeField] private CharacterSource characterSource;
    [SerializeField] private NameSource nameSource;
    [SerializeField] private InteractionSource interactionSource;

    protected override IEnumerator OnStartCoroutine()
    {
        characterSource.RegisterSubscriber(CharacterSelectionScreen);
        interactionSource.RegisterSubscriber(GameScreen);

        yield return StartCoroutine(nameSource.FetchData());
        yield return StartCoroutine(characterSource.FetchData());
        yield return StartCoroutine(CharacterSelectionScreen.Show());
    }
}
