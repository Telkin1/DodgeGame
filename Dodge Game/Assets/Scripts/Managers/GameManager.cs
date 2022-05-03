using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
  public static GameManager instance;

  public GameState State;

  public static event Action<GameState> OnGameStateChanged;

  public event Action<int> OnDifficultyChanged;
  private int difficulty;

  void Awake() {
    if (GameManager.instance == null) GameManager.instance = this;
    else Destroy(this);
  }

  void Start() {
    UpdateGameState(GameState.Playing);
    difficulty = 0;
  }

  public void UpdateGameState(GameState newState) {
    State = newState;

    switch (newState) {
      case GameState.Playing:
        break;
      case GameState.GameOver:
        break;
      default:
        Debug.LogError("Unknown GameState " + newState + " in UpdateGameState()");
        break;
    }

    OnGameStateChanged?.Invoke(newState);
  }

  public void UpdateDifficulty(int steps) {
    difficulty += steps;
    OnDifficultyChanged?.Invoke(steps);
  }

  public enum GameState {
    Playing,
    GameOver
  }
}
