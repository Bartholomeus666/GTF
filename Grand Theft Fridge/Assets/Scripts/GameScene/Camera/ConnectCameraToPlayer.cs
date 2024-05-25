using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCameraToPlayer : MonoBehaviour
{
    public GameObject Player;
    private Vector3 _playerPostition;
    [SerializeField] private float zValue;
    [SerializeField] private float extraYValue;

    public int CamerId = 0;


    private void Start()
    {
        Player.SetActive(true);

        CamerId = Player.GetComponent<SpawnAndAssign>().PlayerID;
    }

    private void Update()
    {
        _playerPostition = Player.transform.position;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_playerPostition.x, _playerPostition.y + extraYValue, zValue), 0.2f);
    }
}
