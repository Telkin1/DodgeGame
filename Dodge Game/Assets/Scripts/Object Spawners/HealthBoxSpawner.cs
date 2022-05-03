using Assets.Scripts.Object_Spawners;
using System.Collections;
using UnityEngine;

public class HealthBoxSpawner : ObjectSpawner
{
  void Start()
  {
    var p = GameObject.Find("Player").GetComponent<PlayerInteractions>();

    p.OnPlayerHit += delegate
    {
      StartSpawning();
    };

    p.OnPlayerHeal += delegate
    {
      if (p.healthBarUI.health == p.healthBarUI.numOfHearts) StopSpawning();
    };
  }
}
