using Interfaces;
using UnityEngine;

namespace Environment.Bedroom.PC.Programs
{
    [DisallowMultipleComponent]
    public class Browser : MonoBehaviour, IClickable
    {
        public void HandleClick()
        {
            print("You clicked on a browser");
        }
    }
}
