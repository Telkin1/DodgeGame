using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : FallingObject {

  public AudioClip ExplosionAudio;
  private AudioSource audioSource;

  private CircleCollider2D circleColl;
  private float scale;
  private float timeStep = 0.02f;

  void Start() {
    circleColl = GetComponent<CircleCollider2D>();
    StartCoroutine(HandleSpawning());

    var expArea = transform.GetChild(0);
    transform.DetachChildren();
    gameObject.transform.localScale = new Vector3(1, 1) * scale;
    expArea.SetParent(transform);

    audioSource = Camera.main.gameObject.GetComponent<AudioSource>();
  }

  void FixedUpdate() {
    var expArea = transform.GetChild(0);
    transform.DetachChildren();
    gameObject.transform.localScale = new Vector3(1, 1) * scale;
    expArea.SetParent(transform);
  }

  private IEnumerator HandleSpawning() {
    circleColl.enabled = false;
    scale = 0f;
    float step = 0.05f;
    //float step = 0.05f;

    while (scale <= 1) {
      scale += step;
      yield return new WaitForSeconds(timeStep * 3);
    }

    StartCoroutine(HandleExploding());
    //StartCoroutine(HandlePulsing());
  }

  private IEnumerator HandlePulsing() {
    circleColl.enabled = true;
    float bigScale = 1.3f;
    float step = 0.02f;
    scale = 1f;

    for (int i = 0; i < 3; i++) {
      while (scale <= bigScale) {
        scale += step;
        yield return new WaitForSeconds(timeStep);
      }

      while (scale >= 1f) {
        scale -= step;
        yield return new WaitForSeconds(timeStep);
      }
    }

    StartCoroutine(HandleExploding());
  }

  private IEnumerator HandleExploding() {
    circleColl.enabled = true;
    float bigScale = 4f;
    float step = 0.1f;
    scale = 1;

    audioSource.PlayOneShot(ExplosionAudio, 0.6f);
    while (scale <= bigScale) {
      scale += step;
      yield return new WaitForSeconds(timeStep / 4);
    }

    //while (scale >= 0) {
    //  scale -= 0.5f;
    //  yield return new WaitForSeconds(timeStep / 4);
    //}

    Destroy(gameObject);
  }
}
