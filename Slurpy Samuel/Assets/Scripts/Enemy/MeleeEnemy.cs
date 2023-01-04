using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy {

    [Header("Specifics")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerMask;
    private float nextAttack;

    protected override void CheckAttack() {

        if (Physics.CheckSphere(transform.position, attackRange, playerMask)) {

            agent.isStopped = true;
            transform.LookAt(player);
            Attack();

        } else {

            agent.isStopped = false;

        }
    }

    protected override void UpdatePath() {

        agent.SetDestination(player.position);

    }

    private void Attack() {

        if (Time.time > nextAttack) {

            playerHealth.DamagePlayer(attackDamage);
            nextAttack = Time.time + attackCooldown;

        }
    }

    private void OnDrawGizmos() {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, attackRange);

    }
}
