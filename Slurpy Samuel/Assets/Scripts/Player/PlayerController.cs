using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Transform feet;
    public new Camera camera;
    public Transform cameraPosition;
    [HideInInspector] public Rigidbody rb;
    private new CapsuleCollider collider;

    [Header("Movement")]
    [SerializeField][Range(0f, 25f)] private float walkSpeed;
    [SerializeField][Range(0f, 25f)] private float sprintSpeed;
    [SerializeField][Range(0f, 25f)] private float crouchSpeed;
    [SerializeField][Range(0f, 25f)] private float airSpeed;
    [HideInInspector] public Vector3 movementDirection;
    private float currentSpeed;

    [Header("Looking")]
    [SerializeField][Range(0f, 100f)] private float xSensitivity;
    [SerializeField][Range(0f, 100f)] private float ySensitivity;
    [SerializeField][Range(0f, 90f)] private float topLookClamp;
    [SerializeField][Range(0f, 90f)] private float bottomLookClamp;
    [Space]
    public bool invertLookX;
    public bool invertLookY;
    [HideInInspector] public Vector2 mouseInput;
    private float xRotation;
    private float yRotation;

    [Header("Sprinting")]
    [HideInInspector] public bool isSprinting;

    [Header("Jumping")]
    [SerializeField][Range(0f, 25f)] private float jumpHeight;
    [SerializeField][Range(0f, 10f)] private float fallMultiplier;

    [Header("Crouching")]
    [SerializeField][Range(0f, 2f)] private float crouchHeight;
    [HideInInspector] public bool isCrouching;
    private float initialScale;
    private float standHeight;

    [Header("Weapons")]
    [SerializeField] private Transform swordHolder;
    private List<Sword> swords;
    private int currWeapon;
    private float nextAttack;

    [Header("Ground Check")]
    [SerializeField][Range(0f, 2f)] private float groundCheckRadius;
    [SerializeField] private LayerMask environmentMask;
    [HideInInspector] public bool isGrounded;

    [Header("Drag Control")]
    [SerializeField][Range(0f, 10f)] private float groundDrag;

    private void Start() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        collider = GetComponent<CapsuleCollider>();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        initialScale = transform.localScale.y;
        standHeight = collider.height;

        swords = new List<Sword>();

        for (int i = 0; i < swordHolder.childCount; i++) {

            swords.Add(swordHolder.GetChild(i).GetComponent<Sword>());

        }

        for (int i = 0; i < swords.Count; i++) {

            swords[i].gameObject.SetActive(false);

        }

        SetCurrentWeapon(0);

    }

    private void Update() {

        isGrounded = Physics.CheckSphere(feet.position, groundCheckRadius, environmentMask);

        cameraPosition.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);

        ControlSpeed();

        if (isGrounded) {

            rb.drag = groundDrag;

        } else {

            rb.drag = 0;

        }
    }

    private void FixedUpdate() {

        rb.AddForce(movementDirection.normalized * currentSpeed, ForceMode.Force);

    }

    public void Move(Vector2 input) {

        movementDirection = transform.forward * input.y + transform.right * input.x;

        if (isGrounded && isSprinting && !isCrouching) {

            currentSpeed = sprintSpeed * 10f;

        } else if (isGrounded && !isSprinting && !isCrouching) {

            currentSpeed = walkSpeed * 10f;

        } else if (isGrounded && isCrouching) {

            currentSpeed = crouchSpeed * 10f;

        } else if (!isGrounded) {

            currentSpeed = airSpeed * 10f;

        }
    }

    private void ControlSpeed() {

        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVelocity.magnitude > currentSpeed) {

            Vector3 limitedVelocity = flatVelocity.normalized * currentSpeed;

            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);

        }
    }

    public void Look(Vector2 input) {

        float mouseX = input.x * xSensitivity / 100;
        float mouseY = input.y * ySensitivity / 100;

        mouseInput = new Vector2(mouseX, mouseY);

        if (invertLookX) {

            yRotation -= mouseX;

        } else {

            yRotation += mouseX;

        }

        if (invertLookY) {

            xRotation += mouseY;

        } else {

            xRotation -= mouseY;

        }

        xRotation = Mathf.Clamp(xRotation, -bottomLookClamp, topLookClamp);

    }

    public void ToggleSprint() {

        if (!isSprinting && isGrounded) {

            isSprinting = true;

        } else {

            isSprinting = false;

        }
    }

    public void Jump() {

        if (isCrouching) {

            ToggleCrouch();
            return;

        }

        if (isGrounded) {

            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            float jumpForce = Mathf.Sqrt(jumpHeight * (Physics.gravity.y * -2 * rb.mass));
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        }
    }

    public void ToggleCrouch() {

        if (isGrounded) {

            isCrouching = !isCrouching;

            if (isCrouching) {

                collider.height = crouchHeight;
                transform.localScale = new Vector3(transform.localScale.x, crouchHeight, transform.localScale.z);

            } else {

                collider.height = standHeight;
                transform.localScale = new Vector3(transform.localScale.x, initialScale, transform.localScale.z);

            }
        }
    }

    public void SetCurrentWeapon(int newWeapon) {

        swords[currWeapon].gameObject.SetActive(false);
        currWeapon = newWeapon;
        swords[newWeapon].gameObject.SetActive(true);

    }

    public void Attack() {

        Sword sword = swords[currWeapon];

        if (Time.time <= sword.lastAttack + sword.attackAnimations[sword.currAnimation].length) {

            if (!sword.attackQueued) {

                sword.attackQueued = true;
                return;

            }
        }

        if (Time.time > nextAttack) {

            swords[currWeapon].Attack();
            nextAttack = Time.time + swords[currWeapon].attackCooldown;

        }
    }
}