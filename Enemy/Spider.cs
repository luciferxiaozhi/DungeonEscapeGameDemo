using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    [SerializeField] private GameObject _acidGameObject;
    private SpriteRenderer _Sprite;
    private Vector3 _direction;

    // Use for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
        _Sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        if (isDead)
            return;

        FaceToPlayer();
    }

    public void FaceToPlayer()
    {
        _direction = GameManager.Instance.Player.transform.localPosition - transform.localPosition;
        if (_direction.x > 0)
        {
            _Sprite.flipX = false;
        }
        else
        {
            _Sprite.flipX = true;
        }
    }

    public void Damage()
    {
        if (isDead)
            return;

        if (GameManager.Instance.FlameEnabled)
            Health--;
        Health--;

        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity) as GameObject;
            diamond.GetComponent<Diamond>().gems = base.gems;
            Destroy(this.gameObject, 20f);
        }
    }

    public void Attack()
    {
        Vector3 shootPos = transform.position;
        shootPos.x -= 0.5f;
        GameObject acid = Instantiate(_acidGameObject, shootPos, Quaternion.identity) as GameObject;
        acid.GetComponent<AcidEffect>().faceRight = _direction.x > 0;
    }
}