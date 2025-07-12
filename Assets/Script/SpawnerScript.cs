using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] Collider _collider;
    [SerializeField, Range(0.0f, 2.0f)] private float _colliderFixTime = 0.0f;
    [SerializeField] private List<GameObject> _spawnList = new List<GameObject>();
    public Owner _owner { get; private set; } = Owner.Enemy;

    private void Awake()
    {
        if (_collider != null) _collider.enabled = false;
        StartCoroutine(SetCollier(true));
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnObject();
        Destroy(gameObject);
    }

    public void SpawnObject()
    {
        foreach (GameObject obj in _spawnList)
        {
            Vector3 pos = transform.position + obj.transform.position;
            Quaternion rot = obj.transform.rotation;
            GameObject spawnObject = Instantiate(obj, pos, rot);
        }

        Destroy(gameObject);
    }

    public void SetOwner(Owner owner) { _owner = owner; }

    private IEnumerator SetCollier(bool key)
    {
        yield return new WaitForSeconds(_colliderFixTime);
        if (_collider != null) { _collider.enabled = key; }
    }
}