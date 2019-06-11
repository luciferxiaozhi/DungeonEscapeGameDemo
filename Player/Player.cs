using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _rigid;
    [SerializeField] private LayerMask _groundLayer;
    private bool _resetJump;
    private bool _isDead;
    [SerializeField]
    private float _speed = 2.5f;
    private SpriteRenderer _spriteRender;
    private SpriteRenderer _swordArcSprite;
    [SerializeField] private bool _grounded;

    private PlayerAnimation _playerAnim;

    public int Health { get; set; }
    public int amountOfDiamond = 0;
    public float jumpForce = 8.0f;

    // Start is called before the first frame update
    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _spriteRender = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        Health = 4;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isDead)
            return;

        Movement();

        if (/*Input.GetMouseButtonDown(0) || */CrossPlatformInputManager.GetButtonDown("A_Button") && IsGrounded())
        {
            if (!GameManager.Instance.FlameEnabled)
                _playerAnim.Attack();
            else
                _playerAnim.FlameAttack();
        }
    }

    void Movement()
    {
        float move = CrossPlatformInputManager.GetAxis("Horizontal");
//        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        if (move > 0) // flip
        {
            Flip(true);
        }
        else if(move < 0)
        {
            Flip(false);
        }

        if ((/*Input.GetKeyDown(KeyCode.Space) ||*/ CrossPlatformInputManager.GetButtonDown("B_Button")) && IsGrounded())
        {
            Debug.Log("Jump!");
            _rigid.velocity = new Vector2(_rigid.velocity.x, jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2(move * _speed, _rigid.velocity.y);
        _playerAnim.Move(move);
    }

    void Flip(bool faceRight)
    {
        if (faceRight)
        {
            _spriteRender.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = newPos.x > 0? newPos.x : -newPos.x;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else
        {
            _spriteRender.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = newPos.x < 0? newPos.x : -newPos.x;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, .8f, _groundLayer);
        if (hitInfo.collider)
        {
            if (!_resetJump)
            {
                _playerAnim.Jump(false);
                return true;
            }
        }

        return false;
    }

    public void Damage()
    {
        if (_isDead)
            return;

        int prevHealth = Health;
        Health--;
        if (Health < 1)
        {
            _isDead = true;
            _playerAnim.Death();
            if (prevHealth > 0)
            {
                UIManager.Instance.UpdateLives(Health);
            }
            UIManager.Instance.GameOver();
        }
        else
        {
            _playerAnim.Hit();
            UIManager.Instance.UpdateLives(Health);
        }
    }

    public void Die()
    {
        while (Health != 1)
            Health--;
        UIManager.Instance.UpdateLives(3);
        UIManager.Instance.UpdateLives(2);
        UIManager.Instance.UpdateLives(1);
        Damage();
    }

    public void AddGems(int amount)
    {
        amountOfDiamond += amount;
        UIManager.Instance.UpdateGemCount(amountOfDiamond);
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

}
