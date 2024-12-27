using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SingletonPlayer : MonoBehaviour
{
    public static SingletonPlayer Instance { get; private set; }


    private Vector2 prevMovementDirection_ = Vector2.zero;

    public Vector2 PrevMovementDirection
    {
        get { return prevMovementDirection_; }
    }

    [SerializeField]
    private float speed_ = 5.0f;
    
    public float Speed
    {
        get { return speed_; }
    }

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float shootDelay = 0.5f;

    [SerializeField]
    private float shootRadius = 5.0f;

    [SerializeField]
    private float health = 100.0f;

    public float Health
    {
        get { return health; }
    }

    private Vector2 prevShootDirection_;

    public Vector2 PrevShootDirection
    {
        get { return prevShootDirection_; }
    }

    private Rigidbody2D rb2d;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {

            Destroy(this);

        }
        else
        {
            Instance = this;
        }
        rb2d = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
      
        Shoot();
    }

    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        prevMovementDirection_.x = Input.GetAxis("Horizontal");
        prevMovementDirection_.y = Input.GetAxis("Vertical");

        prevMovementDirection_.Normalize();
        rb2d.linearVelocity = new Vector2(prevMovementDirection_.x, prevMovementDirection_.y) * speed_;

            

    }

    
    private float previousShootTime_ = 0.0f;
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && previousShootTime_ + shootDelay <Time.time)
        {
            previousShootTime_ = Time.time;
            var currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            prevShootDirection_ = (currentMousePosition - transform.position).normalized;

            var bullet = Instantiate(bulletPrefab, transform.position,Quaternion.identity);
            bullet.transform.up = prevShootDirection_; 
            bullet.GetComponent<Bullet>().SetRange(shootRadius);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AlienBullet"))
        {
            health -= 10.0f;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}