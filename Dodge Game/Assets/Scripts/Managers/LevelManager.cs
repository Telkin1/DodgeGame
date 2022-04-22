using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
  public static LevelManager instance;

  void Awake() {
    if (LevelManager.instance == null) LevelManager.instance = this;
    else Destroy(this);
  }

  public void LoadSceneByName(string name) {
    SceneManager.LoadScene(SceneManager.GetSceneByName(name).buildIndex);
  }

  public void ReloadCurrentScene() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
