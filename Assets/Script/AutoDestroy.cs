using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField, Range(0.0f, 10.0f)] private float _aliveTime = 0.0f;
    [SerializeField] bool _isAutoDestroy = false;

    private void Awake()
    {
        if (_isAutoDestroy) StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(_aliveTime);
        Destroy(gameObject);
    }
}