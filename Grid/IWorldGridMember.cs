namespace Utilities.Grid
{
    using System.Collections;

    using UnityEngine;

    public interface IWorldGridMember
    {
        IEnumerator SetPosition(Vector3 position);
    }
}
