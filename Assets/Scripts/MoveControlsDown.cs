using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControlsDown : MonoBehaviour
{
    [SerializeField] private float speed;

    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0f, -speed);
    }
}
