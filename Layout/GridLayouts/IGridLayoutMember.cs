namespace Utilities
{
    using System.Collections;

    using UnityEngine;

    public interface IGridLayoutMember
    {
        IEnumerator SetPosition(Vector3 position);
    }
}