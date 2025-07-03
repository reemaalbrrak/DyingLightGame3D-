using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public enum EnemyType { Big, Small }

    public NavMeshAgent EnemyAgent;
    public Transform PlayerPoss;
    public float chaseDistance = 10f;

    public int maxHealth;
    private int currentHealth;

    public GameManager1 manager;
    public EnemyType enemyType;

    void Start()
    {
        ApplyStats(); // Apply settings based on type
        currentHealth = maxHealth;

       
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayZombieSpawn();
        }
    }

    public void SetEnemyType(EnemyType type)
    {
        enemyType = type;
        ApplyStats();
    }

    void ApplyStats()
    {
        if (enemyType == EnemyType.Big)
        {
            maxHealth = 5;
            if (EnemyAgent != null) EnemyAgent.speed = 2f; // Slower
        }
        else if (enemyType == EnemyType.Small)
        {
            maxHealth = 1;
            if (EnemyAgent != null) EnemyAgent.speed = 4f; // Faster
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerPoss.position);

        if (distanceToPlayer <= chaseDistance)
        {
            EnemyAgent.SetDestination(PlayerPoss.position);
        }
        else
        {
            EnemyAgent.ResetPath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            currentHealth--;

            if (currentHealth <= 0)
            {
                if (manager != null)
                {
                    manager.EnemyKilled();
                }
                Destroy(gameObject);
                Debug.Log("Enemy destroyed!");
            }
        }
    }
}
