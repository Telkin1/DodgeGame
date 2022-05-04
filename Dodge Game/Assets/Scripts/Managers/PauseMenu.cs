using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
  public GameObject PauseMenuPane;

  void Awake() {
    PauseMenuPane.SetActive(false);
  }

  public void PauseGame() {
    Time.timeScale = 0f;
    PauseMenuPane.SetActive(true);
  }

  public void ResumeGame() {
    Time.timeScale = 1f;
    PauseMenuPane.SetActive(false);
  }

  public void Menu() {
    Time.timeScale = 1f;
    LevelManager.instance.LoadSceneByName("Menu");
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Escape) & !PauseMenuPane.activeSelf) PauseGame();
  }
}
