using System.Collections;
using DG.Tweening;
using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;

public class BubbleMechanics : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float bubbleForce = -5;
    private float bubbleGravity = -0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 1;

        SpawnAnimation();
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<PushDownEvent>(BubblePusher);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<PushDownEvent>(BubblePusher);
    }

    void BubblePusher (PushDownEvent pushDownEvent)
    {
        if (pushDownEvent.IsPressed) {
            rb.gravityScale = 0;
            rb.linearVelocityY = bubbleForce;
        }
        if (!pushDownEvent.IsPressed) {
            rb.gravityScale = bubbleGravity;
        }
    }

    void SpawnAnimation()
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle")
        {
            // Bubble pop animation
            StartCoroutine(PopAnimation());
            
        }

        if (collision.collider.tag == "Walls")
        {
            rb.gravityScale = -0.3f;
        }
    }

    private IEnumerator PopAnimation()
    {
        var animator = GetComponentInChildren<Animator>();
        animator.SetTrigger("Explosion");
        yield return null;
        var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
        Destroy(gameObject);
    }
}
