using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourExtensions : MonoBehaviour
{
  public void PopEffect(GameObject objectToPop, float bigSize = 1.5f, float smallSize = 1f, float upTime = 0f, float downTime = 1f) {
    if (!objectToPop.activeSelf) return;
    StartCoroutine(Resize(objectToPop, bigSize, smallSize, upTime, downTime));
  }

  public void StopPopEffects() {
    StopAllCoroutines();
  }

  private IEnumerator Resize(GameObject objectToPop, float bigSize = 1.5f, float smallSize = 1f, float upTime = 0f, float downTime = 1f) {
    float ts = 1f / 40;
    Vector2 scale = objectToPop.transform.localScale;
    float newScale = 1f;

    float upTimeParts = upTime / ts;
    float scaleStep = (bigSize - newScale) / upTimeParts;

    // Scale up
    for (int i = 0; i < upTimeParts; i++) {
      newScale += scaleStep;
      objectToPop.transform.localScale = new Vector3(newScale, newScale);

      //Debug.Log(newScale);
      yield return new WaitForSeconds(ts);
    }

    if (upTimeParts == 0) {
      newScale = bigSize;
      objectToPop.transform.localScale = new Vector3(newScale, newScale);

      yield return new WaitForSeconds(ts);
    }

    float downTimeParts = downTime / ts;
    scaleStep = (smallSize - bigSize) / downTimeParts;

    // Scale down
    for (int i = 0; i < downTimeParts; i++) {
      newScale += scaleStep;
      objectToPop.transform.localScale = new Vector3(newScale, newScale);

      //Debug.Log(newScale);
      yield return new WaitForSeconds(ts);
    }

    if (downTimeParts == 0) {
      newScale = smallSize;
      objectToPop.transform.localScale = new Vector3(newScale, newScale);

      yield return new WaitForSeconds(ts);
    }
  }
}
