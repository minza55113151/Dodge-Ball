using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;

    [SerializeField] float shakeDuration;
    
    Camera mainCamera;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
    }
    public void Hit(GameObject ball)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        float power = rb.velocity.magnitude * 0.01f;
        StartCoroutine(Shake(power));
    }
    
    private IEnumerator Shake(float power)
    {
        Vector3 pos = mainCamera.transform.position;
        float Stime = 0f;
        float shakeRadius;
        while(Stime < shakeDuration)
        {
            Stime += Time.deltaTime;
            shakeRadius = Mathf.Lerp(0.1f, 0f, Stime / shakeDuration);
            mainCamera.transform.position = pos + Random.insideUnitSphere * shakeRadius * power;
            yield return null;
        }
        mainCamera.transform.position = pos;
    }
}
