using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int can = 3;

    [Header("UI Kalpler")]
    public GameObject[] kalpler;

    [Header("Oyun Sonu Ekranı")]
    public GameObject gameOverPanel;
    public CanvasGroup gameOverCanvasGroup; // Şeffaflığı kontrol edeceğimiz bileşen

    private SpriteRenderer spriteRenderer;
    private bool isInvincible = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void HasarAl()
    {
        if (isInvincible) return;

        can--;

        if (can >= 0 && can < kalpler.Length)
        {
            kalpler[can].SetActive(false);
        }

        if (can <= 0)
        {
            Time.timeScale = 0f; // Oyunu hemen durdur

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true); // Paneli aç
                StartCoroutine(GameOverEkraniGetir()); // Yavaşça belirme efektini başlat
            }
        }
        else
        {
            StartCoroutine(HasarGidipGelmeEfekti());
        }
    }

    IEnumerator GameOverEkraniGetir()
    {
        float sure = 1.0f; // Ekrana gelme süresi (1 saniye, dramatik etki için daha yavaş)
        float gecenZaman = 0f;

        gameOverCanvasGroup.alpha = 0f; // Önce tamamen şeffaf yapıyoruz

        // Şeffaflığı 0'dan 1'e doğru yavaşça artırıyoruz
        while (gecenZaman < sure)
        {
            gecenZaman += Time.unscaledDeltaTime;
            gameOverCanvasGroup.alpha = Mathf.Lerp(0f, 1f, gecenZaman / sure);
            yield return null;
        }
    }

    IEnumerator HasarGidipGelmeEfekti()
    {
        isInvincible = true;
        for (int i = 0; i < 3; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.15f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(0.15f);
        }
        isInvincible = false;
    }
} 