using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class ScoreManager : MonoBehaviourExtensions {

  public static ScoreManager instance;

  public TMP_Text ScoreText;
  public int Score;

  void Awake() {
    if (ScoreManager.instance == null) ScoreManager.instance = this;
    else Destroy(this);
  }

  public void AddScore(int score) {
    Score += 1;
    ScoreText.text = Score.ToString();
    PopEffect(ScoreText.gameObject, 2, 1, 0.05f, 0.15f);

    if (Score % 3 == 0) AttackManager.instance.RandomAttackStage();
  }
}
