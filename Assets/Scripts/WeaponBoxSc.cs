using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBoxSc : MonoBehaviour
{
   void OnTriggerEnter(Collider other)
   {
        if(other.gameObject.CompareTag("Player"))
        {
            GameManager.BoxPicked();
            Destroy(this.gameObject);
        }
   }
}
