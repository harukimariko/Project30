using UnityEngine;
using System.Collections.Generic;

public class CharacterStatus : MonoBehaviour
{
    public string _name { get; private set; } = "none";
    [Range(1, 100)] public int _hpMax = 0;
    public int _hp { get; private set; } = 1;
    [SerializeField] private List<GameObject> _destroySpawnObjectList;

    private void Awake()
    {
        _hp = _hpMax;
    }

    private void Update()
    {
        
    }

    public void SetHp(int hp){_hp = hp; }
    public void ApplyHp(int apply)
    {
        _hp += apply;

        if (_hp <= 0)
        {
            foreach(GameObject obj in _destroySpawnObjectList)
            {
                Vector3 pos = transform.position + obj.transform.position;
                Quaternion rot = obj.transform.rotation;

                GameObject spawnObject = Instantiate(obj, pos, rot);
            }
        }
    }
    public void SetName(string name) { _name = name; }

}