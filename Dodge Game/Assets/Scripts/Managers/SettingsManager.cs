using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

  public Slider volumeSlider;

  void Start() {
    volumeSlider.value = PlayerPrefs.GetFloat( "audioVolume", 1);
  }

  public void SetVolume() {
    float volume = volumeSlider.value;

    if (volume > 1) volume = 1;
    if (volume < 0) volume = 0;

    PlayerPrefs.SetFloat("audioVolume", volume);
  }
}