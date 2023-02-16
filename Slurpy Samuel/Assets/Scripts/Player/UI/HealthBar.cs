using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [Header("References")]
    [HideInInspector] public Slider slider;

    [Header("Transitions")]
    public float slideTime;

    private void Start() {

        slider = GetComponentInChildren<Slider>();

    }
}
