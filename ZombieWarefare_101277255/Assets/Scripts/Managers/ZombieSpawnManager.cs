using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public int zombiesSpawnCount;
    public GameObject[] zombiePrefab;
    public SpawnerManager[] spawnerVolumes;

    GameObject followGameObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    void SpawnZombie()
    {
        GameObject zombieToSpawn = zombiePrefab[Random.Range(0, zombiePrefab.Length)];
        SpawnerManager spawnVolume = spawnerVolumes[Random.Range(0, spawnerVolumes.Length)];
        //if (!followGameObject) return;

        //object pooling can be referenced here
        GameObject zombie = Instantiate(zombieToSpawn, spawnVolume.GetPositionInBoxBounds(), spawnVolume.transform.rotation);

        //zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
    }

    public void spawnNumberOfZombies(int number)
    {
        for (int i = 0;i < number;i++)
        {
            SpawnZombie();
        }
    }
}
