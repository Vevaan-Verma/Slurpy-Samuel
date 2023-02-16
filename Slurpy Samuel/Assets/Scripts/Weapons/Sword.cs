using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Sword : MonoBehaviour {

    [Header("Attacking")]
    [HideInInspector] public bool attackQueued;
    [HideInInspector] public bool canQueueAttack;
    [SerializeField] protected float damage;
    [SerializeField] protected float maxComboInterval;
    [SerializeField] protected LayerMask enemyMask;

    [Header("Animations")]
    public AnimationClip[] attackAnimations;
    [HideInInspector] public float lastAttack;
    [HideInInspector] public int currAnimation;
    [HideInInspector] public Vector3 originalPosition;
    protected Animator animator;

    private void Awake() {

        animator = GetComponent<Animator>();

        lastAttack = float.MinValue;

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

    public void OnAnimationEnd() {

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
