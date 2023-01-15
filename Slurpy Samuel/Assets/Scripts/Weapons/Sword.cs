using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour {

    [Header("Attacking")]
    [Range(0, 10)] public float attackQueueLenience;
    [Range(0, 5)] public float attackCooldown;
    [HideInInspector] public bool attackQueued;
    [HideInInspector] public bool canQueueAttack;
    [SerializeField] protected float range;
    [SerializeField] protected float damage;
    [SerializeField] protected float maxComboInterval;
    [SerializeField] protected LayerMask enemyMask;

    [Header("Animations")]
    public AnimationClip[] attackAnimations;
    [HideInInspector] public int currAnimation;
    [HideInInspector] public float lastAttack;
    protected Animator animator;

    private void Start() {

        animator = GetComponent<Animator>();

    }

    public void Attack() {

        animator.Play(attackAnimations[currAnimation].name);

        if (Time.time <= lastAttack + attackAnimations[currAnimation].length + maxComboInterval) {

            currAnimation++;

            if (currAnimation == attackAnimations.Length) {

                currAnimation = 0;

            }
        } else {

            currAnimation = 0;

        }

        lastAttack = Time.time;

    }

    public void EnableAttackQueue() {

        canQueueAttack = true;

    }

    public void CheckAttackQueue() {

        canQueueAttack = false;

        if (attackQueued) {

            Attack();
            attackQueued = false;

        }
    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.collider.CompareTag("Enemy")) {

            collision.transform.GetComponent<Enemy>().TakeDamage(damage);

        }
    }
}
