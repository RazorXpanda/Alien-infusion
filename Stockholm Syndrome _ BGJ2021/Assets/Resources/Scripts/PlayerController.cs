using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20f;
    private float horizontalMove = 0f;
    private Rigidbody2D rb;
    private bool m_facingRight = true;

    //all inits related to jump and shit
    private bool isGrounded;
    private int jumpCount;
    [Range (1,10)]
    [SerializeField] private float jumpForce;

    //inits for shooting bullets
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float bulletForce;

    [SerializeField] public float health;
    public float maxHealth = 100f;
    public float overflowHealth = 150f;
    [SerializeField] private GameObject losePanel;
    public GameObject enemySpawner;
    public static bool gameOver = false;
    public static bool isStart;
    private Animator animator;
    private AudioSource m_audioSource;
    [SerializeField] private AudioClip audio_bullet;
    [SerializeField] private AudioClip audio_damaged;

    public Healthbar healthbar;

    private void Awake()
    {
        enemySpawner.SetActive(false);
        gameOver = false;
        isStart = false;
    }

    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        m_facingRight = true;
        health = maxHealth;
        losePanel.SetActive(false);
        animator = GetComponent<Animator>();
        healthbar.SetMaxHealth(maxHealth, overflowHealth);
        m_audioSource = GetComponent<AudioSource>();
    }

    public float fallMultiplier = 2.5f;

    void Update()
    {
        if (!gameOver)
        {
            if (isStart)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal");
                Vector2 pos = transform.position;
                pos.x += horizontalMove * moveSpeed * Time.deltaTime;
                transform.position = pos;

                if(horizontalMove == 0)
                {
                    animator.SetBool("isWalking", false);
                }

                else
                {
                    animator.SetBool("isWalking", true);
                }

                if (horizontalMove > 0 && !m_facingRight)
                {
                    Flip();
                }
                else if (horizontalMove < 0 && m_facingRight)
                {
                    Flip();
                }

                //Jump portion
                if (Input.GetKeyDown("space"))
                {
                    if (isGrounded)
                    {
                        rb.velocity = Vector2.up * jumpForce;

                        if (rb.velocity.y < 0)
                        {
                            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
                        }
                        //rb.AddForce(transform.up * jumpForce);
                        animator.SetBool("isJumping", true);
                        animator.SetTrigger("isJumpTrigger");
                        Debug.Log("Jumped!");

                        if (rb.velocity.y != 0)
                        {
                            animator.SetTrigger("isJumpTrigger");
                        }
                    }
                }

                if (rb.velocity.y != 0 && horizontalMove != 0)
                {
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isWalking", true);
                }

                else if (rb.velocity.y != 0 && horizontalMove == 0)
                {
                    animator.SetBool("isJumping", true);
                }

                else
                {
                    animator.SetBool("isJumping", false);
                }
                
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject bullet = Instantiate(bulletPrefab, shootPoint.transform.position, Quaternion.identity);
                    Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
                    var dir =  shootPoint.transform.position - gameObject.transform.position;
                    bulletrb.velocity = transform.TransformDirection(dir) * bulletForce;
                    m_audioSource.PlayOneShot(audio_bullet);
                    if (horizontalMove == 0)
                    {
                        animator.SetTrigger("isShooting");
                    }
                    //bulletrb.AddForce(gameObject.transform.forward * bulletForce, ForceMode2D.Impulse);           
                }
            }
        }
    }

    private void Flip()
    {
        m_facingRight = !m_facingRight;
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        transform.localScale = flipScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
           isGrounded = true; 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "DeathPit")
        {
            healthbar.SetHealthUI(0);
            Die();
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Platform")
        {
        isGrounded = false;
        }
    }

    public GameObject losePanelText;

    public void TakeDamage(float damageReceived)
    {
        health -= damageReceived;
        health = Mathf.Clamp(health, 0, overflowHealth);
        healthbar.SetHealthUI(health);
        animator.SetTrigger("isAttacked");
        m_audioSource.PlayOneShot(audio_damaged);
        if (health <=0)
        {
            Die();
            
        }
    }

    private void Die()
    {
        var enemyPrefs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var items in enemyPrefs)
        {
            Destroy(items);
        }
        KillCounter.instance.OnUITextUpdate();
        this.gameObject.SetActive(false);
        enemySpawner.SetActive(false);
        losePanel.SetActive(true);
        gameOver = true;
    }

    public void HealDamage(float damageToHeal)
    {
        health += damageToHeal * Time.deltaTime;
        healthbar.SetHealthUI(health);
    }
}