using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class AchievementNotificationController : MonoBehaviour {

    [SerializeField] Text achievementTitleLabel;

    Animator animator;

    private void Awake () {
        animator = GetComponent<Animator> ();
    }


    public void ShowNotification(Achievement achievement) {
        achievementTitleLabel.text = achievement.title;
        animator.SetTrigger ("Appear");
    }

}
