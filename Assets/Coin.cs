using UnityEngine;

public class Coin : MonoBehaviour
{
    // Birisi bu altına değdiğinde tetiklenir
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Değen obje iskeletimiz (Player) ise
        if (collision.CompareTag("Player"))
        {
            ScoreManager.instance.AddCoin();
            Destroy(gameObject); // Altını sahneden (oyundan) tamamen sil
        }
    }
} 