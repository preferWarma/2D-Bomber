using System;
using System.Collections.Generic;
using FSM;
using UnityEngine;

namespace Enemy
{
    public enum StateType
    {
        Idle, Patrol, Chase, React, Attack, Hit, Death
    }

    [Serializable]
    public class Parameter  // 参数类
    {
        [Header("基本参数")]
        [Tooltip("生命值")] public int health;
        [Tooltip("移动速度")] public float moveSpeed;
        [Tooltip("追击速度")] public float chaseSpeed;  
        [Tooltip("停止时间")] public float idleTime;
        [Tooltip("巡逻范围")] public Transform[] patrolPoints;
        [Tooltip("追击范围")] public Transform[] chasePoints;
        
        [Header("攻击参数")]
        [Tooltip("攻击目标队列")] public List<Transform> attackList;
        [Tooltip("当前攻击目标")] public Transform currentTarget;
        [Tooltip("攻击目标层级")] public LayerMask targetLayer;
        [Tooltip("攻击检测范围圆心")] public Transform attackPoint;
        [Tooltip("检测范围半径")] public float attackRadius;
        [Tooltip("是否是受击状态")] public bool getHit;
        
        [Header("动画器参数")]
        [Tooltip("动画器组件")] public Animator animator;        
    }
    
    public class EnemyController : MonoBehaviour
    {
        public Parameter parameter = new();  // 参数
        
        private IState _currentState; // 当前状态
        private readonly Dictionary<StateType, IState> _states = new(); // 状态字典

        private void Awake()
        {
            parameter.animator = GetComponent<Animator>();
        }

        private void Start()
        {
            // 状态机注册
             _states.Add(StateType.Idle, new IdleState(this));
             _states.Add(StateType.Patrol, new PatrolState(this));
             _states.Add(StateType.Attack, new AttackState(this));
            
             // 初始状态
             TransitionState(StateType.Idle);
            
        }

        private void Update()
        {
            _currentState.OnUpdate();
        }
        
        public void TransitionState(StateType newState)
        {
            _currentState?.OnExit();
            _currentState = _states[newState];
            _currentState.OnEnter();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (parameter.attackList.Contains(other.transform)) return;
            parameter.attackList.Add(other.transform);
            ChooseTarget();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!parameter.attackList.Contains(other.transform)) return;
            parameter.attackList.Remove(other.transform);
            ChooseTarget();
        }
        
        // 从攻击目标队列中选择一个目标, 玩家优先, 玩家不在范围内则优先选择距离最近的目标
        private void ChooseTarget()
        {
            if (parameter.attackList.Count == 0)
            {
                parameter.currentTarget = null;
                return;
            }
            
            // 如果玩家在攻击范围内, 则优先攻击玩家
            if (parameter.attackList.Contains(PlayerController.Instance.transform))
            {
                parameter.currentTarget = PlayerController.Instance.transform;
                return;
            }
            
            // 否则选择距离最近的目标
            var idx = 0;
            var distance = 10000f;
            for (var i = 0; i < parameter.attackList.Count; i++)
            {
                var target = parameter.attackList[i];
                var curDis = Vector2.Distance(transform.position, target.position);
                if (curDis < distance)
                {
                    distance = curDis;
                    idx = i;
                }
            }
            parameter.currentTarget = parameter.attackList[idx];
        }
        
        public void FlipDirection(Transform target)    // 翻转方向
        {
            if (!target) return;
            // 判断当前位置与目标点的相对位置来翻转自身图片方向
            if (transform.position.x > target.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
    }
}
