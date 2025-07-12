using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager INSTANCE;

    [SerializeField] GameObject _player;

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
            DontDestroyOnLoad(gameObject);


        }
        else
        {
            Destroy(this);
        }

        // プレイヤーがアタッチされていなかった場合
        if (_player == null) _player = GameObject.FindWithTag("Player");
    }


}