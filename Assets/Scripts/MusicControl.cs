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
    [SerializeField] private Image MusicSprite;
    [SerializeField] private Sprite MusicOffSprite;
    [SerializeField] private Sprite MusicOnSprite;
    // Start is called before the first frame update
    private void Start()
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
            MusicSprite.sprite = MusicOffSprite;
        }
        else
        {
            IsMusicOn = true;
            Music.mute = false;
            MusicSprite.sprite = MusicOnSprite;
        }
    }
}
