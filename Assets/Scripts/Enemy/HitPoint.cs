using System;
using Damage;
using UnityEngine;

namespace Enemy
{
    public class HitPoint : MonoBehaviour
    {
        private EnemyController _controller;

        private void Start()
        {
            _controller = GetComponentInParent<EnemyController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                print(">>> 玩家受到伤害");
                other.GetComponent<IDamageable>().GetHit(_controller.parameter.attackPower);
            }
            if (other.CompareTag("Bomb"))
            {
                print(">>> 炸弹被吹灭");
            } 
        }
    }
}