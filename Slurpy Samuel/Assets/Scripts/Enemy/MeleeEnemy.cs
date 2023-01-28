using System.Collections;
using UnityEngine;

public class MeleeEnemy : Enemy {

    [Header("Specifics")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private LayerMask playerMask;

    protected override void CheckAttack() {

        if (Physics.CheckSphere(playerController.transform.position, attackRange, playerMask) && !isDead) {

            Attack();

        }
    }

    protected override void UpdatePath() {

        if (!isDead) {

            agent.SetDestination(playerController.transform.position);

        }
    }

    private void Attack() {

        if (Time.time > nextAttack) {

            playerController.TakeDamage(attackDamage);
            nextAttack = Time.time + attackCooldown;

        }
    }

    public override void TakeDamage(float damage) {

        if (!isDead) {

            health -= damage;

            if (health <= 0) {

                Die();

            }
        }
    }

    protected override void Die() {

        isDead = true;
        agent.enabled = false;
        Destroy(gameObject, 5f);

    }
}
