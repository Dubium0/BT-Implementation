using BT_Implementation;

using UnityEngine;
using Utility;


public class Alien : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    public float Speed
    {
        get { return speed; }
    }   

    [SerializeField]
    private float shootRadius = 10.0f;

    [SerializeField]
    private float shootDelay = 0.2f;

    public float ShootRadius
    {
        get { return shootRadius; }
    }

    [SerializeField]
    private Blackboard blackboard;

    private AlienAIFacade alienAIFacade;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float health = 50.0f;

    [SerializeField]
    private GameEvent onAlienDeath;
    private void Start()
    {
        var alienBlackboardFactory = new AlienBlackboardFactory(this);

        blackboard = alienBlackboardFactory.GetBlackboard();

        alienAIFacade = new AlienAIFacade(blackboard);
        alienAIFacade.ConstructBT();
    }
    private void FixedUpdate()
    {
        alienAIFacade.ExecuteBT();
    }
    

    public void MoveAmount(Vector3 amount)
    {
        transform.position += amount;
    }

    private float lastBulletTime = 0;
    public void Shoot(Vector3 direction)
    {   
        if(lastBulletTime + shootDelay > Time.time)
        {
            return;
        }
        lastBulletTime = Time.time;
        var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.up = direction;
        bullet.GetComponent<Bullet>().SetRange(shootRadius);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            health -= 10.0f;
            if (health <= 0)
            {
                onAlienDeath.Raise();
                Destroy(gameObject);
            }
        }
    }

}

