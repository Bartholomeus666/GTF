using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScoreTrigger : MonoBehaviour
{
    public AudioSource src;
    public AudioClip ScorePoint;
    public int Score;

    private BasicAttack _grabScript;

    private ScoreManager _scoreManager;

    [SerializeField] private GameObject _scoreBomb;


    private void Start()
    {
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").gameObject.GetComponent<ScoreManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Interactable"))
        {
            Destroy(other.gameObject);

            //Play score sound
            src.clip = ScorePoint;
            src.Play();
        }
        else if (other.gameObject.tag.Equals("Player"))
        {
            _grabScript = other.GetComponent<BasicAttack>();

            SpawnAndAssign idScript = other.GetComponent<SpawnAndAssign>();
            MoveRemi moveScript = other.GetComponent<MoveRemi>();

            if (_grabScript.IsHoldingFood)
            {
                Instantiate(_scoreBomb, transform.position, Quaternion.identity);

                AssignPointEvent(idScript.PlayerID);


                _grabScript.IsHoldingFood = false;
            }


            moveScript.Respawning = true;

        }
    }

    private void AssignPointEvent(int playerNr)
    {
        _scoreManager.Points[playerNr - 1]++;

        if(_scoreManager.Points[playerNr - 1] == 10)
        {
            PlayerPrefs.SetInt("Winner", playerNr);
            SceneManager.LoadScene("StatsScene");
        }
    }
}
