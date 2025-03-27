using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] private Bullet bulletPrefab;
    [Space(10)]
    [SerializeField] private float thrustSpeed;
    [SerializeField] private float turnSpeed;

    private Rigidbody2D _playerRigidbody;
    private InputManager _inputManager;
    private GameManager _gameManager;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _inputManager = GameObject.Find("GameManager").GetComponent<InputManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        Thrust();
        Turn();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            _gameManager.TakeDamage();
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

    }

    private void Thrust()
    {
        if (_inputManager.ChekThrust)
            _playerRigidbody.AddForce(transform.up * thrustSpeed);
    }

    private void Turn()
    {
        if (_inputManager.TurnDirection != 0)
            _playerRigidbody.AddTorque(_inputManager.TurnDirection * turnSpeed  * -1);
    }

    private void Shoot()
    {
        if (_inputManager.CheckShooting)
        {
            Bullet bullet = Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
        }
    }

    private void SetVulnerableState() => this.gameObject.layer = LayerMask.NameToLayer("Player");

    public void SetInvulnerableState()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Invulnerability");
        Invoke(nameof(SetVulnerableState), 3);
    }
}

