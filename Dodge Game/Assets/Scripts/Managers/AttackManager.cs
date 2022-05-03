using Assets.Scripts.Object_Spawners;
using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class AttackManager : MonoBehaviour {
  public static AttackManager instance;

  public int newWaveAt = 3;

  public ObjectSpawner FallingEnemySpawner;
  public ObjectSpawner FloatingBombSpawner;
  public ObjectSpawner LaserSpawner;

  private int playerDifficulty;
  private AttackState State;

  void Awake() {
    if (AttackManager.instance == null) AttackManager.instance = this;
    else Destroy(this);

    GameManager.instance.OnDifficultyChanged += GameManagerOnDifficultyChanged;
  }

  void Start()
  {
    UpdateAttackState((AttackState)Random.Range(0, 2));
  }

  private void GameManagerOnDifficultyChanged(int steps) {
    FallingEnemySpawner.IncreaseDifficulty(steps);
    FloatingBombSpawner.IncreaseDifficulty(steps);
    LaserSpawner.IncreaseDifficulty(steps);
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
    } while ((excludeCurrent & newState == currState) || newState == (int)AttackState.NoneOfEm);

    UpdateAttackState((AttackState)newState);
  }

  public void UpdateAttackState(AttackState newState) {
    FallingEnemySpawner.StopSpawning();
    FloatingBombSpawner.StopSpawning();
    LaserSpawner.StopSpawning();

    State = newState;

    switch (newState) {
      case AttackState.FallingNormal:
        HandleFallingNormalState();
        break;
      case AttackState.FloatingBombs:
        HandleFloatingBombState();
        break;
      case AttackState.LaserSpawn:
        HandleLaserSpawn();
        break;
      case AttackState.NoneOfEm:
        HandleNoAttackState();
        break;
      default:
        Debug.LogError("Unknown AttackState in UpdateAttackState()");
        break;
    }
  }

  private void HandleNoAttackState()
  {
    // Do nothing
  }

  private void HandleFloatingBombState() {
    FloatingBombSpawner.StartSpawning();
  }

  private void HandleFallingNormalState() {
    FallingEnemySpawner.StartSpawning();
  }

  private void HandleLaserSpawn() {
    LaserSpawner.StartSpawning();
  }

  public enum AttackState {
    FallingNormal,
    FloatingBombs,
    LaserSpawn,
    NoneOfEm
  }
}
