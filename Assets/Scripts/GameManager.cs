using Player;
using QFramework;
using ToolKits;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GlobalSingleton<GameManager>
{
    public BindableProperty<bool> GameOver = new();

    private void Start()
    {
        GameOver.RegisterWithInitValue(isOver =>
        {
            if (isOver)
            {
                UIManager.Instance.gameOverPanel.SetActive(true);
            }
        });
        
        PlayerController.Instance.Health.RegisterWithInitValue(health =>
        {
            GameOver.Value = health == 0;
        });
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
