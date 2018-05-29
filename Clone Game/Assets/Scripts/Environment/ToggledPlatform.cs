using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggledPlatform : MonoBehaviour, ITriggerable{
    public bool startActive = true;

    BoxCollider2D col;
    SpriteRenderer spriteRenderer;

    public void Start()
    {
        col = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        col.enabled = startActive;
        spriteRenderer.enabled = startActive;
    }

    void ToggleActive()
    {
        col.enabled = !col.enabled;
        spriteRenderer.enabled = !spriteRenderer.enabled;
    }

    public void Activated()
    {
        ToggleActive();
    }

    public void Deactivated()
    {
        ToggleActive();
    }
}
