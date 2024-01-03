using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dialogue
{
    public abstract class DialogueAction : MonoBehaviour
    {
        protected virtual void Awake()
        {
            if (gameObject.scene == SceneManager.GetActiveScene())
                return;
            
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
        }
        
        protected virtual void Start()
        {
            Perform();
        }

        protected abstract void Perform();
    }
}