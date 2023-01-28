using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class InputManager : MonoBehaviour {

    [Header("References")]
    public PlayerInput playerInput;
    private PlayerController playerController;

    [Header("Input Actions")]
    private InputActionMap playerMap;
    private InputActionMap weaponMap;

    private void OnEnable() {

        playerMap = playerInput.Player;
        weaponMap = playerInput.Weapon;

        playerMap.Enable();
        weaponMap.Enable();

    }

    private void OnDisable() {

        playerMap.Disable();
        weaponMap.Disable();

    }

    private void Awake() {

        playerInput = new PlayerInput();

    }

    private void Start() {

        playerController = GetComponent<PlayerController>();

        playerInput.Player.Sprint.performed += ctx => playerController.ToggleSprint();
        playerInput.Player.Sprint.canceled += ctx => playerController.ToggleSprint();
        playerInput.Player.Jump.performed += ctx => playerController.Jump();
        playerInput.Player.Crouch.performed += ctx => playerController.ToggleCrouch();
        playerInput.Weapon.Attack.performed += ctx => playerController.Attack();

    }

    private void FixedUpdate() {

        playerController.Move(playerInput.Player.Movement.ReadValue<Vector2>());

    }

    private void LateUpdate() {

        playerController.Look(playerInput.Player.Look.ReadValue<Vector2>());
        playerController.SwitchSwords(playerInput.Weapon.ScrollWheel.ReadValue<float>());

    }
}