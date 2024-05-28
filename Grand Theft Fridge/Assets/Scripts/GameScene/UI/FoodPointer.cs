using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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
            Debug.LogWarning("No players found!");
            return;
        }

        _PlayerNumbers = new int[_players.Length];
        for (int i = 0; i < _players.Length; i++)
        {
            var spawnAndAssign = _players[i].GetComponent<SpawnAndAssign>();
            if (spawnAndAssign == null)
            {
                Debug.LogError($"SpawnAndAssign component not found on player {i}!");
                return;
            }
            _PlayerNumbers[i] = spawnAndAssign.PlayerID;
        }

        var playerCamerasObjs = GameObject.FindGameObjectsWithTag("PlayerCamera");
        if (playerCamerasObjs.Length == 0)
        {
            Debug.LogWarning("No player cameras found!");
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

            if (spawnAndAssign.PlayerID != pointerID) // Check if the player ID matches the pointer ID
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
                    Vector3 direction = (toPosition - _currentPosition).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;  // Adjust angle to match the UI

                    _pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, angle);

                    Vector3 targetPositionScreenPoint = camera.WorldToScreenPoint(toPosition);
                    bool isOffScreen = false;
                    Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;

                    // Determine the off-screen and capped target positions based on player ID and split-screen region
                    switch (spawnAndAssign.PlayerID)
                    {
                        case 1:
                            isOffScreen = targetPositionScreenPoint.z < 0 ||
                                          targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width / 2 ||
                                          targetPositionScreenPoint.y <= Screen.height / 2 || targetPositionScreenPoint.y >= Screen.height;
                            if (isOffScreen)
                            {
                                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, _borderSize, Screen.width / 2 - _borderSize);
                                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, Screen.height / 2 + _borderSize, Screen.height - _borderSize);
                            }
                            break;
                        case 2:
                            isOffScreen = targetPositionScreenPoint.z < 0 ||
                                          targetPositionScreenPoint.x <= Screen.width / 2 || targetPositionScreenPoint.x >= Screen.width ||
                                          targetPositionScreenPoint.y <= Screen.height / 2 || targetPositionScreenPoint.y >= Screen.height;
                            if (isOffScreen)
                            {
                                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, Screen.width / 2 + _borderSize, Screen.width - _borderSize);
                                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, Screen.height / 2 + _borderSize, Screen.height - _borderSize);
                            }
                            break;
                        case 3:
                            isOffScreen = targetPositionScreenPoint.z < 0 ||
                                          targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width / 2 ||
                                          targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height / 2;
                            if (isOffScreen)
                            {
                                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, _borderSize, Screen.width / 2 - _borderSize);
                                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, _borderSize, Screen.height / 2 - _borderSize);
                            }
                            break;
                        case 4:
                            isOffScreen = targetPositionScreenPoint.z < 0 ||
                                          targetPositionScreenPoint.x <= Screen.width / 2 || targetPositionScreenPoint.x >= Screen.width ||
                                          targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height / 2;
                            if (isOffScreen)
                            {
                                cappedTargetScreenPosition.x = Mathf.Clamp(cappedTargetScreenPosition.x, Screen.width / 2 + _borderSize, Screen.width - _borderSize);
                                cappedTargetScreenPosition.y = Mathf.Clamp(cappedTargetScreenPosition.y, _borderSize, Screen.height / 2 - _borderSize);
                            }
                            break;
                        default:
                            isOffScreen = true;
                            break;
                    }

                    if (isOffScreen)
                    {

                        GetComponent<UnityEngine.UI.Image>().enabled = true;
                        GetComponent<UnityEngine.UI.Image>().sprite = PointerSprite;

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
                        GetComponent<UnityEngine.UI.Image>().sprite = ButtonSprite;



                        // Convert the screen position of the food to the canvas position
                        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                            _canvas.transform as RectTransform,
                            targetPositionScreenPoint,
                            _canvas.worldCamera,
                            out Vector2 localPoint);

                        _pointerRectTransform.localPosition = localPoint + new Vector2(0f, 70f);

                        // Set the pointer rotation to zero when the food is on-screen
                        _pointerRectTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
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