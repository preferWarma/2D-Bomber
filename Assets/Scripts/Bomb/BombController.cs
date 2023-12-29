using System;
using UnityEngine;

namespace Bomb
{
    public class BombController : MonoBehaviour
    {
        [Header("爆炸参数")]
        [Tooltip("爆炸时间")] public float bombCd = 3.0f;
        [Tooltip("爆炸范围")] public float bombRange = 1.0f;
        [Tooltip("爆炸力度")] public float bombForce = 10.0f;
        [Tooltip("影响层")] public LayerMask affectLayer;
        
        private Animator _animator;
        private float _startTime;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _startTime = Time.time;
        }

        private void Update()
        {
            if (Time.time - _startTime >= bombCd)
            {
                _animator.Play("explosion");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, bombRange);
        }
        
        private void Explosion()    // 爆炸
        {
            var objs = Physics2D.OverlapCircleAll(transform.position, bombRange, affectLayer);  // 爆炸范围内的检测物体
            foreach (var obj in objs)
            {
                // 排除自身
                if (obj.gameObject == gameObject)
                {
                    continue;
                }
                
                var dir = (obj.transform.position - transform.position + Vector3.up).normalized;    // 爆炸方向稍微向上
                obj.GetComponent<Rigidbody2D>().AddForce(dir * bombForce, ForceMode2D.Impulse);    // 施加力
            }
        }
        
        private void AfterExplosion()   // 爆炸后
        {
            Destroy(gameObject);
        }
    }
}
