using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {

    [Header("References")]
    [SerializeField] protected LayerMask playerMask;
    protected PlayerController playerController;
    protected PlayerProgress playerProgress;
    protected WaveManager waveManager;
    protected Rigidbody rb;
    protected NavMeshAgent agent;
    protected Vector3 target;

    [Header("Properties")]
    [SerializeField] protected int souls;
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected int attackCooldown;
    protected float nextAttack;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    protected float health;

    [Header("Death")]
    [SerializeField] protected GameObject soulTrail;
    [SerializeField] protected float soulTrailDuration;
    [HideInInspector] public bool isDead;

    private void Awake() {

        playerController = FindObjectOfType<PlayerController>();
        playerProgress = playerController.GetComponent<PlayerProgress>();
        waveManager = FindObjectOfType<WaveManager>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Start() {

        health = maxHealth;
        agent.stoppingDistance = attackRange;

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
