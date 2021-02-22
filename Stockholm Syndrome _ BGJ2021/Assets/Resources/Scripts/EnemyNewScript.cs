using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNewScript : MonoBehaviour
{
    [Header("Jump attacks")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] LayerMask enemyLayer;

    [Header("For Checking Player")]
    [SerializeField] Vector2 lineOfSight;
    [SerializeField] LayerMask playerLayer;
    private bool canSeePlayer;

    [Header ("Others")]
    private Rigidbody2D enemyRB;
    private Animator enemyAnim;


    private void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        if(player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer) || Physics2D.OverlapBox(groundCheck.position, boxSize, 0, enemyLayer);
        canSeePlayer = Physics2D.OverlapBox(transform.position, lineOfSight, 0, playerLayer);
        AnimationController();
        if(!canSeePlayer && isGrounded)
        {
            enemyRB.velocity = new Vector3(1,0,0);
        }
        // if(Input.GetKeyDown(KeyCode.O))
        // {
        //     JumpAttack();
        // }
        FlipTowardsPlayer();
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;
        //Debug.Log(distanceFromPlayer);
        distanceFromPlayer = Mathf.Clamp(distanceFromPlayer, -6f, 6f);

        if(isGrounded)
        {
            enemyRB.AddForce(new Vector2 (distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, lineOfSight);
    }

    private bool facingRight = true;
    private float moveDirection;
    void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if(playerPosition <= 0 && facingRight)
        {
            Flip();
        }
        else if(playerPosition > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 flipScale = transform.localScale;
        flipScale.x *= -1;
        transform.localScale = flipScale;
    }

    void AnimationController()
    {
        enemyAnim.SetBool("canSeePlayer", canSeePlayer);
        enemyAnim.SetBool("isGrounded", isGrounded);
    }
}
