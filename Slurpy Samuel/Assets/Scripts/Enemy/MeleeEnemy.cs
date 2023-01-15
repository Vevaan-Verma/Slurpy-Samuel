using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy {

    [Header("Specifics")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerMask;
    private float nextAttack;

    protected override void CheckAttack() {

        agent.stoppingDistance = attackRange;

        if (Physics.CheckSphere(transform.position, attackRange, playerMask)) {

            Attack();

        }
    }

    protected override void UpdatePath() {

        agent.SetDestination(player.position);

    }

    private void Attack() {

        if (Time.time > nextAttack) {

            playerHealth.TakeDamage(attackDamage);
            nextAttack = Time.time + attackCooldown;

        }
    }

    public override void TakeDamage(float damage) {

        health -= damage;

        if (health <= 0) {

            Die();

        }
    }

    protected override void Die() {

        Destroy(gameObject);

    }
}
