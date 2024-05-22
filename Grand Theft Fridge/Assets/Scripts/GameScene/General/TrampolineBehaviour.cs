using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TrampolineBehaviour : MonoBehaviour
{
    Animator jelloAnimator;

    private void Start()
    {
        jelloAnimator = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            jelloAnimator.SetBool("isBouncing", true);

            StartCoroutine(StopBouncing());

            Debug.Log("TRIGGGEEEEER");
            MoveRemi moveScrpt = other.gameObject.GetComponent<MoveRemi>();
            float jumpForce = moveScrpt.JumpForce * 2f;

            moveScrpt.yValue = 0;
            Debug.Log("Jumped");
            moveScrpt.yValue += jumpForce;
        }
    }

    private IEnumerator StopBouncing()
    {
        yield return new WaitForSeconds(1);
        jelloAnimator.SetBool("isBouncing", false);
    }



}