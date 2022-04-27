using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  public float speed;
  public bool activeMovement = true;

  private Rigidbody2D rb;
  private Camera cam;

  private int controls;
  private Vector2 movement;

  void Start() {
    cam = Camera.main;
    rb = GetComponent<Rigidbody2D>();

    Physics2D.gravity = new Vector2(0, -1f);

    controls = PlayerPrefs.GetInt("playerControls", 0);
    switch (controls) {
      case 0:
        speed = 200;
        break;
      case 1:
        speed = 10;
        break;
    }
  }

  void Update() {
    movement.x = Input.GetAxisRaw("Horizontal");
    movement.y = Input.GetAxisRaw("Vertical");
  }

  void FixedUpdate() {
    if (!activeMovement) return;

    Vector2 newPos = new Vector2();
    switch (controls) {
      case 0:
        newPos = Vector2.MoveTowards(rb.position, cam.ScreenToWorldPoint(Input.mousePosition), speed * Time.fixedDeltaTime);
        break;
      case 1:
        newPos = Vector2.MoveTowards(rb.position, rb.position + movement, speed * Time.fixedDeltaTime);
        break;
    }

    rb.MovePosition(newPos);
  }
}
