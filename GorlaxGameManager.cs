using System.Linq;
using System.Collections;
using UnityEngine;
using Utilities;
using Datum;
using Scripting;

public class GorlaxGameManager : MonoBehaviour
{
    private NodeBehavior NodeBehavior
    {
        get { return nodeBehavior.Instantiate<NodeBehavior>(); }
    }

    [SerializeField]
    private LazyPrefab nodeBehavior;

    private IEnumerator Start()
    {
        var nodesRequest = new ScriptRequest<NodeDatum>(ResourcesConfig.NODES, DatumRequestType.Local);
        var tilesRequest = new ScriptRequest<TileDatum>(ResourcesConfig.TILES, DatumRequestType.Local);
        var animalsRequest = new ScriptRequest<Animal>(ResourcesConfig.ANIMALS, DatumRequestType.Local);

        yield return this.StartParallelCoroutines
        (
            tilesRequest,
            animalsRequest,
            nodesRequest
        );

        NodeBehavior.Datum = nodesRequest.Datum.Array.First();
        StartCoroutine(NodeBehavior.Show());
    }
}
