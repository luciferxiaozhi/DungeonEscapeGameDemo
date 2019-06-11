using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;

    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit;
    protected bool isDead;
    protected Player player;

    private bool _canDamage = true;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isDead)
            return;

        if (other.tag == "Player")
        {
            if (_canDamage)
            {
                _canDamage = false;
                player.Damage();
                StartCoroutine(DamageCoolDownRoutine());
            }
        }
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && !anim.GetBool("InCombat")) // idle
        {
            return;
        }

        if(!isDead)
            Movement();
    }

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        currentTarget = pointB.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void Movement()
    {
        Flip(currentTarget != pointA.position); // never reach this code while idling

        if (transform.position == pointA.position)
        {
            anim.SetTrigger("Idle");
            currentTarget = pointB.position;
        }
        else if (transform.position == pointB.position)
        {
            anim.SetTrigger("Idle");
            currentTarget = pointA.position;
        }


        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > 3.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        Vector3 direction = player.transform.localPosition - transform.localPosition;
        if (direction.x > 0 && anim.GetBool("InCombat"))
        {
            Flip(true);
        }
        else if (direction.x < 0 && anim.GetBool("InCombat"))
        {
            Flip(false);
        }
    }


    private void Flip(bool moveRight)
    {
        sprite.flipX = !moveRight;
    }

    IEnumerator DamageCoolDownRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        _canDamage = true;
    }
}
