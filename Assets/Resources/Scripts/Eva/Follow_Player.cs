using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float stoppingDist = 2f;
    private Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target == null) return;
        float currentDist = Vector2.Distance(transform.position, target.position);
        if (currentDist > stoppingDist)
        {
            transform.position = Vector2.MoveTowards(
                transform.position, 
                target.position, 
                moveSpeed * Time.fixedDeltaTime
            );
        }
    }
}
