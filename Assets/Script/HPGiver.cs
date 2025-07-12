using UnityEngine;

public class HPGiver : MonoBehaviour
{
    public Owner _owner { get; private set; } = Owner.Enemy;
    [Range(-999, 999)] public int _applyValue = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if (_owner == Owner.Enemy && collision.gameObject.CompareTag("Enemy")) return;
        if (_owner == Owner.Player && collision.gameObject.CompareTag("Player")) return;

        CharacterStatus cs = collision.gameObject.GetComponent<CharacterStatus>();
        if (cs != null)
        {
            cs.ApplyHp(_applyValue);
        }
    }

    public void SetOwner(Owner owner) { _owner = owner; }
    public void SetApplyValue(int value) { _applyValue = value; }
}