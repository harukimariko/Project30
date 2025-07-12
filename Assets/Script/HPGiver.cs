using UnityEngine;

public class HPGiver : MonoBehaviour
{
    public Owner _owner = Owner.Enemy;
    [Range(-999, 999)] public int _applyValue = 1;
    [SerializeField, Range(0.0f, 10.0f)] private float _aliveTime = 0.0f;

    private void Start()
    {
        Destroy(gameObject, _aliveTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_owner == Owner.Enemy && other.gameObject.CompareTag("Enemy")) return;
        if (_owner == Owner.Player && other.gameObject.CompareTag("Player")) return;

        CharacterStatus cs = other.gameObject.GetComponent<CharacterStatus>();
        if (cs != null)
        {
            cs.ApplyHp(_applyValue);
        }
    }

    public void SetOwner(Owner owner) { _owner = owner; }
    public void SetApplyValue(int value) { _applyValue = value; }
}