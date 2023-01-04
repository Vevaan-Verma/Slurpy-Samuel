using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour {

    protected NavMeshAgent agent;
    protected Vector3 target;

    [Header("Attack")]
    [SerializeField] protected int attackDamage;
    [SerializeField] protected int attackCooldown;
    [SerializeField] protected float attackRange;

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
