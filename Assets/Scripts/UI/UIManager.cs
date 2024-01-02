using Player;
using ToolKits;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : GlobalSingleton<UIManager>
    {
        [Header("UI组件引用")]
        public GameObject healthBar;
        public GameObject gameOverPanel;
        public Button pauseBtn;
        public Button resumeBtn;
        public Button quitBtn;
        
        private void Start()
        {
            PlayerController.Instance.Health.RegisterWithInitValue(UpDateHealth);
            pauseBtn.onClick.AddListener(PauseGame);
            resumeBtn.onClick.AddListener(ResumeGame);
            quitBtn.onClick.AddListener(Application.Quit);
        }

        private void UpDateHealth(int health)
        {
            for (var i = 0; i < healthBar.transform.childCount; i++)
            {
                if (health > 0)
                {
                    healthBar.transform.GetChild(i).gameObject.SetActive(true);
                    health--;
                }
                else
                {
                    healthBar.transform.GetChild(i).gameObject.SetActive(false); 
                }
            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
        }
        
        private void ResumeGame()
        {
            Time.timeScale = 1;
        }
    }
}
