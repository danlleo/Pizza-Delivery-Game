using System;
using System.Collections.Generic;
using Enums.PC;
using Enums.Player;
using Interfaces;
using Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Environment.Bedroom.PC
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    public class ScreenWorldSpaceCanvas : Singleton<ScreenWorldSpaceCanvas>
    {
        [Header("External references")]
        [SerializeField] private Transform _cursor;
        [SerializeField] private List<Clickable> _clickableObjects;

        [Header("Settings")] 
        [SerializeField] private CursorState _defaultCursorState;
        [SerializeField] private Sprite _defaultCursorSprite;
        [SerializeField] private Sprite _pointingCursorSprite;
        
        [Space(5)]
        [SerializeField] private float _mouseSpeed = 0.35f;
        [SerializeField] private float _screenBoundariesOffset = .125f;
        
        private RectTransform _screenRectTransform;
        private RectTransform _cursorRectTransform;
        
        private CursorState _currentCursorState;
        private Image _cursorImage;
        
        private Vector3[] _rectCorners;
        private List<RectTransform> _clickableObjectRectTransforms;

        private bool _isHovering;

        private IClickable _clickable;

        protected override void Awake()
        {
            base.Awake();
            
            _rectCorners = new Vector3[4];
            _cursorImage = _cursor.GetComponent<Image>();
            _cursorRectTransform = _cursor.GetComponent<RectTransform>();
            _screenRectTransform = GetComponent<RectTransform>();
            _screenRectTransform.GetLocalCorners(_rectCorners);
            _clickableObjectRectTransforms = GetClickableObjectRectTransforms();
            
            HandleCursorChange(_defaultCursorState);
            ApplyOffset();
        }

        private void OnEnable()
        {
            ClickedStaticEvent.OnClicked += ClickedStaticEvent_OnClicked;
        }

        private void OnDisable()
        {
            ClickedStaticEvent.OnClicked -= ClickedStaticEvent_OnClicked;
        }

        private void Update()
        {
            if (Player.Player.Instance.State != PlayerState.UsingPC)
                return;

            MoveCursor();
            TryOverlapWithClickableObjects();
        }

        public void RemoveClickableObject(Clickable clickable)
        {
            _clickableObjects.Remove(clickable);
            _clickableObjectRectTransforms.Remove(clickable.GetComponent<RectTransform>());
            _clickable = null;
            
            print("Fartick");
        }
        
        private void MoveCursor()
        {
            float mouseX = Input.GetAxisRaw(Axis.MouseX);
            float mouseY = Input.GetAxisRaw(Axis.MouseY);

            var cursorMoveDirection = new Vector2(mouseX, mouseY) * (Time.deltaTime * _mouseSpeed);
            var localPosition = _cursor.transform.localPosition;
            var targetDirection = new Vector3(localPosition.x + cursorMoveDirection.x,
                localPosition.y + cursorMoveDirection.y, localPosition.z);

            if (!WithingScreenBoundaries(targetDirection))
                return;
            
            _cursor.transform.localPosition = targetDirection;
        }

        private void TryOverlapWithClickableObjects()
        {
            Rect cursorRect = GetScreenObjectRect(_cursorRectTransform);

            if (_clickableObjectRectTransforms.Count == 0)
            {
                _clickable = null;
                _isHovering = false;

                HandleCursorChange(CursorState.Default);
                
                return;
            }
            
            for (int i = 0; i < _clickableObjectRectTransforms.Count; i++)
            {
                Rect targetRect = GetScreenObjectRect(_clickableObjectRectTransforms[i]);

                if (!IsOverlappingWithRect(cursorRect, targetRect)) continue;
                
                HandleCursorChange(CursorState.Pointing);

                _clickable = _clickableObjectRectTransforms[i].GetComponent<IClickable>();
                _isHovering = true;

                return;
            }

            _clickable = null;
            _isHovering = false;

            HandleCursorChange(CursorState.Default);
        }

        private List<RectTransform> GetClickableObjectRectTransforms()
        {
            var rectTransforms = new List<RectTransform>();
            
            for (int i = 0; i < _clickableObjects.Count; i++)
            {
                var targetRect = _clickableObjects[i].GetComponent<RectTransform>();
                rectTransforms.Add(targetRect);
            }

            return rectTransforms;
        }
        
        private void HandleCursorChange(CursorState targetState)
        {
            _currentCursorState = targetState;

            _cursorImage.sprite = _currentCursorState switch
            {
                CursorState.Default => _defaultCursorSprite,
                CursorState.Pointing => _pointingCursorSprite,
                _ => throw new ArgumentOutOfRangeException()
            };
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
            Vector3 position = target.GetWorldRectPosition();

            var targetPosition = new Vector2(-position.z, position.y);
            var rect = new Rect(targetPosition, target.rect.size);

            return rect;
        }

        private void ApplyOffset()
        {
            _rectCorners[0].x += _screenBoundariesOffset;
            _rectCorners[2].x -= _screenBoundariesOffset;
            _rectCorners[0].y += _screenBoundariesOffset;
            _rectCorners[1].y -= _screenBoundariesOffset;
        }

        #region Click Events

        private void ClickedStaticEvent_OnClicked(object sender, EventArgs e)
        {
            if (!_isHovering) return;
            
            _clickable.HandleClick();
        }

        #endregion
    }
}
