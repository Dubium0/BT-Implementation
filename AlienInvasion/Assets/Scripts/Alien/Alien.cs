using BT_Implementation;

using UnityEngine;


public class Alien : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    public float Speed
    {
        get { return speed; }
    }   

    [SerializeField]
    private float shootRadius = 5.0f;

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

}

