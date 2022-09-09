using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleAnimationBehaviorSc : StateMachineBehaviour
{
    [SerializeField]int idleAnimationsAmount;
    int nextAnimationID = 0;
    [SerializeField]float timeBeforeAlt = 10f;
    float timer;
    bool _isAlt = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetAnimation();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!_isAlt)
        {
            timer+= Time.deltaTime;
            if(timer > timeBeforeAlt && stateInfo.normalizedTime % 1 < 0.02f)
            {
                nextAnimationID = RandomiseNumber();
                _isAlt = true;
            }
        }
        else if(stateInfo.normalizedTime % 1 > 0.98f)
        {
            ResetAnimation();
        }
        animator.SetFloat("idleAnimation", nextAnimationID);
    }

    int RandomiseNumber()
    {
        var result = (int)(Random.value*(idleAnimationsAmount-1)+1);
        return (result>(idleAnimationsAmount-1))?result-1:result;
    }
    void ResetAnimation()
    {
        nextAnimationID = 0;
        _isAlt = false;
        timer = 0;
    }
}
