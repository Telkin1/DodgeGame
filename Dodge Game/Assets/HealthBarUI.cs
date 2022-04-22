using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviourExtensions {

  public ObjectSpawner healthBoxSpawner;

  public int health;
  public int numOfHearts;

  public Image[] hearts;
  public Sprite fullHeart;
  public Sprite emtpyHeart;

  public void AddLive(int lives) {
    health += lives;

    if (health > numOfHearts) health = numOfHearts;
    if (health <= 0) {
      health = 0;
      LevelManager.instance.ReloadCurrentScene();
    }

    for (int i = 0; i < hearts.Length; i++) {

      if (i < health) {
        hearts[i].sprite = fullHeart;
      } else {
        hearts[i].sprite = emtpyHeart;
      }

      if (i < numOfHearts) {
        hearts[i].enabled = true;
      } else {
        hearts[i].enabled = false;
      }

      PopEffect(hearts[i].gameObject, 1.5f, 1f, 0f, 0.1f);
    }

    if (health < hearts.Length) healthBoxSpawner.StartSpawning();
    else healthBoxSpawner.StopSpawning();
  }
}
