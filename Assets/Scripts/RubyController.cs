using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontal;
    float vertical;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 position = rb.position;
        position.y = position.y + 3.0f * vertical * Time.deltaTime;
        position.x = position.x + 3.0f * horizontal * Time.deltaTime;

        rb.MovePosition(position);
    }
}
