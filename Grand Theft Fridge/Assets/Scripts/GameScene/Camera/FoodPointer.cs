using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.GraphicsBuffer;
using Image = UnityEngine.UIElements.Image;

public class FoodPointer : MonoBehaviour
{
    private Vector3 _targetPosition;
     private RectTransform _pointerRectTransform ;
    private GameObject _pointer;
    private GameObject _closestFood = null;
    [SerializeField] private Vector3 _currentPosition;
    private GameObject _player;
    private Camera _playerCamera;
    private float _borderSize = 100f;
    private bool _isImageActive;
    private Canvas _canvas;
    void Start()
    {
        _isImageActive = this.GetComponent<UnityEngine.UI.Image>().enabled;
        _pointerRectTransform = GetComponent<RectTransform>();
        if (_pointerRectTransform == null)
        {
            Debug.LogError("RectTransform component not found on the pointer object!");
            return;
        }

        _player = GameObject.FindWithTag("Player");
        _playerCamera = GameObject.FindWithTag("PlayerCam").GetComponent<Camera>();
        if (_playerCamera == null) 
        {
                    Debug.LogError("PlayerCam not assigned in the SpawnAndAssign component!");
        }
        _canvas = GetComponentInParent<Canvas>();
        if (_canvas == null)
        {
            Debug.LogError("Canvas component not found in parent!");
        }
    }

    void Update()
    {
        if (_player == null || _playerCamera == null || _pointerRectTransform == null)
        {
            return;
        }

        _currentPosition = _player.transform.position;
        _closestFood = FindClosestFoodWithTag("Interactable");

        if (_closestFood != null)
        {
            Vector3 toPosition = _closestFood.transform.position;
            Vector3 direction = (toPosition - _currentPosition).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;  // Adjust angle to match the UI

            _pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            Vector3 targetPositionScreenPoint = _playerCamera.WorldToScreenPoint(toPosition);
            bool isOffScreen = targetPositionScreenPoint.z < 0 ||
                               targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width ||
                               targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
            if (isOffScreen)
            {
                GetComponent<UnityEngine.UI.Image>().enabled = true;

                // Clamp the position within the screen borders
                Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, _borderSize, Screen.width - _borderSize);
                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, _borderSize, Screen.height - _borderSize);

                // Convert the clamped screen position to the canvas position
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvas.transform as RectTransform,
                cappedTargetScreenPosition,
                    _canvas.worldCamera,
                    out Vector2 localPoint);

                _pointerRectTransform.localPosition = localPoint;
                _pointerRectTransform.localPosition = new Vector3(_pointerRectTransform.localPosition.x, _pointerRectTransform.localPosition.y, 0f);
            }
            else
            {
                GetComponent<UnityEngine.UI.Image>().enabled = true;

                // Convert the screen position of the food to the canvas position
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvas.transform as RectTransform,
                    targetPositionScreenPoint,
                    _canvas.worldCamera,
                    out Vector2 localPoint);

                _pointerRectTransform.localPosition = localPoint + new Vector2(-20f, 70f);

                // Set the pointer rotation to zero when the food is on-screen
                _pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        else
        {
            GetComponent<UnityEngine.UI.Image>().enabled = false; // Hide the pointer if no food is found
        }
    }

    private GameObject FindClosestFoodWithTag(string tag)
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestFood = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (GameObject food in foods)
        {
            Vector3 directionToTarget = food.transform.position - _currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                closestFood = food;
            }
        }

        return closestFood;
    }
}