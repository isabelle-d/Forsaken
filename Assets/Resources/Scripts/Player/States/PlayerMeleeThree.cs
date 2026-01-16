using UnityEngine;

public class PlayerMeleeThreeState : State
{
    private PlayerStateMachine playerContext;
    public PlayerMeleeThreeState(PlayerStateMachine currentContext) : base(currentContext)
    {
        playerContext = currentContext;
    }
    public override void EnterState()
    {
        playerContext.AttackFinished = false;
        playerContext.IsHitPressed = false;
        playerContext.Anim.Play("Slash 3");
        Vector3 direction = new Vector3(Mathf.Sign(playerContext.Sprite.localScale.x), 0f, 0f);
        playerContext.RB.AddForce(direction * playerContext.SlashForce, ForceMode2D.Impulse);
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState()
    {
        playerContext.NumHits = 0;
    }

    public override void CheckSwitchStates()
    {
        if (playerContext.AttackFinished)
        {
            playerContext.ComboDone = true;
        }
    }
}
