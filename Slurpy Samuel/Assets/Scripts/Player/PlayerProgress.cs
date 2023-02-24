using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour {

    [Header("References")]
    [SerializeField] private UIController UIController;

    [Header("Progress")]
    private int souls;
    private int kills;

    public void AddSouls(int amount) {

        souls += amount;
        UIController.UpdateSoulCounter();

    }

    public void RemoveSouls(int amount) {

        souls -= amount;
        UIController.UpdateSoulCounter();

    }

    public int GetSouls() {

        return souls;

    }

    public void AddKill() {

        kills++;

    }

    public int GetKills() {

        return kills;

    }
}
