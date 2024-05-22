using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TrampolineBehaviour : MonoBehaviour
{
    public AudioSource src;
    public AudioClip Trampoline;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("TRIGGGEEEEER");
            MoveRemi moveScrpt = other.gameObject.GetComponent<MoveRemi>();
            float jumpForce = moveScrpt.JumpForce * 2f;

            //Play trampoline sound
            src.clip = Trampoline;
            src.Play();

            moveScrpt.yValue = 0;
            Debug.Log("Jumped");
            moveScrpt.yValue += jumpForce;


        }
    }



}