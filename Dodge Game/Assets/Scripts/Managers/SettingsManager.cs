using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

  public Slider volumeSlider;
  public TMP_Dropdown difficultyDropdown;
  public TMP_Dropdown controlsDropdown;

  void Start() {
    volumeSlider.value = PlayerPrefs.GetFloat("audioVolume", 1);
    difficultyDropdown.value = PlayerPrefs.GetInt("playerDifficulty", 1);
    controlsDropdown.value = PlayerPrefs.GetInt("playerControls", 0);
  }

  public void SetVolume() {
    float volume = volumeSlider.value;

    if (volume > 1) volume = 1;
    if (volume < 0) volume = 0;

    PlayerPrefs.SetFloat("audioVolume", volume);
  }

  public void SetDifficulty() {
    int difficulty = difficultyDropdown.value;

    PlayerPrefs.SetInt("playerDifficulty", difficulty);
  }

  public void SetControls() {
    int controls = controlsDropdown.value;

    PlayerPrefs.SetInt("playerControls", controls);
  }
}
