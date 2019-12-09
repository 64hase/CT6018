using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MusicControl : MonoBehaviour
{
    [SerializeField] private Button MusicButton;
    private bool IsMusicOn = true;
    private AudioSource Music;
    // Start is called before the first frame update
    void Start()
    {
        MusicButton.onClick.AddListener(OnMusicSwitch);
        Music = EventSystem.current.GetComponent<AudioSource>();
    }
    private void OnMusicSwitch()
    {
        if (IsMusicOn == true)
        {
            IsMusicOn = false;
            Music.mute = true;
        }
        else
        {
            IsMusicOn = true;
            Music.mute = false;
        }
    }
}
