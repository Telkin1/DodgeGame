using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackManager : MonoBehaviour {
  public static AttackManager instance;
  
  public ObjectSpawner FallingEnemySpawner;

  private AttackState State;

  void Awake() {
    if (AttackManager.instance == null) AttackManager.instance = this;
    else Destroy(this);
  }

  void Start() {
    FallingEnemySpawner.StopSpawning();
    
    UpdateAttackState(AttackState.FallingNormal);
  }

  private void UpdateAttackState(AttackState newState) {
    FallingEnemySpawner.StopSpawning();

    State = newState;

    switch (newState) {
      case AttackState.FallingNormal:
        HandleFallingNormalState();
        break;
      case AttackState.FloatingBombs:
        break;
      default:
        throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
    }
  }

  private void HandleFallingNormalState() {
    FallingEnemySpawner.StartSpawning();
  }

  public enum AttackState {
    FallingNormal,
    FloatingBombs
  }
}
