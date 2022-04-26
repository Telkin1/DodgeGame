using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour {
  public static GameManager instance;

  public GameState State;

  public static event Action<GameState> OnGameStateChanged;

  public event Action<float> OnDifficultyChanged;
  private float difficulty;

  void Awake() {
    if (GameManager.instance == null) GameManager.instance = this;
    else Destroy(this);
  }

  void Start() {
    UpdateGameState(GameState.Playing);
    UpdateDifficulty(0);
  }

  public void UpdateGameState(GameState newState) {
    State = newState;

    switch (newState) {
      case GameState.Playing:
        break;
      case GameState.GameOver:
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }

    OnGameStateChanged?.Invoke(newState);
  }

  public void UpdateDifficulty(float modifier) {
    difficulty += modifier;
    OnDifficultyChanged?.Invoke(difficulty);
  }

  public enum GameState {
    Playing,
    GameOver
  }
}
