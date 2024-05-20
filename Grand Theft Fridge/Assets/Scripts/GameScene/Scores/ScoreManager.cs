using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int[] Points = new int[4];
    public TMP_Text[] Scores = new TMP_Text[4];
    //public GameObject[] ScoreTriggers = new GameObject[1];

    private ScoreTrigger _scoreScript;

    private void Awake()
    {
        this.gameObject.tag = "ScoreManager";
    }

    private void Update()
    {
        for (int i = 0; i < Scores.Length; i++)
        {
            //_scoreScript = ScoreTriggers[i].gameObject.GetComponent<ScoreTrigger>();

            Scores[i].text = "Score: " + Points[i];
        }
    }
}
