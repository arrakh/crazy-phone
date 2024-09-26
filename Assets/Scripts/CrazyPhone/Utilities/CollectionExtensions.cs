using System.Collections.Generic;
using UnityEngine;

namespace CrazyPhone.Utilities
{
    public static class CollectionExtensions
    {
        public static T GetRandom<T>(this IReadOnlyList<T> collection)
            => collection[Random.Range(0, collection.Count)];
    }
}