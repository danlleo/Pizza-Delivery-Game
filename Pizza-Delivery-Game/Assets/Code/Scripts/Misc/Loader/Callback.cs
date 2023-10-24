using UnityEngine;

namespace Misc.Loader
{
    public class Callback : MonoBehaviour
    {
        private bool _isFirstUpdate = true;

        private void Update()
        {
            if (!_isFirstUpdate) return;
            
            _isFirstUpdate = false;
            Loader.Callback();
        }
    }
}
