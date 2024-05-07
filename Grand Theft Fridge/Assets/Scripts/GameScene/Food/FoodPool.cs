using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FoodPool : MonoBehaviour
{
    public static FoodPool instance;

    private List<GameObject> _foods = new List<GameObject>();
    private int _amountToPool = 3;

    [SerializeField] private GameObject _food;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < _amountToPool; i++)
        {
            GameObject obj = Instantiate(_food);
            obj.SetActive(false);
            _foods.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < _foods.Count; i++)
        {
            if (!_foods[i].activeInHierarchy)
            {
                return _foods[i];
            }
        }
        return null;
    }
}
