using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image[] _healthBar;

    public void UpdateScore(int score) => _scoreText.text = $"Score: {score}";

    public void UpdateHealth(int health) => _healthBar[health].enabled = false;

    public void RestartHealth()
    {
        foreach (var count in _healthBar)
            count.enabled = true;
    }

}
