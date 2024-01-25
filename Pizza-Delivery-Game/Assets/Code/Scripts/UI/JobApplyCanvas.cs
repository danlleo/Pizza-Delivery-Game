using Common;
using UnityEngine;

namespace UI
{
    [DisallowMultipleComponent]
    public class JobApplyCanvas : MonoBehaviour
    {
        [SerializeField, ChildrenOnly] private GameObject _jobApplyContainer;
    }
}
