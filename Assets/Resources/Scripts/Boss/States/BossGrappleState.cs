using UnityEngine;
using System.Collections;

public class BossGrappleState : State
{
    private BossStateMachine bossContext;
    private LineRenderer lineRenderer;

    public BossGrappleState(BossStateMachine currentContext) : base(currentContext)
    {
        bossContext = currentContext;
    }

    public override void EnterState()
    {
        Debug.Log("BOSS: Entering Grapple State");
        lineRenderer = bossContext.GetComponentInChildren<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not found on boss GameObject");
            SwitchState(new BossIdleState(bossContext));
            return;
        }

        lineRenderer.enabled = true;
        bossContext.StartCoroutine(AnimateGrapple());
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
        if (bossContext.IsHurt)
        {
            SwitchState(new BossHurtState(bossContext));
        }
        else if (bossContext.GrapplingFinished == 0)
        {
            SwitchState(new BossAttackState(bossContext));
        }
    }

    private IEnumerator AnimateGrapple()
    {
        float elapsed = 0f;
        float duration = bossContext.GrappleDuration;
        float stopDistance = 2f;
        Vector3 bossCenter;
        Vector3 playerCenter;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percent = elapsed / duration;

            bossCenter = bossContext.GetComponent<Collider2D>().bounds.center;
            playerCenter = bossContext.Player.GetComponent<Collider2D>().bounds.center;

            Vector3 chainTip = Vector3.Lerp(bossCenter, playerCenter, percent);

            lineRenderer.SetPosition(0, bossCenter);
            lineRenderer.SetPosition(1, chainTip);

            yield return null;
        }

        while (Vector3.Distance(bossContext.transform.position, bossContext.Player.transform.position) > stopDistance)
        {
            lineRenderer.SetPosition(0, bossContext.GetComponent<Collider2D>().bounds.center);
            lineRenderer.SetPosition(1, bossContext.Player.GetComponent<Collider2D>().bounds.center);
            bossContext.transform.position = Vector3.MoveTowards(bossContext.transform.position, bossContext.Player.transform.position, bossContext.GrappleSpeed * Time.deltaTime);
            yield return null;
        }

        lineRenderer.enabled = false;
        bossContext.GrapplingFinished = 0;
    }

}