using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyClass : MonoBehaviour
{
    [Header("Enemy stat")]
    [SerializeField]protected float movementSpeed = 5;
    [Min(1)]
    [SerializeField]protected int enemyHP = 5;
    [SerializeField]protected float damageStunTime = 2;
    [Header("Attack stat")]
    [Min(1)]
    [SerializeField]protected int enemyDamage = 1;
    [Min(1)]
    [SerializeField]protected float attackRange = 5;
    public float GetAttackRange() => attackRange;
    //__________________________________________________________
    protected bool isCanMove = true;
    protected bool isCanAttack = true;
    public bool GetCDStatus() => isCanAttack;
    public void ChangeCDStatus() => isCanAttack = !isCanAttack;
    //__________________________________________________________
    protected NavMeshAgent navMeshAgent;
    protected GameObject player;
    protected Vector3 movementTarget;
    //__________________________________________________________
    
    protected virtual void Awake()
    {
        player = GameManager.GetPlayerRef();
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
        isCanAttack = isCanMove = true;
    }
    protected virtual void FixedUpdate()
    {
        if(!isCanMove && !isCanAttack)return;
        if(!player)player = GameManager.GetPlayerRef();
        if(CheckDistanceToPlayer())
        {
            if(isCanAttack)Attack();
            movementTarget = player.transform.position + Random.insideUnitSphere*attackRange;
        }
        else
        {
            movementTarget = player.transform.position+Vector3.one*2;
        }
        if(isCanMove)Move();
    }
    //__________________________________________________________
    protected virtual void Attack() => Debug.Log("No realisation", this);
    protected virtual void Move() => navMeshAgent.SetDestination(movementTarget);
    public void DealDamage(int damage = 1)
    {
        enemyHP -= damage;
        StartCoroutine(Stun(damageStunTime));
        if(enemyHP<=0)Death();
    }
    protected virtual void Death()
    {
        Debug.Log("Enemy dead");
        Destroy(this.gameObject);
    }
    IEnumerator Stun(float stunTime)
    {
        isCanMove = isCanAttack = false;
        yield return new WaitForSeconds(stunTime);
        isCanMove = isCanAttack = true;
    }
    //__________________________________________________________
    
    protected bool CheckDistanceToPlayer() => (Vector3.Distance(this.transform.position, player.transform.position) <= attackRange)?true:false;
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(player.tag))
        {
            GameManager.DealDamage(enemyDamage);
        }
    }
    protected IEnumerator StartCooldown(float time)
    {
        ChangeCDStatus();
        yield return new WaitForSeconds(time);
        ChangeCDStatus();
    }
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #endif
}
