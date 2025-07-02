using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform _targetTransform;
    [SerializeField] Vector3 _offsetPosition = Vector3.zero;

    private void Start()
    {
        _offsetPosition = transform.position - _targetTransform.position;
    }

    private void LateUpdate()
    {
        transform.position = _offsetPosition + _targetTransform.position;
        transform.LookAt(_targetTransform.position);
    }
}
