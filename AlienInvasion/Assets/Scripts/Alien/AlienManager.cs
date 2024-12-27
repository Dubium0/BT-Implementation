using UnityEngine;

public class AlienManager : MonoBehaviour
{
    public static AlienManager Instance { get; private set; }

    [SerializeField]
    private int maximumAliens = 10;

    [SerializeField]
    private int spawnDelay = 1; // 1 alien per second

    private int currentAliens = 0;

    [SerializeField]
    private GameObject alienPrefab;
    

    private bool isSpawning = false;

    
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
     
    }

    public void OnAlienDeath()
    {
        currentAliens--;
    }

    private float lastSpawnTime = 0;
    private void Update()
    {
        if(lastSpawnTime + spawnDelay < Time.time && currentAliens < maximumAliens &&  isSpawning)
        {
            lastSpawnTime = Time.time;
            currentAliens++;
            Instantiate(alienPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity);
        }
    }

    public void SetSpawningTrue()
    {
        isSpawning = true;
    }

}

