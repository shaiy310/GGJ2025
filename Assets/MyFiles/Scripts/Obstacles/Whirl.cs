using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirl : MonoBehaviour
{
    List<GameObject> bubbles;

    private void Awake()
    {
        bubbles = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble")) {
            bubbles.Add(collision.gameObject);
            StartCoroutine(PushBubble(collision.gameObject));
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bubble")) {
            bubbles.Remove(collision.gameObject);
        }
    }

    IEnumerator PushBubble(GameObject bubble)
    {
        yield return new WaitForSeconds(0.2f);

        var rb = bubble.GetComponent<Rigidbody2D>();
        while (bubbles.Contains(bubble)) {
            var dir = Random.insideUnitCircle;
            Debug.Log($"r: {dir} d: {dir.normalized}");
            rb.AddForce(2f * dir.normalized, ForceMode2D.Impulse);
            //rb.linearVelocity *= 0.5f;
            yield return new WaitForSeconds(0.3f);
        }
    }
}
