using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using TMPro;

using UnityEngine;

public class FinalScore : MonoBehaviour {

  public TMP_Text finalScore;
  public TMP_Text highscore;

  private int actualScore;
  private int displayedScore = 0;

  void Awake() {
    GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
  }

  private void GameManagerOnGameStateChanged(GameManager.GameState newState) {
    switch (newState) {
      case GameManager.GameState.Playing:
        break;
      case GameManager.GameState.GameOver:
        HandleGameOver();
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }
  }

  private void HandleGameOver() {
    actualScore = ScoreManager.instance.Score;
    if (this != null) {
      StartCoroutine(DisplayFinalScore());

      int oldHighscore = PlayerPrefs.GetInt("myHighscore", 0);
      if (actualScore > oldHighscore) PlayerPrefs.SetInt("myHighscore", actualScore);

      highscore.text = "Highscore " + PlayerPrefs.GetInt("myHighscore");

      //if (!Directory.Exists(Application.persistentDataPath + "/myData")) Directory.CreateDirectory(Application.persistentDataPath + "/myData");
      //if (!File.Exists(Application.persistentDataPath + "/myData/myHighscore.txt")) {
      //  File.Create(Application.persistentDataPath + "/myData/myHighscore.txt");
      //  File.WriteAllText(Application.persistentDataPath + "/myData/myHighscore.txt", "0");
      //}

      //int oldHighscore = Int32.Parse(File.ReadAllText(Application.persistentDataPath + "/myData/myHighscore.txt"));
    }
  }

  private IEnumerator DisplayFinalScore() {
    while (displayedScore <= actualScore) {
      finalScore.text = displayedScore.ToString();
      displayedScore++;
      yield return new WaitForSeconds(0.05f);
    }
  }
}
