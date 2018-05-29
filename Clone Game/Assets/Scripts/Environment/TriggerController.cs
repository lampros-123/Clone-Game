using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour {
    public GameObject triggerReceiverGo;

    ITriggerable triggerReceiver;
    Animator animator;
    int entered;

    public void Start()
    {
        animator = GetComponent<Animator>();
        triggerReceiver = triggerReceiverGo.GetComponent<ITriggerable>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        entered++;
        if(entered == 1)
        {
            animator.SetTrigger("Retract");
            triggerReceiver.Activated();
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        entered--;
        if(entered == 0)
        {
            animator.SetTrigger("Extend");
            triggerReceiver.Deactivated();
        }
    }
}
