using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text[] Scores = new TMP_Text[4];
    public GameObject[] ScoreTriggers = new GameObject[4];

    private ScoreTrigger _scoreScript;

    private void Update()
    {
        for (int i = 0; i < Scores.Length; i++)
        {
            _scoreScript = ScoreTriggers[i].gameObject.GetComponent<ScoreTrigger>();

            Scores[i].text = "Score: " + _scoreScript.Score;
        }
    }
}
