using DG.Tweening;
using UnityEngine;

public class BubbleMechanics : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float bubbleForce = -5;
    private float bubbleGravity = -0.1f;

    void Start()
    {
        Spawn();
    }

    void Update()
    {
        BubblePusher();
    }

    void BubblePusher ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.gravityScale = 0;
            rb.linearVelocityY = bubbleForce;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb.gravityScale = bubbleGravity;
        }

    }

    void Spawn ()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            // Bubble pop animation
            Destroy(gameObject);
        }
    }
}