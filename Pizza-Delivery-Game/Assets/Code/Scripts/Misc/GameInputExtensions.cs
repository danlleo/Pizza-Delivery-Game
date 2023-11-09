using UnityEngine;

namespace Misc
{
    public static class GameInputExtensions
    {
        /// <param name="gameInput"></param>
        /// <param name="actionMap">Suggested to use nameof() method to pass a parameter</param>
        public static void SetDefaultActionMap(this GameInput gameInput, string actionMap)
        {
            int penalty = 0;
            
            for (int i = 0; i < gameInput.asset.actionMaps.Count; i++)
            {
                if (gameInput.asset.actionMaps[i].name != actionMap)
                {
                    penalty++;
                    continue;
                }
                
                break;
            }

            if (penalty == gameInput.asset.actionMaps.Count)
            {
                Debug.LogError("Action map wasn't found");
                return;
            }

            gameInput.asset.FindActionMap(actionMap).Enable(); 
            
            for (int i = 0; i < gameInput.asset.actionMaps.Count; i++)
            {
                if (gameInput.asset.actionMaps[i].name == actionMap) continue;
                
                gameInput.asset.FindActionMap(gameInput.asset.actionMaps[i].name).Disable();
            }
        }
    }
}