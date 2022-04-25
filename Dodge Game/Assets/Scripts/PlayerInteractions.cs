using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

  public HealthBarUI healthBarUI;
  public ObjectSpawner EnemySpawner;

  public AudioClip PlayerHurtAudio;
  public AudioClip PickupCoinAudio;

  private AudioSource audioSource;

  private int invinciblePeriod = 0;

  void Awake() {
    audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
  }

  void OnTriggerEnter2D(Collider2D coll) {
    if (coll.tag == "Enemy") {
      PlayerHit();
    }
  }

  void OnCollisionEnter2D(Collision2D collision) {
    switch (collision.gameObject.tag) {
      case "Enemy":
        if (invinciblePeriod == 0) {
          collision.gameObject.GetComponent<FallingObject>()?.Kill();
        }
        PlayerHit();
        break;
      case "Gold":
        audioSource.PlayOneShot(PickupCoinAudio);
        collision.gameObject.GetComponent<FallingObject>()?.Kill();
        ScoreManager.instance.AddScore(1);
        GameManager.instance.UpdateDifficulty(1);
        break;
    }

    if (collision.gameObject.name.StartsWith("HealthBox")) {
      collision.gameObject.GetComponent<FallingObject>()?.Kill();
      healthBarUI.AddLive(1);
    }
  }

  private void PlayerHit() {
    if (invinciblePeriod == 0) {
      invinciblePeriod = 150;
      
      healthBarUI.AddLive(-1);

      audioSource.PlayOneShot(PlayerHurtAudio);
      Camera.main.gameObject.GetComponent<ScreenShakeBehaviour>().TriggerShake(0.3f);

      var sr = GetComponent<SpriteRenderer>();
      sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.7f);
    }
  }

  void FixedUpdate() {
    if (invinciblePeriod <= 0) return;
    invinciblePeriod -= 1;
    if (invinciblePeriod == 0) {
      var sr = GetComponent<SpriteRenderer>();
      sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + 0.7f);
    }
  }

  void Update() {
    if (Input.GetKey(KeyCode.S)) {
      ScoreManager.instance.AddScore(1);
    }
  }
}
