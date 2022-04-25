using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxScript : FallingObject
{
  public AudioClip Powerup;
  private AudioSource audioSource;

  void Start() {
    audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
  }

  new void OnCollisionEnter2D(Collision2D collision) {
    base.OnCollisionEnter2D(collision);

    if (collision.gameObject.name == "Player") audioSource.PlayOneShot(Powerup);
  }
}
