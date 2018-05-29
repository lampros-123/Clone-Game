using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneController : MonoBehaviour {
    public float maxSpeed = 5f;
    public float jumpSpeed = 10f;
    public int frameOffset = 0;
    public bool destroyOnCollision = false;
    public Transform jumpCheckLeft;
    public Transform jumpCheckRight;

    ICloneController controller;
    Animator animator;
    Rigidbody2D rb;
    int actionIdx = 0;
    float vx = 0;
    bool running = false;
    Vector2 vel = new Vector2();

    public void SetController(ICloneController contr)
    {
        controller = contr;
    }

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartMoving();
    }

    public void SetRunning(bool running) {
        this.running = running;

        if(running) {
            rb.velocity = vel;
            rb.isKinematic = false;
        } else {
            vel = rb.velocity;
            rb.velocity = new Vector2(0, 0);
            rb.isKinematic = true;
        }
    }

    public void StartMoving()
    {
        running = true;
    }

    private bool CanJump()
    {
        Collider2D[] overlaps = Physics2D.OverlapAreaAll(jumpCheckLeft.position, jumpCheckRight.position);
        int nonTriggers = 0;
        foreach(Collider2D col in overlaps)
        {
            if (!col.isTrigger) nonTriggers++;
        }   

        return nonTriggers > 1;
    }

    public void SetFrameOffset(int offset)
    {
        frameOffset = offset;
    }


    void FixedUpdate()
    {
        if (running == PauseManager.Paused) {
            SetRunning(!running);
        }
        if (!running || controller.GetActions().Count == 0) return;

        Action action;
        int limit = 0;
        while (actionIdx < controller.GetActions().Count
            && (action = controller.GetActions()[actionIdx] as Action).GetFrame() <= controller.GetCurrentFrame() - frameOffset
            && limit++ < 100)
        {
            if(limit > 50) 
                Debug.Log(limit);
            actionIdx++;
            if (action.GetAction() == Action.KEYDOWN_RIGHT)
            {
                vx = maxSpeed;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                animator.SetInteger("State", 1);
            }
            else if (action.GetAction() == Action.KEYUP_RIGHT && vx > 0)
            {
                vx = 0;
                animator.SetInteger("State", 0);
            }
            else if (action.GetAction() == Action.KEYDOWN_LEFT)
            {
                vx = -maxSpeed;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*-1, transform.localScale.y, transform.localScale.z);
                animator.SetInteger("State", 1);
            }
            else if (action.GetAction() == Action.KEYUP_LEFT && vx < 0)
            {
                vx = 0;
                animator.SetInteger("State", 0);
            }
            else if (action.GetAction() == Action.KEYDOWN_UP && CanJump())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                animator.SetTrigger("Jump");
            }
        }
        rb.velocity = new Vector2(vx, rb.velocity.y);


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!destroyOnCollision) return;

        if(other.transform.tag == "Clone")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
