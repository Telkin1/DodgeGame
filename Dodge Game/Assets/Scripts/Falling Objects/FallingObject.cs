using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObject : MonoBehaviour
{
  public string KillColliderName;

  protected void OnCollisionEnter2D(Collision2D collision) {
    if (collision.collider.gameObject.name == KillColliderName) Kill();
  }

  public void Kill() {
    Destroy(gameObject);
  }
}
