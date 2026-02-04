using UnityEngine;

public class SwordTrail : MonoBehaviour
{
    TrailRenderer _trailRenderer;
    SpriteRenderer _spriteRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_spriteRenderer.sortingOrder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleSwordTrail()
    {
        _trailRenderer.emitting = !_trailRenderer.emitting;
    }
}
