using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UIManager UIManager;

    [SerializeField] private int health = 3;
    public int Score { get; private set; }

    public void AddScore()
    {
        Score += 1;
        UIManager.UpdateScore(Score);
    }

    public void TakeDamage()
    {
        health -= 1;
        UIManager.UpdateHealth(health);

        if (health > 0)
            ResetPlayer();
        else
            GameOver();
    }

    private void ResetPlayer()
    {
        player.gameObject.SetActive(false);
        player.transform.position = Vector3.zero;
        Invoke(nameof(Reboot), 2);
    }

    private void Reboot()
    {
        player.gameObject.SetActive(true);
        player.SetInvulnerableState();
    }

    private void GameOver()
    {
        player.gameObject.SetActive(false);
        RestartGame();
    }

    public void RestartGame()
    {
        foreach (var asteroid in FindObjectsByType<Asteroid>(FindObjectsSortMode.None))
            Destroy(asteroid.gameObject);

        player.transform.position = Vector3.zero;
        player.gameObject.SetActive(true);

        Score = 0;
        UIManager.UpdateScore(Score);
        health = 3;
        UIManager.RestartHealth();
    }
}
