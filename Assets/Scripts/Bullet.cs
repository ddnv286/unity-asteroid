using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    public float speed = 500.0f;
    public float maxLifeTime = 10.0f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Project (Vector2 direction)
    {
        _rigidbody2D.AddForce(direction * this.speed);
        // destroy the bullet when timeout
        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
