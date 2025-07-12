using UnityEngine;
using SceneState;

public class GameManager : MonoBehaviour
{
    static public GameManager INSTANCE;

    [SerializeField] GameObject _player;
    public State _state { get; private set; } = State.Game;
    // 時間
    [SerializeField] float _timeScalePause = 0.0f;
    [SerializeField] float _timeScaleGame = 1.0f;
    [SerializeField] float _timeScaleSlow = 0.2f;
    [SerializeField] float _timeScaleOver = 0.4f;
    [SerializeField] float _timeScaleClear = 0.8f;

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

    public void SetState(State state)
    {
        _state = state;

        switch(_state)
        {
            case State.Pause:
                Time.timeScale = _timeScalePause;
                break;
            case State.Slow:
                Time.timeScale = _timeScaleSlow;
                break;
            case State.Game:
                Time.timeScale = _timeScaleGame;
                break;
            case State.Over:
                Time.timeScale = _timeScaleOver;
                break;
            case State.Clear:
                Time.timeScale = _timeScaleClear;
                break;
        }

    }
}