using UnityEngine;
using UnityEngine.UI; // Yazıları (Text) kontrol etmek için şarttır!

public class ScoreManager : MonoBehaviour
{
    // Bu, diğer kodlardan (örneğin Coin) buraya direkt ulaşmamızı sağlayan bir taktiktir (Singleton)
    public static ScoreManager instance;

    public int killCount = 0;
    public int coinCount = 0;
    public int killMultiplier = 1; // Varsayılan olarak her düşman 1 puan verir

    [Header("Arayüz Yazıları")]
    public Text killText;
    public Text coinText;

    void Awake()
    {
        // Oyun başlarken bu kodu "Merkez" olarak belirliyoruz
        instance = this;
    }

    // Düşman öldüğünde çalışacak fonksiyon
    public void AddKill()
    {
        killCount += killMultiplier; // Karakter öfkedeyse +3, normalde +1 ekler
        killText.text = "kill score: " + killCount;
    }

    // Altın toplandığında çalışacak fonksiyon
    public void AddCoin()
    {
        coinCount++;
        coinText.text = "coin score: " + coinCount;
    }
} 