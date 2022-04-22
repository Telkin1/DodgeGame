using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public float speed;

  private Rigidbody2D rb;
  private Camera cam;

  void Start() {
    cam = Camera.main;
    rb = GetComponent<Rigidbody2D>();

    Physics2D.gravity = new Vector2(0, -1f);
  }

  void FixedUpdate() {
    rb.MovePosition(Vector2.MoveTowards(rb.position, cam.ScreenToWorldPoint(Input.mousePosition), speed * Time.fixedDeltaTime));
  }
}
