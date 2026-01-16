using UnityEngine;

public class PlayerAttackState : State
{
    private PlayerStateMachine playerContext;
    public PlayerAttackState(PlayerStateMachine currentContext) : base(currentContext)
    {
        playerContext = currentContext;
        isBaseState = true;
        InitializeSubStates();
    }
    public override void EnterState()
    {
        playerContext.CanMove = false;
        playerContext.AppliedMovementX = 0f;
        
        playerContext.AttackFinished = false; 
        playerContext.ComboDone = false;
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        playerContext.Sword.SetActive(false);
        playerContext.CanMove = true;
    }

    public override void InitializeSubStates()
    {
        if (playerContext.NumHits == 1)
        {
            SetSubState(new PlayerMeleeOneState(playerContext));
        } else if (playerContext.NumHits == 2)
        {
            SetSubState(new PlayerMeleeTwoState(playerContext));
        } else if (playerContext.NumHits == 3)
        {
            SetSubState(new PlayerMeleeThreeState(playerContext));
        } else
        {
            playerContext.AttackFinished = true;
        }
    }

    public override void CheckSwitchStates()
    {
        if (playerContext.IsHurt)
        {
            SwitchState(new PlayerHurtState(playerContext));
        }
        // else if (!playerContext.AttackFinished)
        // {
        //     return;
        // }
        if (playerContext.AttackFinished && playerContext.IsMovementPressed && playerContext.IsRunPressed)
        {
            playerContext.ComboDone = true;
            SwitchState(new PlayerRunState(playerContext));
        } else if (playerContext.AttackFinished && playerContext.IsMovementPressed)
        {   playerContext.ComboDone = true;
            SwitchState(new PlayerWalkState(playerContext));
        }
        if (playerContext.ComboDone)
        {
            if (playerContext.IsMovementPressed && playerContext.IsRunPressed)
            {
                SwitchState(new PlayerRunState(playerContext));
            } else if (playerContext.IsMovementPressed) {
                SwitchState(new PlayerWalkState(playerContext));
            }
            else
            {
                SwitchState(new PlayerIdleState(playerContext));
            }
            
        }

        
    }
}
