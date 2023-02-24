using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerProgress playerProgress;
    [SerializeField] private InputManager inputManager;

    [Header("UI References")]
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private TMP_Text soulCounter;
    private HealthBar healthBar;

    private void Awake() {

        pauseMenu.gameObject.SetActive(true);

    }

    private void Start() {

        pauseMenu.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        soulCounter.text = "0";

        healthBar = GetComponentInChildren<HealthBar>();
        healthBar.slider.value = playerController.maxHealth;

    }

    public void UpdateHealthBar(float health) {

        healthBar.slider.DOValue(health, healthBar.transitionDuration).SetEase(Ease.InOutBack);

    }

    public void UpdateSoulCounter() {

        soulCounter.text = playerProgress.GetSouls() + "";

    }

    public void OpenPauseMenu() {

        if (!pauseMenu.isClosing) {

            Time.timeScale = 0f;

            inputManager.playerInput.Player.Disable();
            inputManager.playerInput.Weapon.Disable();
            inputManager.playerInput.Menu.Enable();

            pauseMenu.gameObject.SetActive(true);

            pauseMenu.image.DOColor(pauseMenu.fadeColor, pauseMenu.fadeInDuration).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => {

                SlideInButtons();

            });

            pauseMenu.isOpening = true;

        }
    }

    private void SlideInButtons() {

        foreach (MenuButton menuButton in pauseMenu.buttons) {

            menuButton.transform.DOLocalMove(menuButton.slidePosition, menuButton.slideInDuration).SetEase(Ease.InQuint).SetUpdate(true).OnComplete(() => {

                pauseMenu.isOpening = false;
                pauseMenu.isOpen = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            });
        }
    }

    public void ClosePauseMenu() {

        if (!pauseMenu.isOpening) {

            SlideOutButtons();
            pauseMenu.isClosing = true;

        }
    }

    private void SlideOutButtons() {

        foreach (MenuButton menuButton in pauseMenu.buttons) {

            menuButton.transform.DOLocalMove(menuButton.startPosition, menuButton.slideOutDuration).SetEase(Ease.InQuint).SetUpdate(true).OnComplete(() => {

                pauseMenu.image.DOColor(pauseMenu.startColor, pauseMenu.fadeOutDuration).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => {

                    pauseMenu.gameObject.SetActive(false);
                    pauseMenu.isClosing = false;
                    pauseMenu.isOpen = false;

                    inputManager.playerInput.Menu.Disable();
                    inputManager.playerInput.Weapon.Enable();
                    inputManager.playerInput.Player.Enable();

                    Time.timeScale = 1f;

                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                });
            });
        }
    }

    public void ResumeGame() {

        if (!pauseMenu.isOpening) {

            SlideOutButtons();
            pauseMenu.isClosing = true;

        }
    }

    public void OpenSettingsMenu() {

    }

    public void QuitGame() {

        Application.Quit();

    }
}
