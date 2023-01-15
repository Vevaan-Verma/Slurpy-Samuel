using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [Header("Health")]
    [SerializeField] private float health;

    public void TakeDamage(float damage) {

        health -= damage;

    }
}
