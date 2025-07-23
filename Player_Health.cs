using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Player_Health : MonoBehaviour
{
    public Image[] hearts;
    public int maxHealth = 3;
    private int currentHealth;

    public GameObject gameOverPanel;
    private bool isJumping = false;
    private bool isSliding = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (isJumping || isSliding)
            {
                Debug.Log("È¸ÇÇ");
                return;
            }
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHeartsUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
  
