using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainMenuSc : MonoBehaviour
{
    [SerializeField] CinemachineBrain _cameraBrain;
    [SerializeField] CinemachineVirtualCamera _weaponCam;
    
    CinemachineVirtualCamera _currentCam;

    void Awake()
    {
        if(!_cameraBrain)return;
        _currentCam = (CinemachineVirtualCamera)_cameraBrain.ActiveVirtualCamera;
    }
    public void ToWeaponButton()
    {

    }
    void ChangneCameraTo(CinemachineVirtualCamera targetCam)
    {

    }
}
