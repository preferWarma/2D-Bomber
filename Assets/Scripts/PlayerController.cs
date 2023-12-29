using ToolKits;
using UnityEngine;

public class PlayerController : GlobalSingleton<PlayerController>
{
    [Header("控制参数")]
    [Tooltip("移动速度")] public float speed = 5.0f;
    [Tooltip("跳跃高度")] public float jumpFore = 10.0f;
    [Tooltip("炸弹CD")] public float bombCd = 1.5f;
    
    [Header("检测参数")]
    [Tooltip("地面检测点")] public Transform groundCheck;
    [Tooltip("地面检测半径")] public float groundCheckRadius = 0.1f;
    [Tooltip("地面层")] public LayerMask groundLayer;
    
    [Header("状态")]
    [Tooltip("是否在地面上")] public bool isGrounded;
    
    [Header("组件引用")]
    [Tooltip("自身刚体")] public Rigidbody2D rigidBody;
    [Tooltip("炸弹预制体")] public GameObject bombPrefab;
    
    private bool _canJump;
    private float _lastBombTime;    // 上次放炸弹时间

    private void Update()
    {
        PhysicsCheck();
        Movement();
        Jump();
        Attack();
    }

    private void Movement()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        rigidBody.velocity = new Vector2(horizontalInput * speed, rigidBody.velocity.y);  // 移动
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
        if (Input.GetKeyDown(KeyCode.K) && isGrounded)
        {
            PlayerFXController.Instance.ShowFX("Jump");    // 播放跳跃特效
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpFore);
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time - _lastBombTime >= bombCd)
        {
            _lastBombTime = Time.time;
            Instantiate(bombPrefab, transform.position, Quaternion.identity);
        }
    }
    
    // 地面检测
    private void PhysicsCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position , groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            rigidBody.gravityScale = 1;
        }
        else
        {
            rigidBody.gravityScale = 4;
        }
    }
    
    // 绘制地面检测点
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
