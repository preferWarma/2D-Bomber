using Damage;
using UnityEngine;

namespace Enemy
{
    public class HitPoint : MonoBehaviour
    {
        private EnemyController _controller;

        public bool canHitBomb;
        private void Start()
        {
            _controller = GetComponentInParent<EnemyController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<IDamageable>().GetHit(_controller.parameter.attackPower);
            }

            if (other.CompareTag("Bomb"))
            {
                if (!canHitBomb) return;
                var dir = transform.position.x > other.transform.position.x ? -1 : 1;
                other.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * 10, 10), ForceMode2D.Impulse);
            }
        }
    }
}