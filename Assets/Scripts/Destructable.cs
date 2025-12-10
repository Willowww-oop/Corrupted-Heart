using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] float health = 20f;
    [SerializeField] GameObject destroyFX;

    bool destroyed = false; 

    public float Health { get { return health; } set { health = (value < 0) ? 0 : value; } }

    public void ApplyDamage(float damage)
    {
        if (destroyed) return;

        health -= damage;
        if (health < 0)
        {
            destroyed = true;
            if (destroyFX != null) Instantiate(destroyFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
