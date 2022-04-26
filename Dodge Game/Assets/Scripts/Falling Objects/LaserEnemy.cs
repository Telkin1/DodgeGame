using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour {

  private BoxCollider2D coll;

  private float width;
  private float timeStep = 0.02f;

  void Start() {
    transform.localScale = new Vector2(50f, 0f);
    coll = GetComponent<BoxCollider2D>();
    coll.enabled = false;

    SpawnAtRandom();
  }

  public void SpawnAt(Vector2 position, float angle) {
    transform.position = position;
    transform.rotation = Quaternion.Euler(0, 0, angle);

    StartCoroutine(HandleSpawning());
  }

  public void SpawnAtRandom() {
    transform.position = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 3f));
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

    StartCoroutine(HandleSpawning());
  }

  private IEnumerator HandleSpawning() {
    width = 0;
    float normalWidth = 0.5f;
    float step = 0.01f;

    while (width <= normalWidth) {
      width += step;
      yield return new WaitForSeconds(timeStep);
    }

    StartCoroutine(HandleExisting());
  }

  private IEnumerator HandleExisting() {
    coll.enabled = true;
    width = 0.5f;
    float bigWidth = 0.6f;
    float smallWidth = 0.4f;
    float step = 0.02f;
    
    for (int i = 0; i < 3; i++) {

      for (int j = 0; j < 16; j++) {
        width += step;
        yield return new WaitForSeconds(timeStep);
      }

      for (int j = 0; j < 16; j++) {
        width -= step;
        yield return new WaitForSeconds(timeStep);
      }
    }

    StartCoroutine(HandleDespawning());
  }

  private IEnumerator HandleDespawning() {
    width = 0.5f;
    float smallWidth = 0f;
    float step = 0.04f;

    while (width >= smallWidth) {
      width -= step;
      yield return new WaitForSeconds(timeStep);
    }

    Destroy(gameObject);
  }

  void FixedUpdate() {
    transform.localScale = new Vector3(50, width);
  }
}
