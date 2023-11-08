using Enums.Player;
using Misc;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenWorldSpaceCanvas : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Transform _cursor;
        [SerializeField] private Transform _targetObject;
        
        [Header("Settings")]
        [SerializeField] private float _mouseSpeed = 0.35f;
        [SerializeField] private float _screenBoundariesOffset = .125f;
        
        private RectTransform _rectTransform;
        private Vector3[] _rectCorners;

        private void Awake()
        {
            _rectCorners = new Vector3[4];
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.GetLocalCorners(_rectCorners);

            ApplyOffset();
        }

        private void Update()
        {
            if (Player.Player.Instance.State != PlayerState.UsingPC)
                return;

            MoveCursor();
            print(RectOverlap(_cursor.GetComponent<RectTransform>().GetWorldRect(),
                _targetObject.GetComponent<RectTransform>().GetWorldRect()));
        }

        private bool WithingScreenBoundaries(Vector3 direction)
        {
            return direction.x >= _rectCorners[0].x && // Bottom left corner x
                   direction.x <= _rectCorners[2].x && // Top right corner x
                   direction.y >= _rectCorners[0].y && // Bottom left corner Y
                   direction.y <= _rectCorners[1].y;   // Top left corner y
        }
        
        private void MoveCursor()
        {
            float mouseX = Input.GetAxisRaw(Axis.MouseX);
            float mouseY = Input.GetAxisRaw(Axis.MouseY);

            var cursorMoveDirection = new Vector2(mouseX, mouseY) * (Time.deltaTime * _mouseSpeed);
            var targetDirection = new Vector3(_cursor.transform.localPosition.x + cursorMoveDirection.x,
                _cursor.transform.localPosition.y + cursorMoveDirection.y, _cursor.transform.localPosition.z);

            if (!WithingScreenBoundaries(targetDirection))
                return;
            
            _cursor.transform.localPosition = targetDirection;
        }

        private void ApplyOffset()
        {
            _rectCorners[0].x += _screenBoundariesOffset;
            _rectCorners[2].x -= _screenBoundariesOffset;
            _rectCorners[0].y += _screenBoundariesOffset;
            _rectCorners[1].y -= _screenBoundariesOffset;
        }

        private bool RectOverlap(Rect firstRect, Rect secondRect)
        {
            if (firstRect.x + firstRect.width*0.5f < secondRect.x - secondRect.width*0.5f)
            {
                return false;
            }
            if (secondRect.x + secondRect.width * 0.5f < firstRect.x - firstRect.width * 0.5f)
            {
                return false;
            }
            if (firstRect.y + firstRect.height * 0.5f < secondRect.y - secondRect.height * 0.5f)
            {
                return false;
            }
            if (secondRect.y + secondRect.height * 0.5f < firstRect.y - firstRect.height * 0.5f)
            {
                return false;
            }
            return true;
        }
    }
}
