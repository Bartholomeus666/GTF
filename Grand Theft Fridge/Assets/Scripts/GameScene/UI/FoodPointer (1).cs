using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FoodPointer : MonoBehaviour
{
    private Vector3 _targetPosition;
     private RectTransform _pointerRectTransform ;
    private GameObject _pointer;
    private GameObject _closestFood = null;
    [SerializeField] private Vector3 _currentPosition;
    private GameObject[] _players;
    private Camera[] _playerCameras;
    private float _borderSize = 50f;
    private bool _isImageActive;
    private Canvas _canvas;
    private int[] _PlayerNumbers;
    private bool _isOffScreen;
    [SerializeField] private int pointerID;
    [SerializeField] private Sprite PointerSprite;
    [SerializeField] private Sprite ButtonSprite;
    [SerializeField] private float distanceToFoodSprite = 2.5f;

    void Start()
    {
        _isImageActive = this.GetComponent<UnityEngine.UI.Image>().enabled;
        _pointerRectTransform = GetComponent<RectTransform>();
        if (_pointerRectTransform == null)
        {
            Debug.LogError("RectTransform component not found on the pointer object!");
            return;
        }

        _canvas = GetComponentInParent<Canvas>();
        if (_canvas == null)
        {
            Debug.LogError("Canvas component not found in parent!");
        }
    }

    void Update()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
        if (_players.Length == 0)
        {
            return;
        }

        var playerCamerasObjs = GameObject.FindGameObjectsWithTag("PlayerCamera");
        if (playerCamerasObjs.Length == 0)
        {

            return;
        }

        _playerCameras = new Camera[playerCamerasObjs.Length];
        for (int i = 0; i < playerCamerasObjs.Length; i++)
        {
            var camera = playerCamerasObjs[i].GetComponent<Camera>();
            if (camera == null)
            {
                Debug.LogError($"Camera component not found on PlayerCamera object {i}!");
                return;
            }
            _playerCameras[i] = camera;
        }

        if (_players == null || _playerCameras == null || _pointerRectTransform == null || _canvas == null)
        {
            Debug.LogError("A necessary component is null. Aborting Update.");
            return;
        }

        _closestFood = FindClosestFoodWithTag("Interactable");
        if (_closestFood == null)
        {
            GetComponent<UnityEngine.UI.Image>().enabled = false;
            return;
        }

        foreach (GameObject player in _players)
        {
            var spawnAndAssign = player.GetComponent<SpawnAndAssign>();
            if (spawnAndAssign == null)
            {
                Debug.LogError("SpawnAndAssign component not found on player!");
                continue;
            }

            if (spawnAndAssign.PlayerID != pointerID)
            {
                continue;
            }

            foreach (Camera camera in _playerCameras)
            {
                var connectCameraToPlayer = camera.GetComponent<ConnectCameraToPlayer>();
                if (connectCameraToPlayer == null)
                {
                    Debug.LogError("ConnectCameraToPlayer component not found on camera!");
                    continue;
                }

                if (connectCameraToPlayer.CamerId == spawnAndAssign.PlayerID)
                {
                    _currentPosition = player.transform.position;
                    Vector3 toPosition = _closestFood.transform.position;
                    float distanceToFood = Vector3.Distance(_currentPosition, toPosition);
                    Debug.Log(distanceToFood);
                    Vector3 direction = (toPosition - _currentPosition).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
                    Vector3 targetPositionScreenPoint = camera.WorldToScreenPoint(toPosition);
                    bool isOffScreen = targetPositionScreenPoint.z < 0 ||
                                       targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width ||
                                       targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;

                    if (distanceToFood <= distanceToFoodSprite)
                    {
                        // Show button sprite when close to food
                        GetComponent<UnityEngine.UI.Image>().enabled = true;
                        GetComponent<UnityEngine.UI.Image>().sprite = ButtonSprite;

                        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                            _canvas.transform as RectTransform,
                            targetPositionScreenPoint,
                            _canvas.worldCamera,
                            out Vector2 localPoint);

                        _pointerRectTransform.localPosition = localPoint + new Vector2(0f, 70f);
                        _pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
                    }
                    else if (isOffScreen)
                    {
                        // Show pointer sprite when food is off-screen
                        GetComponent<UnityEngine.UI.Image>().enabled = true;
                        GetComponent<UnityEngine.UI.Image>().sprite = PointerSprite;

                        Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

                        switch (spawnAndAssign.PlayerID)
                        {
                            case 1:
                                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, _borderSize, Screen.width / 2 - _borderSize);
                                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, Screen.height / 2 + _borderSize, Screen.height - _borderSize);
                                break;
                            case 2:
                                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, Screen.width / 2 + _borderSize, Screen.width - _borderSize);
                                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, Screen.height / 2 + _borderSize, Screen.height - _borderSize);
                                break;
                            case 3:
                                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, _borderSize, Screen.width / 2 - _borderSize);
                                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, _borderSize, Screen.height / 2 - _borderSize);
                                break;
                            case 4:
                                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, Screen.width / 2 + _borderSize, Screen.width - _borderSize);
                                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, _borderSize, Screen.height / 2 - _borderSize);
                                break;
                        }

                        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                            _canvas.transform as RectTransform,
                            cappedTargetScreenPosition,
                            _canvas.worldCamera,
                            out Vector2 localPoint);
                        _pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, angle);
                        _pointerRectTransform.localPosition = localPoint;
                        _pointerRectTransform.localPosition = new Vector3(_pointerRectTransform.localPosition.x, _pointerRectTransform.localPosition.y, 0f);
                    }
                    else
                    {
                        // Hide pointer when food is on-screen and not close enough
                        GetComponent<UnityEngine.UI.Image>().enabled = false;
                    }

                    if (player.GetComponent<BasicAttack>().IsHoldingFood)
                    {
                        GetComponent<UnityEngine.UI.Image>().enabled = false;
                    }
                }
            }
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