using System;
using Enemy;
using UnityEngine;

namespace FSM
{
    public class SkillAttackState : IState
    {
        private readonly EnemyController _manager;   // 状态机
        private readonly Parameter _parameter;   // 参数
        private float _timer; // 计时器
        
        public SkillAttackState(EnemyController manager)
        {
            _manager = manager;
            _parameter = manager.parameter;    
        }
        
        public void OnEnter()
        {
            _manager.SkillAttack();
            _timer = Time.time;
        }

        public void OnUpdate()
        {
            if (_parameter.hasBomb) return;
            
            if (_parameter.getHit)
            {
                _manager.TransitionState(StateType.Hit);
                return;
            }
            
            _manager.SkillOnUpdate();
            
            var info = _parameter.animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 0.95f)    // 播放完一次动画后就切换
            {
                if (Time.time - _timer <= _parameter.skillAttackCd) return;    // 攻击间隔时间内不切换
                _manager.TransitionState(StateType.Chase);
            }
        }

        public void OnExit()
        {
            _manager.SkillOnExit();
        }
    }
}