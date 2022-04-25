using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : FallingObject {

  private CircleCollider2D circleColl;
  private float scale;
  private float timeStep = 0.02f;

  new void OnCollisionEnter2D(Collision2D collision) {
    base.OnCollisionEnter2D(collision);
  }

  void Start() {
    circleColl = GetComponent<CircleCollider2D>();
    StartCoroutine(HandleSpawning());
  }

  void FixedUpdate() {
    gameObject.transform.localScale = new Vector3(1, 1) * scale;
  }

  private IEnumerator HandleSpawning() {
    circleColl.enabled = false;
    scale = 0f;
    float step = 0.05f;

    while (scale <= 1) {
      scale += step;
      yield return new WaitForSeconds(timeStep * 3);
    }

    StartCoroutine(HandlePulsing());
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
    float bigScale = 4f;
    float step = 0.1f;
    scale = 1;

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
