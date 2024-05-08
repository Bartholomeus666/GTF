using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool Grabbed = false;

    public GameObject Player;
    private BasicAttack _grabScript;

    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    private Rigidbody _rb;
    private Collider _collider;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Grabbed)
        {
            _rb.isKinematic = true;
            _collider.isTrigger = true;

            transform.position = Player.transform.position + Player.transform.TransformDirection(new Vector3(0, 1.2f, 1.2f));

            _grabScript = Player.GetComponentInParent<BasicAttack>();

            if (!_grabScript.IsHoldingFood)
            {
                _rb.isKinematic = false;
                _collider.isTrigger = false;
                Grabbed = false;
            }
        }
        else {Player = this.gameObject;}


    }
}
