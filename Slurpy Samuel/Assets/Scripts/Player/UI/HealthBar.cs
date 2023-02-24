using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [Header("References")]
    [HideInInspector] public Slider slider;

    [Header("Slide Transition")]
    [Range(0f, 5f)] public float transitionDuration;

    private void Start() {

        slider = GetComponentInChildren<Slider>();

    }
}
