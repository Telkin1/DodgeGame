using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
  void Start()
  {
    GameObject.Find("GameOverPanel").SetActive(false);

    try {
      Camera.main.gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("audioVolume", 1);
    } catch { 
      // ignored
    }
  }
}
