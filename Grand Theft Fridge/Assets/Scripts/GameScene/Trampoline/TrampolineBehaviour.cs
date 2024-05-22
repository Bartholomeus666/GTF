using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class TrampolineBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Prefab; 
    public float yValue { get; set; }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("TRIGGGEEEEER");
            MoveRemi moveScrpt = other.gameObject.GetComponent<MoveRemi>();
            float jumpForce = moveScrpt.JumpForce*1.4f;
           
            moveScrpt.yValue = 0;
            Debug.Log("Jumped");
            moveScrpt.yValue += jumpForce;
          

        }
    }



}