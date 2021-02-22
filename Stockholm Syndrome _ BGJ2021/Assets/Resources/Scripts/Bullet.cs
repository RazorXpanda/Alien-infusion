using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int baseDamage;
    public EnemyHealth enemyHealth;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            enemyHealth = col.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(baseDamage);
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "Pit" || col.gameObject.tag == "Ally")
        {
            Destroy(gameObject);
        }
    }
}
