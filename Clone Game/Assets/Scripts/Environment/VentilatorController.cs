using System.Collections.Generic;
using UnityEngine;

public class VentilatorController : MonoBehaviour {
    public float maxBlowStrength;
    public float actionHeight;

    List<Transform> clones = new List<Transform>();

    private void FixedUpdate()
    {
        if(PauseManager.Paused) {
            return;
        }

        foreach(Transform clone in clones)
        {
            if (clone == null) continue;

            float dist = clone.position.y - transform.position.y;
            float force = maxBlowStrength - maxBlowStrength * (dist / actionHeight);
            clone.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, force));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform trans = collision.GetComponent<Transform>();
        if (collision.tag == "Clone" && !clones.Contains(trans))
        {
            clones.Add(trans);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Transform trans = collision.GetComponent<Transform>();
        if (collision.tag == "Clone" && clones.Contains(trans))
        {
            clones.Remove(trans);
        }
    }
}
