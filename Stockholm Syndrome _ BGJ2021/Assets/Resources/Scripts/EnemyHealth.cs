using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    //Attach this to each enemy prefab
    private static int startHealth = 100;
    [SerializeField] private float health;
    private float randomVal;
    [SerializeField] private bool isDead;
    [SerializeField] public bool canHostage;
    [SerializeField] private float maskRadius;

    [Header("Health UI")]
    public Color healthColor;
    public Slider healthSlider;
    public Image fillImage;
    public Healthbar healthBar;
 
    public LayerMask mask;
    private float regenHealth = 0f;
    GameObject player;
    [SerializeField] private float damageDealt;
    private PlayerController playerController;

    private AudioSource audioSource;
    [SerializeField] private AudioClip clip_attacked;
    [SerializeField] private AudioClip restoreHP;

    [Header ("For Hostage Situation")]
    [SerializeField] float distanceSize;
    [SerializeField] bool isHealing = false;

    private bool canScoreUp  = true;

    //Animation component
    private Animator animator;
    [SerializeField] private AnimationClip deadClip; //gets duration of clip

    [SerializeField] private float regenVal = 5f;
    private float timerResetValue = 0;
    private float timer = 0;
    [SerializeField] private float timeToDie;

    private void Start()
    {
        healthBar.SetMaxHealth(startHealth, startHealth);
        health = startHealth;
        healthSlider.maxValue = startHealth;
        healthSlider.minValue = 0f;
        isDead = false;
        canHostage = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            health -= damage;
            health = Mathf.Clamp (health, 0, 100);
            healthBar.SetHealthUI(health);
            audioSource.PlayOneShot(clip_attacked);
            //animator.SetTrigger("isAttacked");
        }
        if (health <= 0)
        {
            randomVal = Random.value;
            Debug.Log (randomVal);
            if (randomVal <= 0.3) //30% chance
            {
                Destroy(GetComponent<EnemyNewScript>());
                isDead = true;
                canHostage = true;
                animator.SetBool("isDead", isDead);
                Debug.Log("Hostage");
                if (canScoreUp)
                {
                    PlayerData.enemyKilled += 1;
                    KillCounter.instance.OnTextUpdate();
                    canScoreUp = false;
                }
            }
            
            else if (canHostage == false)
            {
                animator.SetBool("isDead", true);
                StartCoroutine(Die());
            }
        }
    }

    private void Update()
    {
        if (canHostage)
        {
            timer = timer + 1f * Time.deltaTime;
            Debug.Log(timer);
            if (timer >= timeToDie)
            {
                Destroy(gameObject);
                timer = timerResetValue;
            }
            checkforPlayer();
        }
    }

    void checkforPlayer()
    {
        float playerDistanceFromCharacter = Vector3.Distance(player.transform.position, transform.position);
        if(playerDistanceFromCharacter < distanceSize)
        {
            //Debug.Log("Player in range");
            if (Input.GetKey(KeyCode.F) && (!Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.D)))
            {
                Debug.Log("Reviving");
                Revival();
            }
            if (Input.GetKeyUp(KeyCode.F))
            {
                animator.SetBool("isHealing", false);
            }
        }
    }
    IEnumerator Die()
    {
        PlayerData.enemyKilled += 1;
        KillCounter.instance.OnTextUpdate();
        Destroy(this.gameObject);
        yield return new WaitForEndOfFrame();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Pit")
        {
            Destroy(this.gameObject);
        }
    }

    private void Revival()
    {
        isHealing = true;
        playerController.HealDamage(regenVal);
        regenHealth = regenHealth +  regenVal * Time.deltaTime;
        Debug.Log(regenHealth);
        animator.SetBool("isHealing", isHealing);
        audioSource.PlayOneShot(restoreHP, 0.1f);
        
        if (regenHealth >= 15f)
        {
            Destroy(gameObject);
        }
        // health += regenHealth * Time.deltaTime;
        // healthBar.SetHealthUI(health);
        // if (health >= 100)
        // {
        //     SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();
        //     rend.color = new Color (255,255,0); 
        //     Instantiate(prefabVariant, transform.position, Quaternion.identity);        
        //     Destroy(this.gameObject);              
        // }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            if (!isDead)
            {
                playerController.TakeDamage(damageDealt);
                StartCoroutine(Die());
            }            
        }
    }
}
