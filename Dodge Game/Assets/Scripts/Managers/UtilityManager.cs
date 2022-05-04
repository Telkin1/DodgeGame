using Assets.Scripts.Object_Spawners;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityManager : MonoBehaviour
{
  public ObjectSpawner[] SpawnersToStartOnStartup;
  private void Start()
  {
    foreach (var spawner in SpawnersToStartOnStartup)
    {
      spawner.StartSpawning();
    }
  }
}
