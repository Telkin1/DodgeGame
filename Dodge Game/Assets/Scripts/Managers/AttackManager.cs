using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class AttackManager : MonoBehaviour {
  public static AttackManager instance;

  public int newWaveAt = 3;

  public ObjectSpawner FallingEnemySpawner;
  public ObjectSpawner FloatingBombSpawner;
  public LaserSpawner LaserSpawner;

  private int playerDifficulty;
  private AttackState State;

  void Awake() {
    if (AttackManager.instance == null) AttackManager.instance = this;
    else Destroy(this);

    playerDifficulty = PlayerPrefs.GetInt("playerDifficulty", 1);
    playerDifficulty = Mathf.Abs(playerDifficulty - 2);
    GameManager.instance.OnDifficultyChanged += GameManagerOnDifficultyChanged;
  }

  private void GameManagerOnDifficultyChanged(float difficulty) {
    FallingEnemySpawner.spawnRate = 0.4f - (difficulty * 0.006f) + playerDifficulty * 0.2f;
    if (FallingEnemySpawner.spawnRate < 0.02) FallingEnemySpawner.spawnRate = 0.02f;

    FloatingBombSpawner.spawnRate = 0.4f - (difficulty * 0.006f) + playerDifficulty * 0.2f;
    if (FloatingBombSpawner.spawnRate < 0.02) FloatingBombSpawner.spawnRate = 0.02f;

    LaserSpawner.spawnRate = 1f - (difficulty * 0.006f) + playerDifficulty * 0.2f;
    if (LaserSpawner.spawnRate < 0.02) LaserSpawner.spawnRate = 0.02f;
  }

  void Start() {
    FallingEnemySpawner.StopSpawning();
    
    UpdateAttackState((AttackState)Random.Range(0, 2));
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

  private void HandleLaserSpawn() {
    LaserSpawner.StartSpawning();
  }

  public enum AttackState {
    FallingNormal,
    FloatingBombs,
    LaserSpawn
  }
}
