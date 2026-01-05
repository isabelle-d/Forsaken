using UnityEngine;
using System.Collections;

public class BossGrappleDebugger : MonoBehaviour
{
    public Transform player;
    public LineRenderer lineRenderer;
    
    public float grappleSpeed = 20f;
    public float stopDistance = 1.5f;

    private bool isGrappling = false;

    void Start()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) 
        {
            isGrappling = true;
            if(lineRenderer != null) lineRenderer.enabled = true;
            Debug.Log("DEBUG: Grapple Started");
            StartCoroutine(AnimateGrapple());
        }

        // if (isGrappling)
        // {
        //     AnimateGrapple();
        // }
    }

private IEnumerator AnimateGrapple()
{
    lineRenderer.enabled = true;

    float elapsed = 0f;
    float duration = 0.5f; // How long it takes the chain to "reach" the player

    Vector3 bossCenter;
    Vector3 playerCenter;

    // PHASE 1: Throw the chain out
    while (elapsed < duration)
    {
        elapsed += Time.deltaTime;
        float percent = elapsed / duration;

        bossCenter = GetComponent<Collider2D>().bounds.center;
        playerCenter = player.GetComponent<Collider2D>().bounds.center;

        // The "Tip" of the chain moves from Boss to Player
        Vector3 chainTip = Vector3.Lerp(bossCenter, playerCenter, percent);

        lineRenderer.SetPosition(0, bossCenter);
        lineRenderer.SetPosition(1, chainTip);

        yield return null; // Wait for the next frame
    }

    // PHASE 2: Once the chain hits, pull the boss!
    // (This is where your existing MoveTowards logic goes)
    while (Vector3.Distance(transform.position, player.position) > stopDistance)
    {
        // Update both ends of the line while moving
        lineRenderer.SetPosition(0, GetComponent<Collider2D>().bounds.center);
        lineRenderer.SetPosition(1, player.GetComponent<Collider2D>().bounds.center);

        transform.position = Vector3.MoveTowards(transform.position, player.position, grappleSpeed * Time.deltaTime);
        yield return null;
    }

    lineRenderer.enabled = false;
    isGrappling = false;
}

   void PerformGrapple()
    {
        if (player == null) return;

        // calc midpts of colliders
        Vector3 bossCenter = GetComponent<Collider2D>().bounds.center;
        Vector3 playerCenter = player.GetComponent<Collider2D>().bounds.center;

        transform.position = Vector3.MoveTowards(transform.position, playerCenter, grappleSpeed * Time.deltaTime);

        if (lineRenderer != null)
        {
            lineRenderer.SetPosition(0, bossCenter); 
            lineRenderer.SetPosition(1, playerCenter);
        }

        if (Vector3.Distance(bossCenter, playerCenter) < stopDistance)
        {
            isGrappling = false;
            if(lineRenderer != null) lineRenderer.enabled = false;
        }
    }
}