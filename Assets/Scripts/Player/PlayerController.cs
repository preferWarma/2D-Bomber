using System;
using Damage;
using QFramework;
using ToolKits;
using UnityEngine;

namespace Player
{
    public class PlayerController : GlobalSingleton<PlayerController> , IDamageable
    {
        [Header("控制参数")] 
        [Tooltip("生命值")] public readonly BindableProperty<int> Health = new(3);
        [Tooltip("移动速度")] public float speed = 5.0f;
        [Tooltip("跳跃高度")] public float jumpFore = 10.0f;
        [Tooltip("炸弹CD")] public float bombCd = 1.5f;
    
        [Header("检测参数")]
        [Tooltip("地面检测点")] public Transform groundCheck;
        [Tooltip("地面检测半径")] public float groundCheckRadius = 0.1f;
        [Tooltip("地面层")] public LayerMask groundLayer;
    
        [Header("组件引用")]
        [Tooltip("自身刚体")] public Rigidbody2D rigidBody;
        [Tooltip("炸弹预制体")] public GameObject bombPrefab;
    
        public bool isGrounded;   // 是否在地面上
        public bool isDead;   // 是否死亡
    
        private bool _canJump;  // 是否可以跳跃
        private float _lastBombTime;    // 上次放炸弹时间
        private FixedJoystick _joystick;    // 操作杆

        private void Start()
        {
            _joystick = FindObjectOfType<FixedJoystick>();
        }

        private void Update()
        {
            if (isDead) return;
            PhysicsCheck();
            Movement();
        }

        private void Movement()
        {
            // var horizontalInput = Input.GetAxis("Horizontal");
            var horizontalInput = _joystick.Horizontal;
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

        public void Jump()
        {
            if (isGrounded)
            {
                PlayerFXController.Instance.ShowFX("Jump");    // 播放跳跃特效
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpFore);
            }
        }

        public void Attack()
        {
            if (Time.time - _lastBombTime >= bombCd)
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

        public void GetHit(int damage)
        {
            // 受击动画播放中处于无敌状态
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(1).IsName("Hit")) return;
        
            Health.Value -= damage;
            if (Health.Value <= 0)
            {
                Health.Value = 0;
                isDead = true;
                rigidBody.velocity = Vector2.zero;
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Hit");
            }
        }
    }
}
