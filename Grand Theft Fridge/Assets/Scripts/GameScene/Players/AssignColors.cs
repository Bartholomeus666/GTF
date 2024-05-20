using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignColors : MonoBehaviour
{
    [SerializeField] private Material[] _materials = new Material[4];

    public void AssignMaterials()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            SpawnAndAssign idScript = player.GetComponent<SpawnAndAssign>();

            SkinnedMeshRenderer skinnedMeshRenderer = player.GetComponentInChildren<SkinnedMeshRenderer>();

            skinnedMeshRenderer.material = _materials[idScript.PlayerID - 1];
        }
    }
}
