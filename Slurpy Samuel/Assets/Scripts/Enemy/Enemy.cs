using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {

    [Header("References")]
    protected Rigidbody rb;
    protected NavMeshAgent agent;
    protected Vector3 target;

    [Header("Attack")]
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected int attackCooldown;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    protected float health;

    private void Start() {

        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        health = maxHealth;

    }

    private void FixedUpdate() {

        UpdatePath();
        CheckAttack();

    }

    protected abstract void UpdatePath();

    protected abstract void CheckAttack();

    public abstract void TakeDamage(float damage);

    protected abstract void Die();

}
