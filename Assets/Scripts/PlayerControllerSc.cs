using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;
public class PlayerControllerSc : MonoBehaviour
{
    //_______________________________________________________
    [Header("Input")]
    [SerializeField]float smoothInputSpeed = 0.35f;
    Vector2 input; //Previous/current input V2
    Vector2 smoothInputVelocity = Vector2.zero; // need for smoothdamp
    
    [Header("Movement")]
    [SerializeField]float movementSpeed = 6;
    Vector3 movement; //Current movement V3
    
    [Header("Weapon")]
    [SerializeField]WeaponClass currentWeapon;

    [Header("Character")]
    [SerializeField]GameObject currentCharacterPrefub;
    GameObject currentCharacterRef;
    CharControllerSc currentCharacterSc;
    //_______________________________________________________
    CharacterController _charControl;
    PlayerInputActions _playerInput;
    public PlayerInputActions GetPlayerInput() => _playerInput;
    //_______________________________________________________
    void Awake()
    {
        _charControl = GetComponent<CharacterController>();
        GameManager.SetPlayer(this.gameObject);
        if(!currentWeapon.GetCDStatus())currentWeapon.ChangeCDStatus();
        _playerInput = new PlayerInputActions();
        if(!currentCharacterPrefub)currentCharacterPrefub = GameManager.GetCharacter();
        SpawnCurrentChar();
    }
    void Update()
    {
        SaveMovement();
        PlayerMoving();
        LookAtMouse();
        if(_playerInput.Player.Shoot.IsPressed() && currentWeapon.GetCDStatus())
        {
            if(currentCharacterSc)currentCharacterSc.StartAttackAnimation();
            currentWeapon.Shoot(this.gameObject);
            StartCoroutine(currentWeapon.StartCooldown());
        }
        if(_playerInput.Player.Ability1.IsPressed() && currentCharacterSc)
        {
            currentCharacterSc.UseAbility1();
        }
    }
    //_______________________________________________________

    void OnEnable()
    {
        _playerInput.Player.Enable();
    }
    void OnDisable()
    {
        _playerInput.Player.Disable();
    }
    
    //_______________________________________________________
    void PlayerMoving() 
    {
        _charControl.SimpleMove(movementSpeed * movement);
    }
    void LookAtMouse()
    {
        transform.LookAt(PlayerCameraSc.GetMousePos());
    }
    void SaveMovement()
    {
        input = Vector2.SmoothDamp(input,_playerInput.Player.Move.ReadValue<Vector2>(), ref smoothInputVelocity, smoothInputSpeed);
        movement = new Vector3(input.x, 0, input.y);
    }
    //_______________________________________________________

    void SpawnCurrentChar()
    {
        if(!currentCharacterPrefub)return;
        Vector3 position = transform.position;
        position.y -= 1;
        currentCharacterRef = Object.Instantiate(currentCharacterPrefub, position, transform.rotation, transform);
        currentCharacterSc = currentCharacterRef.GetComponent<CharControllerSc>();
        if(currentWeapon)currentCharacterSc.SetCurrentWeapon(currentWeapon);
    }
}
