using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessRunner : MonoBehaviour
{
    public GameObject[] prefabArray; // массив префабов
    public GameObject startPrefab; // стартовый префаб

    private List<GameObject> spawnedObjects; // список созданных объектов
    private Transform lastSpawnedObject; // последний созданный объект

    private void Start()
    {
        spawnedObjects = new List<GameObject>();
        lastSpawnedObject = Instantiate(startPrefab, transform.position, Quaternion.identity).transform;
        spawnedObjects.Add(lastSpawnedObject.gameObject);

        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            GameObject prefabToSpawn = prefabArray[Random.Range(0, prefabArray.Length)];
            GameObject newObject = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);
            spawnedObjects.Add(newObject);

            BoxCollider2D lastSpawnedObjectCollider = lastSpawnedObject.GetComponent<BoxCollider2D>();
            BoxCollider2D newObjectCollider = newObject.GetComponent<BoxCollider2D>();

            float lastSpawnedObjectWidth = lastSpawnedObjectCollider.size.x;
            float lastSpawnedObjectOffset = lastSpawnedObjectCollider.offset.x;
            float newObjectWidth = newObjectCollider.size.x;
            float newObjectOffset = newObjectCollider.offset.x;

            float spawnPositionX = lastSpawnedObject.position.x +
                                   lastSpawnedObjectWidth * 0.5f +
                                   lastSpawnedObjectOffset +
                                   newObjectOffset -
                                   newObjectWidth * 0.5f;

            newObject.transform.position = new Vector3(spawnPositionX, transform.position.y, transform.position.z);
            lastSpawnedObject = newObject.transform;

            yield return null;
        }
    }
}