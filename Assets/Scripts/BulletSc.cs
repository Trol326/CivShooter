using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSc : MonoBehaviour
{
    private float flyingLength;
    private int bulletDamage = 1;
    private bool isFlying = false;
    private Vector3 startPosition;
    private bool bulletMode; // true - player's bullet; false - enemy's bullet
    void Update()
    {
        if(!isFlying)return;
        if(Vector3.Distance(startPosition, transform.position) > flyingLength)
        {
            Destroy(this.gameObject);
        }
    }
    public void ShootBullet(float fireRange, Vector3 firePointPosition, float bulletSpeed=25, int damage = 1, bool mode = true)
    {
        flyingLength = fireRange;
        startPosition = firePointPosition;
        isFlying = true;
        bulletDamage = damage;
        bulletMode = mode;
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward*bulletSpeed, ForceMode.Impulse);
        float lifeTime = flyingLength/bulletSpeed;
        Destroy(this.gameObject, lifeTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(bulletMode)collision.gameObject.GetComponent<EnemyClass>().DealDamage(bulletDamage);
            Destroy(this.gameObject);
        }
        if(collision.gameObject.CompareTag("Player") && !bulletMode)
        {
            GameManager.DealDamage(bulletDamage);
            Destroy(this.gameObject);
        }
    }
}
