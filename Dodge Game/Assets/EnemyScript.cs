using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

  void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.gameObject.name == "ObjectSpawner") Kill();
  }

  private void Kill() {
    Destroy(gameObject);
  }
}
