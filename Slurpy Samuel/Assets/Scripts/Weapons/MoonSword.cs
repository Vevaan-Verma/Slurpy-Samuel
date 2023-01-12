using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSword : Sword {

    public override void Attack() {

        animator.Play(attackAnimations[currAnimation].name);

        if (Time.time <= lastAttack + attackAnimations[currAnimation].length + maxComboIntervalMultiplier) {

            currAnimation++;

            if (currAnimation == attackAnimations.Length) {

                currAnimation = 0;

            }
        } else {

            currAnimation = 0;

        }

        lastAttack = Time.time;

    }
}
