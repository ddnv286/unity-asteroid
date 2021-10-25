using UnityEngine;

public class Player : MonoBehaviour
{
    public Bullet bulletPrefab;

    private Rigidbody2D _rigidbody2D;

    public float thrustSpeed = 1.0f;
    public float turnSpeed = 0.1f;

    private bool _thrusting;
    private float _turnDirection;

    // initialize
    private void Awake ()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update ()
    {
        _thrusting = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W));
        // turning left and right
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _turnDirection = -1.0f;
        } else _turnDirection = 0.0f;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    
    private void FixedUpdate ()
    {
        if (_thrusting)
        {
            // the way to move the physics body forward in 2d is transform.up and in 3d is transform.forward
            _rigidbody2D.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (_turnDirection != 0.0f)
        {
            // add torque to rotate the object
            _rigidbody2D.AddTorque(_turnDirection * this.turnSpeed);
        }
    }

    private void Shoot ()
    {
        // must first define the bullet prefab in Unity
        // declare bullet instance
        Bullet bullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
        // then base on player's current position, project the bullet
        bullet.Project(this.transform.up);
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            // stop all movement when dies
            _rigidbody2D.velocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);

            // bad way since this function is really slow
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}
