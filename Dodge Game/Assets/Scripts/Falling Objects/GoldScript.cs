using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : FallingObject {
  public GameObject ParticleSystemPrefab;

  new void OnCollisionEnter2D(Collision2D collision) {
    base.OnCollisionEnter2D(collision);

    if (collision.collider.name == "Player") {
      var ps = Instantiate(ParticleSystemPrefab);
      Vector3 v = gameObject.transform.position;
      v.z = -1;
      ps.gameObject.transform.position = v;
      Destroy(ps, 4f);
    }
  }
}
