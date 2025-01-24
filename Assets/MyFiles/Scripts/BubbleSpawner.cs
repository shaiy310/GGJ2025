using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class BubbleSpawner : MonoBehaviour
{
    [SerializeField] BubbleMechanics prefab;
    [SerializeField] float spawnDelay = 0.25f;
    [SerializeField] float spawnInterval = 0.5f;

    Coroutine spawner;

    private void OnEnable()
    {
        Messenger.Default.Subscribe<PushDownEvent>(OnInput);
        Debug.Log("registered");
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<PushDownEvent>(OnInput);
    }

    private void OnInput(PushDownEvent pushDownEvent)
    {
        Debug.Log("oninput");
        if (pushDownEvent.IsPressed && spawner == null) {
            Debug.Log("down");
            spawner = StartCoroutine(SpawnBubble());
        }
        if (!pushDownEvent.IsPressed && spawner != null) {
            Debug.Log("up");
            StopCoroutine(spawner);
            spawner = null;
        }
    }

    IEnumerator SpawnBubble()
    {
        yield return new WaitForSeconds(spawnDelay);
        
        while (true) {
            Instantiate(prefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
