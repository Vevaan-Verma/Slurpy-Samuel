using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour {

    [Header("References")]
    [HideInInspector] public Button button;
    [HideInInspector] public TMP_Text text;

    [Header("Transition Trackers")]
    [HideInInspector] public bool isHoveringIn;
    [HideInInspector] public bool isHoveringOut;

    [Header("Slide Transition")]
    public Vector3 slidePosition;
    [Range(0f, 5f)] public float slideInDuration;
    [Range(0f, 5f)] public float slideOutDuration;
    [HideInInspector] public Vector3 startPosition;

    [Header("Hover Transition")]
    public Color hoverColor;
    [Range(0f, 5f)] public float hoverInDuration;
    [Range(0f, 5f)] public float hoverOutDuration;
    [HideInInspector] public Color startColor;

    private void Awake() {

        button = GetComponent<Button>();

        text = GetComponentInChildren<TMP_Text>();
        //text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        startColor = text.color;

        startPosition = transform.localPosition;

    }
}
