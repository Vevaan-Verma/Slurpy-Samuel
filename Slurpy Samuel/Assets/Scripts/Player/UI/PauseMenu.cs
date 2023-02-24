using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [Header("References")]
    [HideInInspector] public Image image;
    [HideInInspector] public MenuButton[] buttons;

    [Header("Transition Trackers")]
    [HideInInspector] public bool isOpening;
    [HideInInspector] public bool isClosing;
    [HideInInspector] public bool isOpen;

    [Header("Fade Transition")]
    public Color fadeColor;
    [Range(0f, 5f)] public float fadeInDuration;
    [Range(0f, 5f)] public float fadeOutDuration;
    [HideInInspector] public Color startColor;

    private void Awake() {

        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
        startColor = image.color;

        buttons = GetComponentsInChildren<MenuButton>();

    }
}
