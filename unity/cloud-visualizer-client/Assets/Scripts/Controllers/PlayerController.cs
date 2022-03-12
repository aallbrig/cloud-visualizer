using System;
using Control;
using Core.Touch;
using Generated;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {

        public float moveSpeed = 5f;
        [Range(0f, 5f)] public float minimumSwipeDistance = 1f;
        public int movementLayer = 8;
        [SerializeField] private TouchInteraction end;
        [SerializeField] private TouchInteraction start;
        [SerializeField] private Swipe swipe;
        private Vector3 _moveTarget;
        private Player _playerInputActions;
        private int _layerMask;

        private bool ShouldMove => _moveTarget != Vector3.zero && Vector3.Distance(transform.position, _moveTarget) > 0.2f;

        private void Awake() => _playerInputActions = new Player();
        private void Start()
        {
            _layerMask = 1 << movementLayer;
            _playerInputActions.Gameplay.Touch.started += OnTouchStarted;
            _playerInputActions.Gameplay.Touch.canceled += OnTouchEnd;
            SwipeOccurred += OnSwipe;
            TapOccurred += OnTap;
        }
        private void Update()
        {
            if (ShouldMove)
            {
                var offset = _moveTarget - transform.position;
                if (offset.magnitude > .1f)
                {
                    var moveStep = offset.normalized * moveSpeed;
                    transform.position += moveStep * Time.deltaTime;
                }
            }
        }
        private void OnEnable() => _playerInputActions.Enable();
        private void OnDisable() => _playerInputActions.Disable();
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (start != null && Time.time - start.timing < 5f)
                Gizmos.DrawWireSphere(start.position, 1.5f);
            if (end != null)
                Gizmos.DrawWireSphere(end.position, 1.5f);
            if (swipe != null && end.timing < 5f)
                Gizmos.DrawLine(start.position, end.position);
            if (_moveTarget != Vector3.zero)
                Gizmos.DrawWireSphere(_moveTarget, 1);
        }

        public static event Action<Swipe> SwipeOccurred;

        public static event Action<TouchInteraction> TapOccurred;

        public static void TriggerSwipe(Swipe swipe) => SwipeOccurred?.Invoke(swipe);
        public static void TriggerTap(TouchInteraction end) => TapOccurred?.Invoke(end);
        private void OnTap(TouchInteraction touch)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(end.position), out var hit, 100f, _layerMask))
            {
                var invisibleGroundPlane = hit.transform.GetComponent<InvisibleGroundPlane>();
                if (invisibleGroundPlane) _moveTarget = hit.point;
            }
        }
        private void OnSwipe(Swipe swipe)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(end.position), out var hit, 100f))
            {
                var invisibleGroundPlane = hit.transform.GetComponent<InvisibleGroundPlane>();
                if (invisibleGroundPlane) _moveTarget = hit.point;
            }
        }
        private void OnTouchStarted(InputAction.CallbackContext context) =>
            start = TouchInteraction.Of(_playerInputActions.Gameplay.Position.ReadValue<Vector2>());

        private void OnTouchEnd(InputAction.CallbackContext obj)
        {
            end = TouchInteraction.Of(_playerInputActions.Gameplay.Position.ReadValue<Vector2>());
            swipe = Swipe.Of(start, end);

            if (swipe.distance > minimumSwipeDistance) TriggerSwipe(swipe);
            else TriggerTap(end);
        }
    }
}