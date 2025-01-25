using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class Claw : MonoBehaviour
{
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [SerializeField] float rotationAngle = 30;
    [SerializeField] float animationTime = 0.35f;
    [SerializeField] float closeTime = 0.2f;
    [SerializeField] float openTime = 0.8f;

    Coroutine anim;
    void OnEnable()
    {
        anim = StartCoroutine(RunAnimation());
    }

    private void OnDisable()
    {
        if (anim != null) {
            StopCoroutine(anim);
            anim = null;
        }
    }

    private IEnumerator RunAnimation()
    {
        while (true) {
            topPivot.DOLocalRotate(rotationAngle * Vector3.back, animationTime);
            bottomPivot.DOLocalRotate(rotationAngle * Vector3.forward, animationTime);

            yield return new WaitForSeconds(animationTime + openTime);
            
            topPivot.DOLocalRotate(Vector3.zero, animationTime);
            bottomPivot.DOLocalRotate(Vector3.zero, animationTime);

            yield return new WaitForSeconds(animationTime + closeTime);
        }
    }
}
