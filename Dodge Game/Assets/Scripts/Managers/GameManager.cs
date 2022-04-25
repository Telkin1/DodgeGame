using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public static GameManager instance;

  public GameState State;

  public static event Action<GameState> OnGameStateChanged;

  void Awake() {
    if (GameManager.instance == null) GameManager.instance = this;
    else Destroy(this);
  }

  void Start() {
    UpdateGameState(GameState.Playing);
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

  public enum GameState {
    Playing,
    GameOver
  }
}
