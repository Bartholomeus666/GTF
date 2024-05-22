using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LookingForRats : MonoBehaviour
{

    private GameObject[] _players;

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

            Vector3 cameraToPlayerVector = centerPositionPlayer - Camera.transform.position;

            Ray ray = new Ray(Camera.transform.position, cameraToPlayerVector.normalized);

            Debug.Log("Raycasting");

            if(Physics.Raycast(ray, out RaycastHit hit, 500, LayerMask))
            {
                Debug.Log(hit.collider.gameObject.tag);


                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    GameObject hitPlayer = hit.collider.gameObject;
                    SpawnAndAssign playerIdScript = hitPlayer.GetComponent<SpawnAndAssign>();

                    LiveManager losingLifeScript = LifeUI[playerIdScript.PlayerID - 1].GetComponent<LiveManager>();
                    if (losingLifeScript.LoseLife())
                    {
                       MoveRemi moveScript = hitPlayer.GetComponent<MoveRemi>();

                        moveScript.RemiGotCaught();
                    }

                    
                }
            }
        }
        BackToSplitscreen.Invoke();       
    }

    private void OnDrawGizmos()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in _players)
        {
            Vector3 centerPositionPlayer = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            Gizmos.DrawLine(centerPositionPlayer, Camera.transform.position);
        }
    }
}
