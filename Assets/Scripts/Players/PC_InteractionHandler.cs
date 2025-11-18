using UnityEngine;

public class PC_InteractionHandler : MonoBehaviour
{

    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private Color _gizmoColor = Color.green;
    [SerializeField] private float _gizmoSphereRadius = 0.2f;

    [Header("Camera Settings")]
    [SerializeField] private string _cameraTag = "MainCamera"; // Option 1: Par tag

    private Camera _interactionCamera;
    private Vector2? _lastTouchPos;
    private RaycastHit2D? _lastHit;

    private void Awake()
    {
        InitializeCamera();
    }

    private void InitializeCamera()
    {
        if (!string.IsNullOrEmpty(_cameraTag))
        {
            GameObject taggedCam = GameObject.FindWithTag(_cameraTag);
            if (taggedCam != null && taggedCam.TryGetComponent(out Camera cam))
            {
                _interactionCamera = cam;
                return;
            }
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            CheckInteraction();
        }
    }

    private void CheckInteraction()
    {
        if (_interactionCamera == null) return;

        Vector2 touchPos = _interactionCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
        RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero, Mathf.Infinity, _interactableLayer);

        _lastTouchPos = touchPos;
        _lastHit = hit.collider != null ? hit : (RaycastHit2D?)null;

        if (hit.collider != null)
        {
            OBJ_Objects obj = hit.collider.GetComponent<OBJ_Objects>();
            if (obj != null && obj.IsInteractable &&
                Vector2.Distance(transform.position, hit.transform.position) <= obj.InteractionRange)
            {
                obj.Interact();
            }
        }
    }
}
