using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private PlayerController playerController;

    [Header("UI References")]
    private HealthBar healthBar;

    private void Awake() {

        healthBar = GetComponentInChildren<HealthBar>();

    }

    private void Start() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healthBar.slider.value = playerController.maxHealth;

    }

    public void UpdateHealthBar(float health) {

        DOTween.To(() => healthBar.slider.value, x => healthBar.slider.value = x, health, healthBar.slideTime);

    }
}
