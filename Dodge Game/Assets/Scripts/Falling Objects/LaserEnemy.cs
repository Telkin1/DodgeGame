using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : MonoBehaviour {

  private BoxCollider2D coll;

  void Start() {
    transform.localScale = new Vector2(50f, 0f);
    coll = GetComponent<BoxCollider2D>();
    //coll.enabled = false;

    SpawnAtRandom();
    StartCoroutine(EnableCollider());
  }

  private IEnumerator EnableCollider() {
    yield return new WaitForSeconds(0);
    coll.enabled = true;
    yield return new WaitForSeconds(1.5f);
    Destroy(gameObject);
  }

  public void SpawnAt(Vector2 position, float angle) {
    transform.position = position;
    transform.rotation = Quaternion.Euler(0, 0, angle); 
  }

  public void SpawnAtRandom() {
    transform.position = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 3f));
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
  }

  void FixedUpdate() {
    transform.localScale = transform.localScale;
  }
}
