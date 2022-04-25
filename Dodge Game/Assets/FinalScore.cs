using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class FinalScore : MonoBehaviour {

  public TMP_Text finalScore;

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
    if (this != null)
      this.StartCoroutine(this.DisplayFinalScore());
  }

  private IEnumerator DisplayFinalScore() {
    while (displayedScore <= actualScore) {
      finalScore.text = displayedScore.ToString();
      displayedScore++;
      yield return new WaitForSeconds(0.05f);
    }
  }
}
