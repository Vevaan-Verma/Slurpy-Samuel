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

    private void OnEnable() {

        playerMap = playerInput.Player;

        playerMap.Enable();

    }

    private void OnDisable() {

        playerMap.Disable();

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

    }

    private void FixedUpdate() {

        playerController.Move(playerInput.Player.Movement.ReadValue<Vector2>());

    }

    private void LateUpdate() {

        playerController.Look(playerInput.Player.Look.ReadValue<Vector2>());

    }
}