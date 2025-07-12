using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform _targetTransform;
    [SerializeField] Vector3 _offsetPosition = Vector3.zero;
    [SerializeField, Range(0.0f, 1.0f)] float _linearRatio = 0.9f;

    private void Start()
    {
        _offsetPosition = transform.position - _targetTransform.position;
    }

    private void LateUpdate()
    {
        if (_targetTransform == null) return;

        Vector3 position = _offsetPosition + _targetTransform.position;

        transform.position = Vector3.Lerp(transform.position, position, _linearRatio);
        transform.LookAt(_targetTransform.position);
    }
}
