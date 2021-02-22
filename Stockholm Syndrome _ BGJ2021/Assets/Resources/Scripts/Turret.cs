using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public Transform enemy;
    [SerializeField] Transform firePoint;
    [SerializeField] private Vector2 lineOfSight;
    [SerializeField] private LayerMask enemyLayer;

    private float timeBtwShots;
    public float startTimeBtwShots;

    void Start()
    {
            timeBtwShots = startTimeBtwShots;
    }

    // Update is called once per frame
    void Update()
    {
        Ally();
    }     
    private void Ally()
    {
            //Debug.Log("Enemy Present");
            if (timeBtwShots <= 0)
            {
                Fire();
                timeBtwShots = startTimeBtwShots;
            }

            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }

    void Fire()
    {
        GameObject tmpBullet = Instantiate(bullet, firePoint.transform.position, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSight);
    }
}
