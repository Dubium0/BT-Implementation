using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed_ = 5.0f;    
    private float range_ = 5.0f;
    
    Vector3 startPosition = Vector3.zero;

    private Rigidbody2D rb2d { get; set; }

    [SerializeField]
    private LayerMask layerMask_;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.includeLayers = layerMask_;
    }

    private void Start()
    {
        startPosition = transform.position;
        rb2d.linearVelocity = transform.up * speed_;
    }

    private void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) > range_)
        {
            Destroy(gameObject);
        }
    }

    public void SetRange(float range)
    {
        range_ = range;
    }

}
