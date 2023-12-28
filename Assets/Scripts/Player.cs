using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("控制参数")]
    [Tooltip("移动速度")] public float speed = 5.0f;
    [Tooltip("跳跃高度")] public float jumpFore = 10.0f;
    
    [Header("检测参数")]
    [Tooltip("地面检测点")] public Transform groundCheck;
    [Tooltip("地面检测半径")] public float groundCheckRadius = 0.1f;
    [Tooltip("地面层")] public LayerMask groundLayer;
    
    [Header("状态")]
    [Tooltip("是否在地面上")] public bool isGrounded;
    
    private bool _canJump;
    private Rigidbody2D _rigidBody;
    
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PhysicsCheck();
        CheckInput();
        Movement();
        Jump();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            _canJump = true;
        }
    }

    private void Movement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(horizontalInput * speed, _rigidBody.velocity.y);  // 移动
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        
    }

    private void Jump()
    {
        if (_canJump)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpFore);
            _canJump = false;
        }
    }
    
    // 地面检测
    private void PhysicsCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position , groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            _rigidBody.gravityScale = 1;
        }
        else
        {
            _rigidBody.gravityScale = 4;
        }
    }
    
    // 绘制地面检测点
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
