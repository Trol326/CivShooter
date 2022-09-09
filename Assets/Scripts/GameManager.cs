using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
static public class GameManager
{
    static private int _maxPlayerHP = 5, _playerHP = _maxPlayerHP;
    static public int GetPlayerHP() => _playerHP;
    //________________________________________________________
    static private GameObject _player;
    static public GameObject GetPlayerRef() => _player;
    static public void SetPlayer(GameObject player) => _player = player;
    static private GameObject _character;
    static public GameObject GetCharacter() => _character?_character:null;
    static public void SetCharacter(GameObject character) => _character = character;

    //________________________________________________________
    static private int _score = 0;
    static public int GetScore() => _score;
    static private void ChangeScore(int number = 1) => _score+=number;
    //________________________________________________________


    // Weapon dict. WeaponID / status
    static public Dictionary<string, bool> weaponList = new Dictionary<string, bool>();


    // Player methods
    static public void DealDamage(int damage = 1)
    {
        Debug.Log("MaxHP = "+_maxPlayerHP+". HP = "+_playerHP + ". Damage = "+damage);
        _playerHP -= damage;
        if(_playerHP <= 0)Death();
        
    }
    static private void Death()
    {
        Debug.Log("<color=red>YOU DEAD</color>");
    }
    static private void ChangeWeapon()
    {
        
    }

    static public void BoxPicked()
    {
        ChangeScore();
        UIMasterSc.Instance.ScoreChanged();
        SpawnManagerSc.Instance.SpawnNextBox();
    }
}

