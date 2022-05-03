using Assets.Scripts.Object_Spawners;
using System.Collections;
using UnityEngine;

public class GoldSpawner : ObjectSpawner
{
  private void Awake()
  {
    base.SpawnObjectTest = SpawnObject;
  }

  private new IEnumerator SpawnObject()
  {
    var gold = Instantiate(Prefab, new Vector2(Random.Range(spawnArea.x, spawnArea.x + spawnArea.width), Random.Range(spawnArea.y, spawnArea.y + spawnArea.height)), Quaternion.Euler(0, 0, Random.Range(0, 360)));
    var goldColl = gold.GetComponent<BoxCollider2D>();
    var goldRb = gold.GetComponent<Rigidbody2D>();

    goldRb.AddForce(Vector2.MoveTowards(goldRb.position, new Vector2(0f, 0f), -40));

    goldColl.enabled = false;
    yield return new WaitForSeconds(0.5f);
    goldColl.enabled = true;
  }
}
