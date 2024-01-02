using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class CheckArea : MonoBehaviour
    {
        private EnemyController _manager;
        private Parameter _parameter;
        private void Start()
        {
            _manager = GetComponentInParent<EnemyController>();
            _parameter = _manager.parameter;
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_parameter.attackList.Contains(other.transform) || _parameter.hasBomb) return;
            _parameter.attackList.Add(other.transform);
            _manager.ChooseTarget();
            StartCoroutine(OnAlert());
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_parameter.attackList.Contains(other.transform)) return;
            _parameter.attackList.Remove(other.transform);
            _manager.ChooseTarget();
        }

        // 用协程实现警示标识，警示动画播放完成后关闭显示
        private IEnumerator OnAlert()
        {
            if (_parameter.health == 0) yield return null;
            
            _parameter.alertSignal.SetActive(true);
            yield return new WaitForSeconds(_parameter.alertSignal.GetComponent<Animator>().
                GetCurrentAnimatorClipInfo(0)[0].clip.length);  // 等待动画播放完成
            _parameter.alertSignal.SetActive(false);
        }
    }
}