using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

  public Animator myAnimator;
  private string menuToLevel = "MenuToLevelTransition";

  public void Play() {
    StartCoroutine(_Play());
  }

  private IEnumerator _Play() {
    myAnimator.Play(menuToLevel, 0, 0.0f);

    yield return new WaitForSeconds(1f);

    LevelManager.instance.LoadSceneByName("Level_1");
    //SceneManager.LoadScene(1);
  }
}
