using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int[] Points = new int[4];
    public TMP_Text[] Scores = new TMP_Text[4];
    public Image[] Positions = new Image[4];
    public Sprite[] PositionsSprites = new Sprite[4];
    //public GameObject[] ScoreTriggers = new GameObject[1];

    private ScoreTrigger _scoreScript;

    private int _playersBetter;

    private void Awake()
    {
        this.gameObject.tag = "ScoreManager";
    }

    private void Update()
    {
        for (int i = 0; i < Scores.Length; i++)
        {
            //_scoreScript = ScoreTriggers[i].gameObject.GetComponent<ScoreTrigger>();

            Scores[i].text = ($"{Points[i]}/5");
        }
        CheckPositions();
    }

    private void CheckPositions()
    {
        for (int i = 0; i < Points.Length; i++)
        {
            _playersBetter = 0;
            for (int j = 0; j < Points.Length; j++)
            {
                if (Points[i] < Points[j])
                {
                    _playersBetter++;
                }
            }
            Positions[i].sprite = PositionsSprites[_playersBetter];
        }
    }
}
