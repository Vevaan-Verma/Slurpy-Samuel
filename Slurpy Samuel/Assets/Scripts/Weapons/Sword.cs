using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sword : MonoBehaviour {

    [Header("Attacking")]
    [HideInInspector] public bool attackQueued;
    [Range(0, 5)] public float attackCooldown;
    [SerializeField] protected float range;
    [SerializeField] protected float damage;
    [SerializeField] protected float maxComboIntervalMultiplier;
    [SerializeField] protected LayerMask enemyMask;

    [Header("Animations")]
    public AnimationClip[] attackAnimations;
    [HideInInspector] public int currAnimation;
    [HideInInspector] public float lastAttack;
    protected Animator animator;

    private void Start() {

        animator = GetComponent<Animator>();

    }

    private void FixedUpdate() {

        if (attackQueued && Time.time > lastAttack + attackAnimations[currAnimation].length) {

            Attack();
            attackQueued = false;

        }
    }

    public abstract void Attack();

}
