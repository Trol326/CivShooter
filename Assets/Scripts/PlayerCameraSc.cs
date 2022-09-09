using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCameraSc : MonoBehaviour
{
    //___________________________________________________________
    [Header("Mouse")]
    public LayerMask groundLayersWL;
    [SerializeField]float mouseRadius = 10.0f;
    [SerializeField]GameObject mouseObject;
    //___________________________________________________________
    Camera _cam;
    public Camera GetCamera() => _cam;
    public void SetCamera(Camera camera) {_cam = camera;}
    [HideInInspector]
    [SerializeField]GameObject _player;
    public void SetPlayerObj(GameObject player){_player = player;}
    public GameObject GetPlayerObj() => _player;
    static Vector3 _mousePos;
    static public Vector3 GetMousePos() => _mousePos;

    PlayerInput.PlayerInputActions _playerInputActions;
    //___________________________________________________________
    void Awake()
    {
        try
        {
            if(!GetCamera())
            {
                SetCamera(Camera.main);
            }
            if(!GetPlayerObj())
            {
                SetPlayerObj(GameObject.FindWithTag("Player"));
            }
        }
        catch
        {
            Debug.Log("<color=red>Failed update variables</color>", this);
        }
    }
    void Update()
    {
        SaveMousePosition();
    }
    //___________________________________________________________
    void SaveMousePosition()
    {
        if(_playerInputActions == null)
        {
            _playerInputActions = _player.GetComponent<PlayerControllerSc>().GetPlayerInput();
        }
        var input = _playerInputActions.Player.Aim.ReadValue<Vector2>();
        Vector3 lookingPoint = new Vector3();
        if (Physics.Raycast(_cam.ScreenPointToRay(input), out var hitInfo, groundLayersWL))
        {
            lookingPoint = hitInfo.point;
            lookingPoint.y = _player.transform.position.y;
        }
        Vector3 diff = lookingPoint - _player.transform.position;
        float magn = diff.magnitude;
        if (magn > mouseRadius)
        {
            diff = diff * (mouseRadius / magn);
        }
        _mousePos = _player.transform.position + diff;

        MoveInvisibleMouse();
    }
    void MoveInvisibleMouse()
    {
        if (mouseObject)
        {
            mouseObject.transform.position = GetMousePos();
            return;
        }
        if (!GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.Follow.TryGetComponent<Cinemachine.CinemachineTargetGroup>(out var targetGroup))
        {
            Debug.Log("<color=red>Error, no target group</color>");
            return;
        }
        if(targetGroup.m_Targets.Length > 1)
        {
            mouseObject = targetGroup.m_Targets[1].target.gameObject;
            return;
        }
        mouseObject = new GameObject("mouseObject");
        targetGroup.AddMember(mouseObject.transform, 1, 1);
    }
    //___________________________________________________________



}