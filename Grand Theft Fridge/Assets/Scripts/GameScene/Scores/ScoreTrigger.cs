using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreTrigger : MonoBehaviour
{
    public UnityEvent AssignPoint;

    [SerializeField] private int PlayerNr;

    public int Score;

    private BasicAttack _grabScript;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Interactable"))
        {
            AssignPoint.Invoke();
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.tag.Equals("Player"))
        {
            _grabScript = other.GetComponent<BasicAttack>();

            _grabScript.IsHoldingFood = false;
        }
    }

    public void AssignPointEvent()
    {
        Score++;
    }
}
