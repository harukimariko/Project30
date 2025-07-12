using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class CharacterStatus : MonoBehaviour
{
    public string _name { get; private set; } = "none";
    [Range(1, 100)] public int _hpMax = 0;
    public int _hp { get; private set; } = 1;
    [SerializeField] private List<GameObject> _destroySpawnObjectList;
    bool _isDeath = false;
    [SerializeField, Range(0.0f, 5.0f)] float _isDeathAliveTime = 1.0f;

    private void Awake()
    {
        _hp = _hpMax;
    }

    private void Update()
    {
        
    }

    public void SetName(string name) { _name = name; }

    public void SetHp(int hp){_hp = hp; }
    public virtual bool ApplyHp(int apply)
    {
        bool death = false;

        _hp += apply;

        if (_hp <= 0)
        {
            CallDestroy(_isDeathAliveTime);
            death = true;
        }

        _isDeath = death;

        return death;
    }

    public void CallDestroy(float time)
    {
        StartCoroutine(DestroyCharacter(time));
    }

    private IEnumerator DestroyCharacter(float time)
    {
        yield return new WaitForSeconds(time);

        // 死んだときに出るエフェクト
        foreach (GameObject obj in _destroySpawnObjectList)
        {
            Vector3 pos = transform.position + obj.transform.position;
            Quaternion rot = obj.transform.rotation;

            GameObject spawnObject = Instantiate(obj, pos, rot);
        }

        Destroy(gameObject); // 何秒後に消す
    }
}