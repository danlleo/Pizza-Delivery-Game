using System;
using DataPersistence;
using UnityEngine.SceneManagement;
using Scene = Enums.Scenes.Scene;

namespace Misc.Loader
{
    public static class Loader
    {
        private static Action s_onLoaderCallback;
        
        public static void Load(Scene scene)
        {
            s_onLoaderCallback = () =>
            {
                SceneManager.LoadScene(scene.ToString());
            };

            SaveStaticEvent.Call(scene);
            SceneManager.LoadScene(Scene.LoadingScene.ToString());
        }

        public static void Callback()
        {
            if (s_onLoaderCallback == null) return;
            
            s_onLoaderCallback();
            s_onLoaderCallback = null;
        }
    }
}
