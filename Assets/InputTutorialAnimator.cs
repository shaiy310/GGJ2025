using MyFiles.Scripts.Events;
using SuperMaxim.Messaging;
using UnityEngine;

public class InputTutorialAnimator : MonoBehaviour
{
    Animation anim;

    private void Awake()
    {
        anim = GetComponent<Animation>();
    }

    private void OnEnable()
    {
        Messenger.Default.Subscribe<InputEvent>(RunAnimation);
    }

    private void OnDisable()
    {
        Messenger.Default.Unsubscribe<InputEvent>(RunAnimation);
    }

    void RunAnimation(InputEvent inputEvent)
    {
        if (inputEvent.HorizontalMovement != 0) {
            anim.Play("TutorialFadeOut");
        } else {
            anim.Play("TutorialFadeIn");
        }
    }

}
