using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ListExtensions
    {
        public static void Log<T>(this List<T> messages)
        {
            foreach (T message in messages)
                Debug.Log(message);
        }
    }
}