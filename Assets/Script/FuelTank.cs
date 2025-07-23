using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class FuelTank : MonoBehaviour
{
    [SerializeField] private float _fuelVolume = 0.0f;
    [SerializeField] private float _fuelMax = 100.0f;
    public float GetFuelMax() { return _fuelMax; }
    [SerializeField] private TextViewer _textViewer;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _sliderValueAxis;
    [SerializeField] private float _sliderLerp = 0.95f;

    private void Awake()
    {
        ViewUI();
    }

    private void FixedUpdate()
    {
        _slider.value = Mathf.Lerp(_sliderValueAxis, _slider.value, _sliderLerp);
    }

    public void AddFuel(float volume)
    {
        _fuelVolume += volume;

        if (_fuelVolume > _fuelMax)
        {
            _fuelVolume = _fuelMax;
        }
        else if (_fuelVolume < 0.0f)
        {
            _fuelVolume = 0.0f;
        }

        ViewUI();
    }

    public bool CheckCost(float cost)
    {
        bool key = true;
        if (_fuelVolume < cost) key = false;
        return key;
    }

    // UI•\Ž¦
    private void ViewUI()
    {
        _textViewer.ChangeText(_fuelVolume);
        _sliderValueAxis = _fuelVolume / _fuelMax;
    }
}
