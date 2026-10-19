using Unity.VisualScripting;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    public float minSpeed = 50f;
    public float maxSpeed = 150f;
    public float spin = 10f;    
    public GameObject bounceEffect;
    Rigidbody2D body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(size, size, 1);
        
        Vector2 direction = Random.insideUnitCircle;
        float speed = Random.Range(minSpeed, maxSpeed) / size;

        float torque = Random.Range(-spin, spin);

        body = GetComponent<Rigidbody2D>();
        body.AddForce(direction * speed);
        body.AddTorque(torque);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 contact = collision.GetContact(0).point;
        Instantiate(bounceEffect, contact, Quaternion.identity);
    }
}
