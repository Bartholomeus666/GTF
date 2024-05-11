using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreTrigger : MonoBehaviour
{
    public UnityEvent AssignPoint;

    [SerializeField] private int PlayerNr;

    public int Score;

    private BasicAttack _grabScript;


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Interactable"))
        {
            AssignPoint.Invoke();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Player"))
        {
            _grabScript = other.GetComponent<BasicAttack>();

            MoveRemi moveScript = other.GetComponent<MoveRemi>();
            SpawnAndAssign spawn = other.GetComponent<SpawnAndAssign>();

            _grabScript.IsHoldingFood = false;

            spawn.Respawn();

        }
    }

    public void AssignPointEvent()
    {
        Score++;

        if(Score == 10)
        {
            PlayerPrefs.SetInt("Winner", PlayerNr);
            SceneManager.LoadScene("StatsScene");
        }
    }
}
