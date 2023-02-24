using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public void SpawnEnemy(GameObject enemy) {

        Instantiate(enemy, transform.position, Quaternion.identity);

    }
}
