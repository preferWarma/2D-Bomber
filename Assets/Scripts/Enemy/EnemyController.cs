using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("巡逻点")] 
        public Transform pointA;
        public Transform pointB;
        
        [Header("自身属性")]
        public float moveSpeed = 1.0f;
        
        
        private readonly List<Transform> _attackList = new(); // 攻击目标列表
        
        private Transform _currentTarget;   // 当前攻击目标

        private void Start()
        {
            ChoosePoint();
        }
        
        private void Update()
        {
            // 判断是否到达目标点, 到达则选择另一个目标点
            if (Mathf.Abs(transform.position.x - _currentTarget.position.x) < 0.1f)
            {
                ChoosePoint();
            }
            MoveToTarget();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_attackList.Contains(other.transform)) return;
            _attackList.Add(other.transform);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_attackList.Contains(other.transform)) return;
            _attackList.Remove(other.transform);
        }

        // 普通攻击
        protected virtual void NormalAttack() { }
        // 技能攻击
        protected virtual void SkillAttack() { }

        private void MoveToTarget() // 移动到目标点
        {
            transform.position = Vector2.MoveTowards(transform.position, _currentTarget.position, moveSpeed * Time.deltaTime);
            FlipDirection();
        }
        
        private void FlipDirection()    // 翻转方向
        {
            // 判断当前位置与目标点的相对位置来翻转自身图片方向
            if (transform.position.x > _currentTarget.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
        private void ChoosePoint()  // 选择目标点
        {
            // 选择较远的点
            var disA = Mathf.Abs(transform.position.x - pointA.position.x);
            var disB = Mathf.Abs(transform.position.x - pointB.position.x);
            _currentTarget = disA > disB ? pointA : pointB;
        }
    }
}
