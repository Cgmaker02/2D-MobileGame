using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamageable
{
    public int diamonds;
    private Rigidbody2D _myBody;
    private PlayerAnimation _playerAnim;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private bool _resetJumpNeeded = false;
    private NewControls _input;
    private Animator _anim;
    private bool _deathOccurred = false;
    [SerializeField] private float _jumpForce = 5.0f;
    [SerializeField] private bool _grounded = false;
    [SerializeField] private float _speed;
    [SerializeField] private float _mobileSpeed;
    

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _myBody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _anim = GetComponentInChildren<Animator>();
        Health = 4;
        _input = new NewControls();
        _input.Player.Enable();
        _input.Player.Jump.performed += Jump_performed;
        _input.Player.Attack.performed += Attack_performed;
    }

    private void Attack_performed(InputAction.CallbackContext obj)
    {
        if(_deathOccurred == false)
        {
            _playerAnim.Attack();
        }
       
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if(_grounded == true && _deathOccurred == false)
        {
            _myBody.velocity = new Vector2(_myBody.velocity.x, _jumpForce);
            _grounded = false;
            _resetJumpNeeded = true;
            _playerAnim.Jump(true);
            StartCoroutine(JumpReset());
        }
       
    }



    // Update is called once per frame
    void Update()
    {
        if(_deathOccurred == false)
        {
            MobileMovement();
            Movement();
            Attack();
            GroundCheck();
        }
       
    }

    void Movement()
    {
       /* var x = Input.GetAxisRaw("Horizontal");

        if(x > 0)
        {
            Flip(true);
        }
        else if(x < 0)
        {
            Flip(false);
        }
        
        GroundCheck();

        if (Input.GetKeyDown(KeyCode.Space) && _grounded == true)
        {
            _myBody.velocity = new Vector2(_myBody.velocity.x, _jumpForce);
            _grounded = false;
            _resetJumpNeeded = true;
            _playerAnim.Jump(true);
            StartCoroutine(JumpReset());
        }

        _myBody.velocity = new Vector2(x * _speed, _myBody.velocity.y);
        _playerAnim.PlayerMove(x);*/
    }

    void GroundCheck()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, 1 << 6);
        Debug.DrawRay(transform.position, Vector2.down, Color.green);

        if (hitInfo.collider != null)
        {
            if(_resetJumpNeeded == false)
            {
                _grounded = true;
                _playerAnim.Jump(false);
            }
            
        }
    }
    

    void Flip(bool faceRight)
    {
        if (faceRight == true)
        {
            _playerSprite.flipX = false;
            _swordArcSprite.flipX = false;
            _swordArcSprite.flipY = false;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
        else if(faceRight == false)
        {
            _playerSprite.flipX = true;
            _swordArcSprite.flipX = true;
            _swordArcSprite.flipY = true;

            Vector3 newPos = _swordArcSprite.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcSprite.transform.localPosition = newPos;
        }
    }

    IEnumerator JumpReset()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false;
    }

    void Attack()
    {
       /* if(Input.GetMouseButtonDown(0))
        {
            _playerAnim.Attack();
        }*/
    }

    public void Damage()
    {
        if(Health < 1)
        {
            return;
        }

        Health--;
        UIManager.Instance.UpdateLives(Health);

        if(Health < 1)
        {
            Debug.Log("Death has occured");
            _playerAnim.Death();
            _deathOccurred = true;
            StartCoroutine(ResetGame());
        }
    }

    public void AddGems(int amount)
    {
        diamonds += amount;
        UIManager.Instance.UpdateGemCount(diamonds);
    }

    void MobileMovement()
    {
        var move = _input.Player.Move.ReadValue<Vector2>();
        _myBody.velocity = new Vector2(move.x * _mobileSpeed, _myBody.velocity.y);

        if (move.x > 0)
        {
            Flip(true);
        }
        else if(move.x < 0)
        {
            Flip(false);
        }
        _playerAnim.PlayerMove(move.x);
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(0);
    }
}
