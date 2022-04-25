using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStarter : MonoBehaviour
{
  void Start()
  {
    GameObject.Find("GameOverPanel").SetActive(false);
  }
}
