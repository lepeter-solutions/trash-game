using UnityEngine;

public class BinlidAnimationController : MonoBehaviour
{
    [SerializeField] private Animator binlidAnimator = null;
    [SerializeField] private bool openTrigger = false;
    [SerializeField] private bool closeTrigger = false;

    private bool isLidOpen = false;
    void Start()
    {
        binlidAnimator = GetComponent<Animator>();
    }

    public void TriggerLidAnimation(bool state)
    {
        if (state)
        {
            Debug.Log("Open Trigger");
            binlidAnimator.speed = 2f;
            binlidAnimator.Play("TrashbinLidOpen", 0, 0);
            isLidOpen = true;
        }
        else
        {
            binlidAnimator.speed = 2f;
            binlidAnimator.Play("TrashbinLidClose");
            isLidOpen = false;
        }
    }

    public void OpenLidWithState(bool state)
    {
        if (state)
        {
            binlidAnimator.Play("TrashbinLidOpen");
            isLidOpen = true;
        }
        else
        {
            binlidAnimator.Play("TrashbinLidClose");
            isLidOpen = false;
        }
    }
}