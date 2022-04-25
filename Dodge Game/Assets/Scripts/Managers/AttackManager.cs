using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

using Random = UnityEngine.Random;

public class AttackManager : MonoBehaviour {
  public static AttackManager instance;

  public int newWaveAt = 3;

  public ObjectSpawner FallingEnemySpawner;
  public ObjectSpawner FloatingBombSpawner;

  private AttackState State;

  void Awake() {
    if (AttackManager.instance == null) AttackManager.instance = this;
    else Destroy(this);

    GameManager.instance.OnDifficultyChanged += GameManagerOnDifficultyChanged;
  }

  private void GameManagerOnDifficultyChanged(float difficulty) {
    FallingEnemySpawner.spawnRate = 0.4f - (difficulty * 0.006f);
    if (FallingEnemySpawner.spawnRate < 0.02) FallingEnemySpawner.spawnRate = 0.02f;

    FloatingBombSpawner.spawnRate = 0.4f - (difficulty * 0.006f);
    if (FloatingBombSpawner.spawnRate < 0.02) FloatingBombSpawner.spawnRate = 0.02f;
  }

  void Start() {
    FallingEnemySpawner.StopSpawning();
    
    UpdateAttackState(AttackState.FallingNormal);
  }

  public void RandomAttackStage(bool excludeCurrent = true) {
    StartCoroutine(_RandomAttackStage(excludeCurrent));
  }

  private IEnumerator _RandomAttackStage(bool excludeCurrent) {
    yield return new WaitForSeconds(2);

    int currState = (int)State;

    int newState = currState;
    int tries = 0;
    do {
      newState = Random.Range(0, Enum.GetValues(typeof(AttackState)).Length);
      tries++;
      if (tries >= 5) break;
    } while (excludeCurrent & newState == currState);

    UpdateAttackState((AttackState)newState);
  }

  private void UpdateAttackState(AttackState newState) {
    FallingEnemySpawner.StopSpawning();
    FloatingBombSpawner.StopSpawning();

    State = newState;

    switch (newState) {
      case AttackState.FallingNormal:
        HandleFallingNormalState();
        break;
      case AttackState.FloatingBombs:
        HandleFloatingBombState();
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }
  }

  private void HandleFloatingBombState() {
    FloatingBombSpawner.StartSpawning();
  }

  private void HandleFallingNormalState() {
    FallingEnemySpawner.StartSpawning();
  }

  public enum AttackState {
    FallingNormal,
    FloatingBombs
  }
}
