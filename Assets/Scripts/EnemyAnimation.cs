using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    public Transform player;

    public float attackCooldown;
    public float moveSpeed;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = player.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", enemy.velocity.x);
        animator.SetFloat("Vertical", enemy.velocity.y);
        animator.SetFloat("Speed", enemy.velocity.sqrMagnitude);
    }

    public void Attack(bool attack)
    {
        animator.SetBool("Attack", attack);
    }
}
