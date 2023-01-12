using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {

    protected NavMeshAgent agent;
    protected Vector3 target;

    [Header("Attack")]
    [SerializeField] protected float attackDamage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected int attackCooldown;

    private void Start() {

        agent = GetComponent<NavMeshAgent>();

    }

    private void FixedUpdate() {

        UpdatePath();
        CheckAttack();

    }

    protected abstract void UpdatePath();

    protected abstract void CheckAttack();

}
