using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyController : MonoBehaviour
{

    public AudioClip hitClip;

    public float speed = 3.0f;
    public float changeTime = 3.0f;
    public bool vertical;

    bool broken = true;

    public ParticleSystem smokeEffect;

    Rigidbody2D rb2D;
    Animator animator;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if(!broken)
        {
            return;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb2D.position;

        if (vertical)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rb2D.MovePosition(position);

        if (!broken)
        {
            return;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-1);
            player.PlaySound(hitClip);
        }
    }
    public void Fix()
    {
        broken = false;
        rb2D.simulated = false;
        smokeEffect.Stop();
        animator.SetTrigger("Fixed");
    }
}
