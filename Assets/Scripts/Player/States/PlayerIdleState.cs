using UnityEngine;

public class PlayerIdleState : State
{
    private PlayerStateMachine playerContext;
    public PlayerIdleState(PlayerStateMachine currentContext) : base(currentContext)
    {
        playerContext = currentContext;
        isBaseState = true;
    }
    public override void EnterState()
    {
        playerContext.CanMove = true;
        playerContext.Anim.Play("Idle");
        playerContext.AppliedMovementX = 0f;
        playerContext.AppliedMovementY = 0f;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
    }

    public override void CheckSwitchStates()
    {
        if (playerContext.IsHurt)
        {
            SwitchState(new PlayerHurtState(playerContext));
        }
        else if (playerContext.IsHitPressed)
        {
            SwitchState(new PlayerAttackState(playerContext));
        }
        else if (playerContext.IsShootPressed && playerContext.IsAimingForward)
        {
            SwitchState(new PlayerShootState(playerContext));
        } else if (playerContext.Grounded && playerContext.IsJumpPressed)
        {
            SwitchState(new PlayerJumpState(playerContext));
        } else if (playerContext.IsDashPressed && playerContext.CanDash)
        {
            SwitchState(new PlayerDashState(playerContext));
        } else if (playerContext.IsMovementPressed && playerContext.IsRunPressed)
        {
            SwitchState(new PlayerRunState(playerContext));
        } else if (playerContext.IsMovementPressed)
        {   
            SwitchState(new PlayerWalkState(playerContext));
        } 
    }
}
