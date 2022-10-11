using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Reference GameObject")]
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private List<GameObject> spawnObjects = new List<GameObject>();

    [Header("Enemy Holder Objects")]
    [SerializeField] private Transform enemyFrogHolder;

    private void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            Spawn(3);
        }

        if (Input.GetKeyDown("z"))
        {
            spawnObjects.ForEach(spawnObj =>
            {
                if (spawnObj.activeInHierarchy)
                {
                    spawnObj.GetComponent<EnemyMovement>().Run();
                }
            });
        }

    }

    private List<GameObject> cloneGameObject(int quantity)
    {
        List<GameObject> listSpawn = new List<GameObject>();

        for (int i = 0; i < quantity; i++)
        {
            // clone gameObject
            GameObject clone = Instantiate(spawnObject, transform);
            clone.SetActive(false);
            // set clone gameObject into holder
            clone.transform.SetParent(enemyFrogHolder);
            // add that cloned object into list 
            listSpawn.Add(clone);
        }

        return listSpawn;
    }

    private void Spawn(int quantity)
    {
        List<GameObject> listSpawn = new List<GameObject>();
        // get available in gameObjects to list spawn when gameObj is active in hierarchy 
        spawnObjects.ForEach(spawnObj =>
        {
            if (!spawnObj.activeInHierarchy)
            {
                listSpawn.Add(spawnObj);
            }
        });
      
        // check if quantity enough or not
        // if not create new gameObj
        if (listSpawn.Count < quantity)
        {
            List<GameObject> newObjs = cloneGameObject(quantity - listSpawn.Count);
            listSpawn.AddRange(newObjs);
            spawnObjects.AddRange(newObjs);
        }

        listSpawn.ForEach(spawnObj =>
        {
            spawnObj.GetComponent<Health>().Respawn();
            randomPosition(spawnObj);
        });
    }

    public void randomPosition(GameObject spawnObj)
    {
        // new spawn position
        var newSpawnPosition = transform.position;
        newSpawnPosition += new Vector3(Random.Range(-6f, 0f), Random.Range(2f, 4f), 0f);
        // set position value
        spawnObj.transform.position = newSpawnPosition;
    }


}
