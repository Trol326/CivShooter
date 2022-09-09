using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoomer : EnemyClass
{
    [Min(1)]
    [SerializeField]protected float timeBeforeExplotion = 2f;
    [Min(4)]
    [SerializeField]protected int circleSegments = 6;
    protected LineRenderer lineDrawer;
    protected override void Awake()
    {
        base.Awake();
        lineDrawer = GetComponent<LineRenderer>();
    }
    protected override void Attack()
    {
        Debug.Log("Boom...?");
        StartCoroutine(Explosion());
    }
    public void ChainAttack() => Attack();
    IEnumerator Explosion()
    {
        isCanAttack = false;
        isCanMove = false;
        movementTarget = player.transform.position;
        navMeshAgent.speed = 2f;
        DrawCircleAround();
        yield return new WaitForSeconds(timeBeforeExplotion);
        navMeshAgent.enabled = false;
        var colls = Physics.OverlapSphere(this.transform.position, attackRange);
        foreach(var col in colls)
        {
            if(col.CompareTag("Player"))GameManager.DealDamage(enemyDamage);
            if(col.CompareTag("Enemy"))
            {
                if(col.TryGetComponent<EnemyBoomer>(out var en))
                {
                    en.DealDamage(enemyDamage);
                    en.ChainAttack();
                }
                else
                {
                    var enemy = col.GetComponent<EnemyClass>();
                    enemy.DealDamage(enemyDamage);
                }
            }
        }
        Debug.Log("BOOOOOOM");
        Death();
    }
    void DrawCircleAround()
    {
        lineDrawer.positionCount = circleSegments+1;
        Vector3[] points = new Vector3[circleSegments+1];;
        for(int i = 0; i < circleSegments+1; i++)
        {
            var rad = Mathf.Deg2Rad * (i*360f/circleSegments);
            points[i] = new Vector3(Mathf.Sin(rad)*attackRange, 1, Mathf.Cos(rad)*attackRange);
        }
        lineDrawer.SetPositions(points);
    }
}
