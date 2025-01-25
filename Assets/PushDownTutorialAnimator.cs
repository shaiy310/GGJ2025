using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;

public class PushDownTutorialAnimator : MonoBehaviour
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<PushDownEvent>(RunAnimation);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<PushDownEvent>(RunAnimation);
    }
    
    void RunAnimation(PushDownEvent pushDownEvent)
    {
        if (pushDownEvent.IsPressed) {
            anim.Play("TutorialFadeOut");
        } else {
            anim.Play("TutorialFadeIn");
        }
    }

}
