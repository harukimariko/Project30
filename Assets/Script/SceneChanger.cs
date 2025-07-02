using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //public static SceneChanger INSTANCE { get; private set; }
    [SerializeField, Range(0.0f, 5.0f)] float _offsetTime = 1.0f;

    private void Awake()
    {
        //if (INSTANCE == null)
        //{
        //    INSTANCE = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    public void SetOffsetTime(float time)
    {
        _offsetTime = time;
    }

    public void ChangeSceneCoroutine(string scene)
    {
        StartCoroutine(ChangeScene(scene));
    }

    private IEnumerator ChangeScene(string scene)
    {
        yield return new WaitForSeconds(_offsetTime);
        SceneManager.LoadScene(scene);
    }
}