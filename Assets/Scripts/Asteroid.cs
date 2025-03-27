using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] Sprite[] _sprites;
    [SerializeField] GameObject explosionPrefab;

    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _maxLifeTime = 30f;


    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private GameManager _gameManager;

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * size;
        _rigidbody.mass = size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * _speed);
        Destroy(this.gameObject, _maxLifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (this.size * 0.5 >= minSize)
            {
                CreateSplit();
                CreateSplit();
            }
            _gameManager.AddScore();

            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, transform.rotation);
        half.size = this.size * 0.5f;
        half.SetTrajectory(Random.insideUnitCircle.normalized);

    }
}
