using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneState;

public class SceneChanger : MonoBehaviour
{
    [SerializeField, Range(0.0f, 5.0f)] float _offsetTime = 1.0f;

    private void Awake()
    {

    }

    public void SetOffsetTime(float time) { _offsetTime = time; }
    public void SetOffsetTimeScale(float time) { Time.timeScale = time; }

    public void ChangeSceneCoroutine(string scene)
    {
        StartCoroutine(ChangeScene(scene));
    }

    private IEnumerator ChangeScene(string scene)
    {
        Debug.Log("ÉVÅ[ÉìÇ™ïœÇÌÇÈÇÊ");
        yield return new WaitForSeconds(_offsetTime);
        SceneManager.LoadScene(scene);
    }
}