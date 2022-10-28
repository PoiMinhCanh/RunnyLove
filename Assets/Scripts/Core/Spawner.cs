using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Point Spawner")]
    [SerializeField] private Transform pointSpawner;

    [Header("Enemy Holder Objects")]
    [SerializeField] private GameObject[] listSpawnObject;
    [SerializeField] private Transform[] listEnemyBagHolder;

    [SerializeField] private List<GameObject>[] listSpawnContainer;

    // Component
    private Animator anim;

    private int _quantitySpawnObject;

    private void Awake()
    {
        // get component
        anim = GetComponent<Animator>();

        _quantitySpawnObject = listSpawnObject.Length;
        listSpawnContainer = new List<GameObject>[_quantitySpawnObject];
        for (int i = 0; i < _quantitySpawnObject; i++)
        {
            listSpawnContainer[i] = new List<GameObject>();
        }
    }

    private List<GameObject> cloneGameObject(int index, int quantity)
    {
        List<GameObject> listSpawn = new List<GameObject>();

        for (int i = 0; i < quantity; i++)
        {
            // clone gameObject
            GameObject clone = Instantiate(listSpawnObject[index], transform);
            clone.GetComponent<EnemyController>().setType((EnemyType)index);
            clone.SetActive(false);
            // set clone gameObject into holder
            clone.transform.SetParent(listEnemyBagHolder[index]);
            // add that cloned object into list 
            listSpawn.Add(clone);
        }

        return listSpawn;
    }

    private int SpawnEveryType(int index, int quantity, ref List<GameObject> listSpawn)
    {
        foreach(var spawnObj in listSpawnContainer[index]){
            if (!spawnObj.activeInHierarchy)
            {
                listSpawn.Add(spawnObj);
                quantity--;
                if (quantity == 0) break;
            }
        }
        return quantity;
    }

    public List<GameObject> Spawn(int quantity)
    {
        // random list type of enemy
        int[] randomSpawnObj = new int[_quantitySpawnObject];
        
        for (int i = 0; i < quantity; i++)
        {
            randomSpawnObj[Random.Range(0, _quantitySpawnObject)]++;
        }

        List<GameObject> listSpawn = new List<GameObject>();
        // get available in gameObjects to list spawn when gameObj is active in hierarchy 
        for (int i = 0; i < _quantitySpawnObject; i++) {
            // continue when quantity is 0
            if (randomSpawnObj[i] == 0) continue;
            
            int remain = SpawnEveryType(i, randomSpawnObj[i], ref listSpawn);
            // check if quantity enough or not
            // if not create new gameObj
            if (remain != 0)
            {
                List<GameObject> newObjs = cloneGameObject(i, remain);
                listSpawn.AddRange(newObjs);
                listSpawnContainer[i].AddRange(newObjs);
            }

        }
        return listSpawn;
    }

    public void resetSpawnObj(GameObject spawnObj)
    {
        spawnObj.GetComponent<Health>().Respawn();
        spawnObj.transform.position = pointSpawner.position;
        spawnObj.GetComponent<EnemyMovement>().isFirstMove = false;
    }

    // set open gate animation
    public void openGateAnimation()
    {
        anim.SetBool("open", true);
    }

    // set close gate animation
    public void closeGateAnimation()
    {
        anim.SetBool("open", false);
    }
}
