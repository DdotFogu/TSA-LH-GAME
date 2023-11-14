using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallHandler : MonoBehaviour
{
    public float strenght;
    public float quota;

    private void Update()
    {
        State();
    }

    public void State()
    {
        if (strenght >= quota)
        {
            DisableRenderer();
            DisableCollider();
        }
        else
        {
            EnableRenderer();
            EnableCollider();
        }
    }

    private void DisableRenderer()
    {
        if (TryGetComponent<SpriteRenderer>(out var renderer))
        {
            renderer.enabled = false;
        }
        else if (TryGetComponent<TilemapRenderer>(out var tilemapRenderer))
        {
            tilemapRenderer.enabled = false;
        }
    }

    private void EnableRenderer()
    {
        if (TryGetComponent<SpriteRenderer>(out var renderer))
        {
            renderer.enabled = true;
        }
        else if (TryGetComponent<TilemapRenderer>(out var tilemapRenderer))
        {
            tilemapRenderer.enabled = true;
        }
    }

    private void DisableCollider()
    {
        if (TryGetComponent<BoxCollider2D>(out var boxCollider))
        {
            boxCollider.enabled = false;
        }
        else if (TryGetComponent<TilemapCollider2D>(out var tilemapCollider))
        {
            tilemapCollider.enabled = false;
        }
    }

    private void EnableCollider()
    {
        if (TryGetComponent<BoxCollider2D>(out var boxCollider))
        {
            boxCollider.enabled = true;
        }
        else if (TryGetComponent<TilemapCollider2D>(out var tilemapCollider))
        {
            tilemapCollider.enabled = true;
        }
    }

    private bool TryGetComponent<T>(out T component) where T : Component
    {
        component = GetComponent<T>();
        return component != null;
    }
}
