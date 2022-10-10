using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCamp : MonoBehaviour
{
    // Start is called before the first frame update

    public float campRadius = 20f;

    public int enemyAmount = 3;

    public int enemySpawnDuration = 1200; //In frame 
    int currentEnemySpawnDuration;
    List<GameObject> enemyList;

    [SerializeField]
    GameObject prefab;

    Vector3 campPosition;

    void Start()
    {
        campPosition = gameObject.transform.position;
        currentEnemySpawnDuration = enemySpawnDuration;
        enemyList = new List<GameObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemySpawnDuration <= 0)
        {

            if(enemyList.Count < enemyAmount)
            {
              
                Vector3 spawnPosition = GenerateSpawnLocation();
               

                GameObject spawnedPrefab = Instantiate(prefab, spawnPosition, Quaternion.identity);
                spawnedPrefab.transform.parent = transform;
                enemyList.Add(spawnedPrefab);

            }
            currentEnemySpawnDuration = enemySpawnDuration;
        }
        currentEnemySpawnDuration--;

       
    }

    public void RemoveAnimal(GameObject animal)
    {
        enemyList.Remove(animal);
    }

    Vector3 GenerateSpawnLocation()
    {
        Vector3 spawnPosition = campPosition;
        spawnPosition.x += Random.Range(-campRadius, campRadius);
        spawnPosition.z += Random.Range(-campRadius, campRadius);
        return spawnPosition;
    }

    
}
