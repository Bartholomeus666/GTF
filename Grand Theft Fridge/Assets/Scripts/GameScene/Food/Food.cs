using System;
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

    private GameObject _skin;
    public RandomizerDatabase RandomizerDB;

    private Rigidbody _rb;
    private Collider _col;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        AddSkin();
    }

    private void AddSkin()
    {
        int index = UnityEngine.Random.Range(0, RandomizerDB.SkinsCount);
        _skin = Instantiate(RandomizerDB.Skins[index]);

        _skin.transform.position = transform.position;
        _skin.transform.SetParent(transform, true);
    }

    private void OnDisable()
    {
        Destroy(_skin);
    }

    private void Update()
    {
        if (Grabbed)
        {
            _rb.isKinematic = true;
            _col.isTrigger = true;

            transform.position = Player.transform.position + Player.transform.TransformDirection(new Vector3(0, 1.2f, 1.2f));

            _grabScript = Player.GetComponentInParent<BasicAttack>();

            if (!_grabScript.IsHoldingFood)
            {
                _rb.isKinematic = false;
                _col.isTrigger = false;

                Grabbed = false;
            }
        }
        else {Player = this.gameObject;}
    }
}
