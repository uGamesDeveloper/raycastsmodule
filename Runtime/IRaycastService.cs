using UnityEngine;
using UnityEngine.Events;

public interface IRaycastService : IService
{
    void Raycast();
    void Raycast(int layerMask);
    void AddOnRaycastHitListener(UnityAction<GameObject> action);
    void RemoveOnRaycastHitListener(UnityAction<GameObject> action);
    void RemoveRaycastTarget();
    void SetRaycastTarget(IRaycastable raycastable);
}