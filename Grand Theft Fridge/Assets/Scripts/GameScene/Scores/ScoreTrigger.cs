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

    private ScoreManager _scoreManager;


    private void Start()
    {
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").gameObject.GetComponent<ScoreManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Interactable"))
        {
            Debug.Log("Point!");

            AssignPoint.Invoke();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag.Equals("Player"))
        {
            _grabScript = other.GetComponent<BasicAttack>();

            MoveRemi moveScript = other.GetComponent<MoveRemi>();

            _grabScript.IsHoldingFood = false;

            moveScript.Respawning = true;

        }
    }

    public void AssignPointEvent()
    {
        _scoreManager.Points[PlayerNr - 1]++;

        if(Score == 3)
        {
            PlayerPrefs.SetInt("Winner", PlayerNr);
            SceneManager.LoadScene("StatsScene");
        }
    }
}
