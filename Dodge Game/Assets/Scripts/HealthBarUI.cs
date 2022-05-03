using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviourExtensions {

  public EnemySpawner healthBoxSpawner;
  public GameObject gameOverPanel;

  public int health;
  public int numOfHearts;

  public Image[] hearts;
  public Sprite fullHeart;
  public Sprite emtpyHeart;

  private bool playerDead = false;

  public void AddLive(int lives) {
    health += lives;

    if (health > numOfHearts) health = numOfHearts;
    if (health <= 0) {
      health = 0;
      PlayerDeath();
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

      PopEffect(hearts[i].gameObject, 1.5f, 0.8f, 0f, 0.1f);
    }

    if (health < hearts.Length) healthBoxSpawner?.StartSpawning();
    else healthBoxSpawner?.StopSpawning();
  }

  private void PlayerDeath() {
    if (playerDead) return;
    playerDead = true;
    gameOverPanel.SetActive(true);

    var player = GameObject.Find("Player");

    player.GetComponent<PlayerMovement>().activeMovement = false;
    var rb = player.GetComponent<Rigidbody2D>();
    Physics2D.gravity = new Vector2();

    rb.gravityScale = 1;
    rb.AddForce(Vector2.MoveTowards(rb.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 10f) * 50);

    GameObject.Find("GameWall").GetComponent<EdgeCollider2D>().sharedMaterial = new PhysicsMaterial2D("b_test") {bounciness = 0.75f, friction = 0};

    GameManager.instance?.UpdateGameState(GameManager.GameState.GameOver);
  }
}
