using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PlayerInteractions : MonoBehaviour {

  void OnCollisionEnter2D(Collision2D collision) {
    switch (collision.gameObject.tag) {
      case "Enemy":
        Debug.Log("Hit enemy");
        collision.gameObject.GetComponent<FallingObject>()?.Kill();
        break;
      case "Gold":
        collision.gameObject.GetComponent<FallingObject>()?.Kill();
        ScoreManager.instance.AddScore(1);
        Debug.Log("gold collected");
        break;
    }
  }
}
