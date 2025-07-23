using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager INSTANCE = null;
    public static float _masterVolume = 1.0f;
    public static float _bgmVolume = 0.8f;
    public static float _seVolume = 0.8f;

    public enum AudioType
    {
        SE,
        BGM
    }

    [Serializable]
    public class AudioInstance
    {
        public string _name = "None";
        public AudioType _type = AudioType.SE;
        public AudioSource _source = null;
    }

    [SerializeField] private List<AudioInstance> _audioList;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(!INSTANCE)
        {
            INSTANCE = this;
            INSTANCE.Initialize();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            INSTANCE.Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // èâä˙âªä÷êî
    public void Initialize()
    {
        
    }

    // âπÇÉäÉXÉgÇ©ÇÁíTÇµÇƒçƒê∂Ç∑ÇÈÇæÇØÇÃèàóù
    public void PlayAudio(string name)
    {
        foreach (var audio in _audioList)
        {
            if (name == audio._name)
            {
                float volume = 0.0f;

                switch (audio._type)
                {
                    case AudioType.SE:
                        volume = _seVolume * _masterVolume;
                        break;
                    case AudioType.BGM:
                        volume = _bgmVolume * _masterVolume;
                        break;
                }

                if (audio._source)
                {
                    audio._source.volume = volume;
                    audio._source.Play();
                }
            }
        }
    }
}
