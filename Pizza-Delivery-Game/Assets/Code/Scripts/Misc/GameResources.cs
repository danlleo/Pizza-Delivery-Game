using UnityEngine;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameResources : MonoBehaviour
    {
        private static GameResources s_instance;

        public static GameResources Instance
        {
            // We get data from a prefab
            // With this we can easily share data between scenes, cause everything is located in resource folder

            get
            {
                if (s_instance == null)
                    s_instance = Resources.Load<GameResources>("GameResources");

                return s_instance;
            }
        }

        public Door.Door TestDoor;
    }
}