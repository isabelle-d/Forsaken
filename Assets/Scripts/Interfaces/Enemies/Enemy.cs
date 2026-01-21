using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{   
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected float targetDistance;
    [SerializeField] protected float cooldown;
    protected float canTakeDamage = 0f;
    
    protected Animator animator;
    protected Transform sprite;
    protected Rigidbody2D rb;
    protected bool isFlipped;
    protected Transform player;

    public float Speed {get {return speed;} set {speed = value;}}
    public float TargetDistance {get {return targetDistance;} set {targetDistance = value;}}
    public int Health {get {return health;} set {health = value;}}
    public float Cooldown {get {return cooldown;} set {cooldown = value;}}
    public void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = transform.Find("Sprite");
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Update()
    {
    }

    //by default, this moves to some position close to the player. Override for more complex behavior
    public virtual void Move()
    {
        FacePlayer();
        Vector2 target = new Vector2(player.position.x, animator.gameObject.transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, Speed * Time.fixedDeltaTime);
        if (Vector2.Distance(newPos, target) <= TargetDistance)
        {
            Attack();
        } else {
            rb.MovePosition(newPos);
        }
    }

    //override this method for each implemented Enemy to include more complex attack logic
    public virtual void Attack()
    {
        animator.SetTrigger("Attack");
    }

    //DO NOT CHANGE
    //this flips the sprites to face the player
    public void FacePlayer()
    {
        Vector3 flipped = sprite.localScale;
        flipped.x *= -1f;
        if (sprite.position.x < player.position.x && isFlipped)
        {
            sprite.localScale = flipped;
            isFlipped = false;
        } else if (sprite.position.x > player.position.x && !isFlipped)
        {
            sprite.localScale = flipped;
            isFlipped = true;
        }
    }

    //DO NOT CHANGE
    //update the current health
    public virtual void ApplyDamage(int damage)
    {
        if (Time.time > canTakeDamage)
        {
            canTakeDamage = Time.time + cooldown;
            Health -= damage;
            Debug.Log("Enemy Health: " + Health);
            animator.SetTrigger("Hurt");

        }
        
        if (Health <= 0)
        {
            Debug.Log("Enemy killed!");
        }
    }


}
