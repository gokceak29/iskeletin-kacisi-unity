using UnityEngine;
using System.Collections; // Zamanlayıcı (Coroutine) kullanmak için şart

public class IksirSpawner : MonoBehaviour
{
    public GameObject iksirPrefab; // Klasördeki mavi iksir kalıbı
    public Transform[] spawnNoktalari; // İksirin çıkabileceği noktaların listesi
    public float spawnAraligi = 20f; // Kaç saniyede bir iksir çıkacağını buradan ayarlayabilirsin

    private GameObject aktifIksir; // Sahnedeki mevcut iksirin toplanıp toplanmadığını takip eder

    void Start()
    {
        // Oyun başlar başlamaz iksir üretim zamanlayıcısını başlatıyoruz
        StartCoroutine(IksirZamanlayiciDongusu());
    }

    IEnumerator IksirZamanlayiciDongusu()
    {
        while (true) // Oyun boyunca bu döngü hep arka planda dönecek
        {
            // Belirlediğin süre kadar (Örn: 20 saniye) kronometre çalışır
            yield return new WaitForSeconds(spawnAraligi);

            // Eğer oyuncu yerdeki iksiri henüz almadıysa (aktifIksir hala duruyorsa) YENİSİNİ ÜRETME
            // Ne zaman ki oyuncu iksiri alır ve haritadan silinirse (null olursa), yeni iksir üretilir
            if (aktifIksir == null)
            {
                IksirUret();
            }
        }
    }

    void IksirUret()
    {
        if (spawnNoktalari.Length == 0) return;

        // Noktalar arasından rastgele birini seç (Kura çekimi)
        int rastgeleIndex = Random.Range(0, spawnNoktalari.Length);
        Transform secilenNokta = spawnNoktalari[rastgeleIndex];

        // İksiri o noktada üret ve takibe al
        aktifIksir = Instantiate(iksirPrefab, secilenNokta.position, Quaternion.identity);
        Debug.Log("Haritada yeni bir Öfke İksiri belirdi!");
    }
} 