using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public EnemySpawner spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spawner.ActiveSpawner();
        }
    }
}
