using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab; // Üreteceğimiz altın kalıbı
    public Transform[] spawnNoktalari; // Altınların çıkabileceği noktalar listesi
    public float beklemeSuresi = 2f; // Altınlar bitince kaç saniye beklenecek?

    void Start()
    {
        // Oyun başlar başlamaz altın kontrol döngüsünü başlat
        StartCoroutine(CoinDongusu());
    }

    IEnumerator CoinDongusu()
    {
        while (true) // Oyun boyunca bu döngü hiç durmadan çalışır
        {
            // Sahnede o an kaç tane "Coin" etiketli obje var sayalım
            GameObject[] sahadakiCoinler = GameObject.FindGameObjectsWithTag("Coin");

            // Eğer sahnede hiç coin kalmadıysa (hepsi toplandıysa)
            if (sahadakiCoinler.Length == 0)
            {
                yield return new WaitForSeconds(beklemeSuresi); // 2 saniye bekle
                CoinUret(); // Yeni altınları sahaya saç
            }

            // Sistemi yormamak için her kontrol arasında yarım saniye bekle
            yield return new WaitForSeconds(0.5f);
        }
    }

    void CoinUret()
    {
        // Nokta listemiz boşsa hata vermemesi için güvenlik önlemi
        if (spawnNoktalari.Length == 0) return;

        // 1 veya 2 adet coin üretilmesine rastgele karar ver
        int uretilecekSayi = Random.Range(1, 3);

        for (int i = 0; i < uretilecekSayi; i++)
        {
            // Belirlediğimiz noktalardan rastgele birinin sırasını seç
            int rastgeleIndex = Random.Range(0, spawnNoktalari.Length);
            Transform secilenNokta = spawnNoktalari[rastgeleIndex];

            // Seçilen noktada altını oluştur
            Instantiate(coinPrefab, secilenNokta.position, Quaternion.identity);
        }
    }
} 