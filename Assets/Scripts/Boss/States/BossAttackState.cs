using UnityEngine;

public class BossAttackState : State
{
    private BossStateMachine bossContext;
    public BossAttackState(BossStateMachine currentContext) : base(currentContext)
    {
        
        bossContext = currentContext;
    }
    public override void EnterState()
    {
        bossContext.AttackFinished = 0;
        bossContext.Anim.Play("Attack");
        bossContext.AppliedMovementX = 0f;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        bossContext.AttackFinished = 0;
    }

    public override void CheckSwitchStates()
    {
        if (bossContext.IsStunned)
        {   Debug.Log("switching states");
            SwitchState(new BossStunState(bossContext));
        } else if (bossContext.AttackFinished == 1)
        {
            SwitchState(new BossWalkState(bossContext));
        }
        
    }
}
