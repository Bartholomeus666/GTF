using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LookingForRats : MonoBehaviour
{
    private GameObject[] _players;
    private GameObject[] _playerCameras;


    [SerializeField] private UnityEvent RaycastEvent;

    public Camera Camera;

    public LayerMask LayerMask;

    public UnityEvent BackToSplitscreen;

    public Material FoundMaterial;

    [SerializeField] private GameObject[] LifeUI = new GameObject[4];   
    public void BlockMovement()
    {
        Debug.Log("Players getting assigned");

        _players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in _players)
        {
            if (player != null)
            {
                MoveRemi moveScript = player.GetComponent<MoveRemi>();

                moveScript.enabled = false;

            }
        }

        Debug.Log("Players assigned");
        RaycastEvent.Invoke();
    }
    public void CameraToPlayers()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in _players)
        {
            Vector3 centerPositionPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);

            //Ray ray = new Ray(new Vector3(centerPositionPlayer.x, centerPositionPlayer.y, -5), Vector3.forward);

            //Vector3 cameraToPlayerVector = centerPositionPlayer - Camera.transform.position;

            //Ray ray = new Ray(Camera.transform.position, cameraToPlayerVector.normalized);

            Ray ray = new Ray(centerPositionPlayer, Vector3.back);

            Debug.Log("Raycasting");

            if (Physics.Raycast(ray, out RaycastHit hit, 500, LayerMask))
            {
                Debug.Log(hit.collider.gameObject.tag);

                if (hit.collider.gameObject.CompareTag("Invisible"))
                {
                    //GameObject hitPlayer = hit.collider.gameObject;
                    SpawnAndAssign playerIdScript = player.GetComponent/*InParent*/<SpawnAndAssign>();

                    LiveManager losingLifeScript = LifeUI[playerIdScript.PlayerID - 1].GetComponent<LiveManager>();
                    if (losingLifeScript.LoseLife())
                    {
                       MoveRemi moveScript = player.GetComponent/*InParent*/<MoveRemi>();

                        moveScript.RemiGotCaught();
                    }

                    
                }
            }
        }
        BackToSplitscreen.Invoke();
    }


    private void Update()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in _players)
        {
            Vector3 centerPositionPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);

            Debug.DrawRay(centerPositionPlayer, Vector3.back * 10);
        }
    }
    //private void OnDrawGizmos()
    //{
    //    _players = GameObject.FindGameObjectsWithTag("Player");

    //    foreach (GameObject player in _players)
    //    {
    //        Vector3 centerPositionPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
    //        Gizmos.DrawLine(centerPositionPlayer, Camera.transform.position);
    //    }
    //}
}
