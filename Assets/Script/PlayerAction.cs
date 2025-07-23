using UnityEngine;
using System.Collections.Generic;
using SceneState;

public class PlayerAction : MonoBehaviour
{
    public List<GameObject> _actionList = new List<GameObject>();
    [SerializeField, Range(0.0f, 20.0f)] private float _launchSpeed;
    [SerializeField] private Vector3 _direction = Vector3.forward;
    [SerializeField] private FuelTank _tank;
    [SerializeField] private float _fuelCost = -10.0f;

    private void Update()
    {
        if (GameManager.INSTANCE._state == State.Pause) return;

        _direction = transform.forward;

        if (Input.GetButtonDown("Fire1") && _tank.CheckCost(Mathf.Abs(_fuelCost)))
        {
            Action();
            _tank.AddFuel(_fuelCost);
        }
    }

    public void Action()
    {
        foreach (GameObject obj in _actionList)
        {
            Vector3 pos = transform.position + obj.transform.position;
            Quaternion rot = obj.transform.rotation;

            GameObject spawnObject = Instantiate(obj, pos, rot);

            Rigidbody rb = spawnObject.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.linearVelocity = _direction * _launchSpeed;
            }
        }
    }
}
