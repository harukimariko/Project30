using UnityEngine;
using UnityEngine.UI;

public class TextViewer : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private string _float;

    private void Awake()
    {
        if (!_text) _text = GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ChangeText(10.0f);
        }
    }

    public void ChangeText(float text)
    {
        _text.text = text.ToString("F" + _float);
    }
}
