using System;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
        private Animator _animator;     // Player动画控制器
        
        // Animator参数
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int Ground = Animator.StringToHash("Ground");
        private static readonly int VelocityY = Animator.StringToHash("Velocity_Y");
        private static readonly int Dead = Animator.StringToHash("Dead");

        private void Start()
        {
                _animator = GetComponent<Animator>();
        }

        private void Update()
        {
                _animator.SetFloat(Speed, Math.Abs(PlayerController.Instance.rigidBody.velocity.x));    // 设置Speed参数
                _animator.SetFloat(VelocityY, PlayerController.Instance.rigidBody.velocity.y);       // 设置Velocity_Y参数
                _animator.SetBool(Jump, !PlayerController.Instance.isGrounded);       // 设置Jump参数;
                _animator.SetBool(Ground, PlayerController.Instance.isGrounded);        // 设置Ground参数
                _animator.SetBool(Dead, PlayerController.Instance.isDead);      // 设置Death参数
        }
}