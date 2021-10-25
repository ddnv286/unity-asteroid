using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float size = 1.0f;
    public float speed = 10.0f;
    public float maxLifeTime = 30.0f;

    private void Awake ()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start ()
    {
        // render random sprite for each asteroid
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        // render randomized rotation, z will be random rotation by multiplying to 360 degrees
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        // randomizing scale would have other impacts and change asteroid's physics properties
        // if the asteroid is big enough, it would be split to smaller asteroid when shot
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    public void SetTrajectory (Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        // destroy when timeout
        Destroy(this.gameObject, this.maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // processing colliding with anything
        // add Bullet tag in Unity first
        if (collision.gameObject.tag == "Bullet")
        {
            // if the asteroid is big enough, split the asteroid
            if (this.size * 0.5f >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit ()
    {
        // offset the splits of the asteroid to feel more natural
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;
        // set the trajectory to be random
        // keep the magnitude because it would affect the split's speed
        half.SetTrajectory(Random.insideUnitCircle.normalized * this.speed);
    }
}
