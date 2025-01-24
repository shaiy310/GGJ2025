using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject prefab;
    [SerializeField] float spawnDelay = 0.25f;

    Coroutine spawner;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spawner == null) {
            spawner = StartCoroutine(SpawnBubble());
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            StopCoroutine(spawner);
            spawner = null;
        }
    }

    IEnumerator SpawnBubble()
    {
        yield return new WaitForSeconds(spawnDelay);
        
        if (Input.GetKey(KeyCode.Space)) { 
            Instantiate(prefab, spawnPoint);
        }
        spawner = null;
    }
}
