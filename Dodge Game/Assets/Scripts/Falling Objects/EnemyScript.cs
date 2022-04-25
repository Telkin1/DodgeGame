using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : FallingObject {
  void Start() {
    GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -50f));
  }
}
