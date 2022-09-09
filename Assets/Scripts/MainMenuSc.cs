using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainMenuSc : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]GameObject UIWeaponMenu;
    [SerializeField]GameObject UICharacterMenu;
    //_____________________________________________________________
    [Header("Cameras")]
    [SerializeField] CinemachineBrain _cameraBrain;
    [SerializeField] CinemachineVirtualCamera _weaponCam;
    [SerializeField] CinemachineVirtualCamera _characterCam;
    CinemachineVirtualCamera _currentCam;
    //_____________________________________________________________
    void Awake()
    {
        if(!_cameraBrain)return;
        _currentCam = (CinemachineVirtualCamera)_cameraBrain.ActiveVirtualCamera;
    }
    public void ToWeaponButton()
    {
        if(UICharacterMenu)UICharacterMenu.SetActive(false);
        ChangneCameraTo(_weaponCam);
        StartCoroutine(WaitForSec(1));
        if(UIWeaponMenu)UIWeaponMenu.SetActive(true);
    }
    public void ToCharactersButton()
    {
        if(UIWeaponMenu)UIWeaponMenu.SetActive(false);
        ChangneCameraTo(_characterCam);
        StartCoroutine(WaitForSec(1));
        if(UICharacterMenu)UICharacterMenu.SetActive(true);
    }
    void ChangneCameraTo(CinemachineVirtualCamera targetCam)
    {
        if(_currentCam == targetCam)return;
        if(_currentCam)_currentCam.m_Priority = 0;
        targetCam.m_Priority = 10;
        _currentCam = targetCam;
    }

    IEnumerator WaitForSec(int sec)
    {
        yield return new WaitForSeconds(sec);
    }

}
