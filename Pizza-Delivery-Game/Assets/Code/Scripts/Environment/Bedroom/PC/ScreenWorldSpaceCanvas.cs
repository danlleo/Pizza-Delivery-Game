using Enums.Player;
using Misc;
using UnityEngine;

namespace Environment.Bedroom.PC
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class ScreenWorldSpaceCanvas : MonoBehaviour
    {
        [Header("External references")]
        [SerializeField] private Transform _cursor;
        [SerializeField] private Transform _targetObject;
        
        [Header("Settings")]
        [SerializeField] private float _mouseSpeed = 0.35f;
        [SerializeField] private float _screenBoundariesOffset = .125f;
        
        private RectTransform _screenRectTransform;
        private Vector3[] _rectCorners;

        private void Awake()
        {
            _rectCorners = new Vector3[4];
            _screenRectTransform = GetComponent<RectTransform>();
            _screenRectTransform.GetLocalCorners(_rectCorners);

            ApplyOffset();
        }

        private void Update()
        {
            if (Player.Player.Instance.State != PlayerState.UsingPC)
                return;

            MoveCursor();

            Rect cursorRect = GetScreenObjectRect(_cursor.GetComponent<RectTransform>());
            Rect targetRect = GetScreenObjectRect(_targetObject.GetComponent<RectTransform>());

            print(IsOverlappingWithRect(cursorRect, targetRect));
        }

        private bool WithingScreenBoundaries(Vector3 direction)
        {
            return direction.x >= _rectCorners[0].x && // Bottom left corner x
                   direction.x <= _rectCorners[2].x && // Top right corner x
                   direction.y >= _rectCorners[0].y && // Bottom left corner Y
                   direction.y <= _rectCorners[1].y;   // Top left corner y
        }

        private bool IsOverlappingWithRect(Rect anchorRect, Rect overlappingRect)
            => anchorRect.Overlaps(overlappingRect);

        private Rect GetScreenObjectRect(RectTransform target)
        {
            Vector3 position = target.position;
            
            var targetPosition = new Vector2(position.z, position.y);
            var rect = new Rect(targetPosition, target.rect.size);

            return rect;
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
    }
}
