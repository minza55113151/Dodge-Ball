using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    
    [SerializeField] private float startSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float shakeEvery;
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeRadius;

    float speed;
    float time = 0f;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        speed = startSpeed;
        LaunchBall();
    }


    private void Update()
    {
        SpeedUp();   
    }

    private void LaunchBall()
    {
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        rb.velocity = direction * speed;
    }

    private void SpeedUp()
    {
        time += Time.deltaTime;
        if (time > shakeEvery)
        {
            time -= shakeEvery;
            StartCoroutine(Shake());
            speed *= speedMultiplier;
            LaunchBall();
        }
    }

    private IEnumerator Shake()
    {
        float Stime = 0f;
        Vector3 pos = transform.position;
        while(Stime < shakeTime)
        {
            transform.position = pos + Random.insideUnitSphere * shakeRadius;
            Stime += Time.deltaTime;
            yield return null;
        }
        transform.position = pos;
    }

}
