using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneHealthController : MonoBehaviour {
    public float maxHp = 100;
    public Transform healthBar;
    public float spikeDamage = 30;
    public float spikeDamageDelay = 2;

    float maxHealthBar;
    float hp;

    public void Start()
    {
        maxHealthBar = healthBar.localScale.x;
        hp = maxHp;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            hp = 0;
            Die();
        }

        healthBar.localScale = new Vector3(hp / maxHp * maxHealthBar, healthBar.localScale.y, healthBar.localScale.z);
    }

    public IEnumerator TakeSpikeDamage()
    {
        while(true)
        {
            if(PauseManager.Paused) {
                yield return null;
            } else {
                TakeDamage(spikeDamage);
                yield return new WaitForSeconds(spikeDamageDelay);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Spike")
        {
            StartCoroutine("TakeSpikeDamage");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Spike")
        {
            StopCoroutine("TakeSpikeDamage");
        }
    }
}
