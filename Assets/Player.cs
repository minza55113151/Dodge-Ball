using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    [SerializeField] float speed;
    [SerializeField] float dodgeDuration;
    [SerializeField] float dodgeCoolDownTime;


    bool isDodgeReady = true;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Collider2D cld;
    private SpriteRenderer spriteRender;

    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        cld = GetComponent<BoxCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;
        InputDodge();
    }
    private void InputDodge()
    {
        if (isDodgeReady && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Dodge());
            StartCoroutine(CoolDownDodge());
        }
    }
    private IEnumerator Dodge()
    {
        Color oldColor = spriteRender.color;
        cld.enabled = false;
        spriteRender.color = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a * 0.2f);
        yield return new WaitForSeconds(dodgeDuration);
        cld.enabled = true;
        spriteRender.color = oldColor;
    }
    private IEnumerator CoolDownDodge()
    {
        isDodgeReady = false;
        yield return new WaitForSeconds(dodgeCoolDownTime);
        isDodgeReady = true;
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GameManager.instance.GameOver();
        }
    }
}
