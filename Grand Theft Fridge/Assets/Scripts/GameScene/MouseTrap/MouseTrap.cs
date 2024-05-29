using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseTrap : MonoBehaviour
{
    public bool Activated = true;

    [SerializeField] private GameObject[] LifeUI = new GameObject[4];
    private void Start()
    {
        Activated = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit");

        if(Activated && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player on trap");

            SpawnAndAssign playerIdScript = collision.gameObject.GetComponent<SpawnAndAssign>();
            LiveManager losingLifeScript = LifeUI[playerIdScript.PlayerID - 1].GetComponent<LiveManager>();
            if (losingLifeScript.LoseLife())
            {
                MoveRemi moveScript = collision.gameObject.GetComponent<MoveRemi>();

                moveScript.RemiGotCaught();
            }
            Activated = false;
        }
        else
        {
            return;
        }
    }
}
