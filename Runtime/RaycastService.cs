using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Scripting;

public class RaycastService : IRaycastService
{
    private Camera _camera;

    private const float _maxRaycastDistance = 1000;

    private UnityEvent<GameObject> onRaycastHit = new UnityEvent<GameObject>();
    private EventSystem _eventSystem;

    private IRaycastable _raycastTarget;

    [Preserve]
    public RaycastService(Camera camera, EventSystem eventSystem)
    {
        _eventSystem = eventSystem;
        _camera = camera;
    }

    public void Raycast()
    {
        var gameObject = GetRaycastableGameObject();

        if (gameObject != null) onRaycastHit?.Invoke(gameObject);
    }

    public void Raycast(int layerMask)
    {
        var gameObject = GetRaycastableGameObject(layerMask);

        if (gameObject != null) onRaycastHit?.Invoke(gameObject);
    }

    public void AddOnRaycastHitListener(UnityAction<GameObject> action)
    {
        onRaycastHit.AddListener(action);
    }

    public void RemoveOnRaycastHitListener(UnityAction<GameObject> action)
    {
        onRaycastHit.RemoveListener(action);
    }

    private GameObject GetRaycastableGameObject()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit, _maxRaycastDistance)) return null;

        var raycastable = hit.collider.GetComponent<IRaycastable>();

        if (raycastable == null) return null;
        
        if (_raycastTarget == null) return hit.collider.gameObject;
        
        return _raycastTarget == raycastable ? hit.collider.gameObject : null;
    }
    

    private GameObject GetRaycastableGameObject(int layerMask)
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out hit, _maxRaycastDistance, layerMask)) return null;

        var raycastable = hit.collider.GetComponent<IRaycastable>();

        if (raycastable == null) return null;
        
        if (_raycastTarget == null) return hit.collider.gameObject;
        
        return _raycastTarget == raycastable ? hit.collider.gameObject : null;
    }

    public void SetRaycastTarget(IRaycastable raycastable)
    {
        _raycastTarget = raycastable;
    }

    public void RemoveRaycastTarget()
    {
        _raycastTarget = null;
    }
}