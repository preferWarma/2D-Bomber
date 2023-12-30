using Damage;
using UnityEngine;

namespace Bomb
{
    public class BombController : MonoBehaviour
    {
        [Header("爆炸参数")] 
        [Tooltip("爆炸伤害")] public int bombDamage = 1;
        [Tooltip("爆炸时间")] public float bombCd = 2.0f;
        [Tooltip("爆炸范围")] public float bombRange = 1.4f;
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
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Off")) return;
            if (Time.time - _startTime >= bombCd)
            {
                _animator.Play("explosion");
            }
        }

        public void TurnOff()   // Animation Event
        {
            _animator.Play("Off");
            // 修改显示效果
            gameObject.layer = LayerMask.NameToLayer("NPC");
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        
        public void TurnOn()
        {
            _animator.Play("Bomb");
            // 修改显示效果
            gameObject.layer = LayerMask.NameToLayer("Bomb");
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
            // 重置计时器
            _startTime = Time.time;
        }
        
        private void Explosion() // Animation Event
        {
            var objs = Physics2D.OverlapCircleAll(transform.position, bombRange, affectLayer); // 爆炸范围内的检测物体
            foreach (var obj in objs)
            {
                // 排除自身
                if (obj.gameObject == gameObject)
                {
                    continue;
                }
                // 如果是熄灭的炸弹, 则重新点燃
                if (obj.gameObject.CompareTag("Bomb") && obj.gameObject.layer != LayerMask.NameToLayer("Bomb"))
                {
                    obj.gameObject.GetComponent<BombController>().TurnOn();
                }
                // 爆炸范围内的物体施加力
                var dir = (obj.transform.position - transform.position + Vector3.up).normalized; // 爆炸方向稍微向上
                obj.GetComponent<Rigidbody2D>().AddForce(dir * bombForce, ForceMode2D.Impulse); // 施加力
                // 如果是可攻击的物体, 则造成伤害
                if (obj.gameObject.CompareTag("Player") || obj.gameObject.CompareTag("NPC"))
                {
                    obj.GetComponent<IDamageable>()?.GetHit(bombDamage);
                }
            }
        }

        private void AfterExplosion() // Animation Event
        {
            Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, bombRange);
        }
    }
}