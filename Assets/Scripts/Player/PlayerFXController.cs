using ToolKits;
using UnityEngine;

namespace Player
{
        public class PlayerFXController : GlobalSingleton<PlayerFXController>
        {
                [Header("特效位置调整")]
                public float offsetX = 0.15f;
                public float offsetY = -0.8f;
        
                private GameObject _jumpFX;     // 跳跃特效
                private GameObject _landFX;     // 落地特效

                private void Start()
                {
                        var playerRoot = transform.parent;
                        _jumpFX = playerRoot.Find("FX_jump").gameObject;
                        _landFX = playerRoot.Find("FX_land").gameObject;
                }
        
                public void ShowFX(string fxName)       // 播放特效
                {
                        switch (fxName)
                        {
                                case "Jump":
                                        FXStart(_jumpFX);
                                        break;
                                case "Land":
                                        FXStart(_landFX);
                                        break;
                                default:
                                        Debug.LogError("特效名称错误");
                                        break;
                        }
                }

                private void FXStart(GameObject fx)
                {
                        // 特效出现位置
                        var pos = PlayerController.Instance.transform.position;
                        fx.transform.position = new Vector3(pos.x + offsetX, pos.y + offsetY, pos.z);
                        fx.SetActive(true);
                }
        }
}