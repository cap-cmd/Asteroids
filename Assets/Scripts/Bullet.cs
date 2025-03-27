using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 500.0f;
    [SerializeField] private float _destroyTime = 10.0f;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {
        _rb.AddForce(direction * _speed);
        Destroy(this.gameObject, _destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
