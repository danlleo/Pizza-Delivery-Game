using UnityEngine;
using UnityEngine.UIElements;

namespace Misc
{
    [DisallowMultipleComponent]
    public class GameResources : MonoBehaviour
    {
        private static GameResources s_instance;

        public static GameResources Retrieve
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
        
        [field:SerializeField] public StyleSheet PopupWindowStylesheet {get; private set;}
        [field:SerializeField] public StyleSheet CreditsWindowStylesheet {get; private set;}
        [field:SerializeField] public StyleSheet SettingsWindowStylesheet {get; private set;}
    }
}
