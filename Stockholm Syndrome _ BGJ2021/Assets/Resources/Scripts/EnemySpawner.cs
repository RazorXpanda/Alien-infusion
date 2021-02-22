using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] spawnPoint = new GameObject[5];
    public GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    public EnemyAI enemyAI;
    public Transform target;
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(Spawner(1/spawnRate));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawner(float timeToNextSpawn)
    {
        yield return new WaitForSeconds (3); //this does nothing
        while (true)
        {
            int rand = Random.Range(0,spawnPoint.Length);
            SpriteRenderer rend = spawnPoint[rand].GetComponentInChildren<SpriteRenderer>();
            rend.color = new Color (149,255,0);
            Instantiate(enemyPrefab, spawnPoint[rand].transform.position, Quaternion.identity);
            enemyAI.target = target;
            audioSource.PlayOneShot(audioClip);
            yield return new WaitForSeconds(1f);  
            rend.color = new Color (255,255,255);            
            Debug.Log(timeToNextSpawn);
            timeToNextSpawn = timeToNextSpawn - 0.03f;
            yield return new WaitForSeconds(timeToNextSpawn);
        }

    }
}
