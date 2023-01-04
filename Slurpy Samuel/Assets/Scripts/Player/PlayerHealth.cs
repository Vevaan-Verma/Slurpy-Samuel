using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [Header("Health")]
    [SerializeField] private int health;

    public void DamagePlayer(int damage) {

        health -= damage;

    }
}
