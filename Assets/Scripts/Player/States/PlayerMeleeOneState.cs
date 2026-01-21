using UnityEngine;

public class PlayerMeleeOneState : State
{
    private PlayerStateMachine playerContext;
    public PlayerMeleeOneState(PlayerStateMachine currentContext) : base(currentContext)
    {
        playerContext = currentContext;
    }
    public override void EnterState()
    {
        playerContext.AttackFinished = false;
        playerContext.IsHitPressed = false;
        playerContext.Anim.Play("Slash");
        Vector3 direction = new Vector3(Mathf.Sign(playerContext.Sprite.localScale.x), 0f, 0f);
        playerContext.RB.AddForce(direction * playerContext.SlashForce, ForceMode2D.Impulse);
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
    }
}
