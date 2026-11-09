using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // Zamanlayıcılar (Coroutine) için gerekli

public class MenuManager : MonoBehaviour
{
    public GameObject girisPaneli;
    public CanvasGroup girisCanvasGroup; // Şeffaflığı kontrol edeceğimiz bileşen

    [Header("Oyun İçi Arayüzler")]
    public GameObject skorArayuzu;

    void Start()
    {
        Time.timeScale = 0f; // Oyun donuk başlar
        if (girisPaneli != null) girisPaneli.SetActive(true);
        if (skorArayuzu != null) skorArayuzu.SetActive(false);
    }

    public void OyunuBaslat()
    {
        // Anında kapatmak yerine yavaşça kaybetme efektini başlatıyoruz
        StartCoroutine(GirisEkraniniKaybet());
    }

    IEnumerator GirisEkraniniKaybet()
    {
        float sure = 0.5f; // Kaybolma süresi (Yarım saniye. İstersen artırabilirsin)
        float gecenZaman = 0f;

        // Zaman akarken şeffaflığı 1'den (tam görünür) 0'a (görünmez) doğru yavaşça indiriyoruz
        while (gecenZaman < sure)
        {
            // Time.timeScale 0 olduğu için gerçek dünya zamanını (unscaledDeltaTime) kullanmak zorundayız!
            gecenZaman += Time.unscaledDeltaTime;
            girisCanvasGroup.alpha = Mathf.Lerp(1f, 0f, gecenZaman / sure);
            yield return null;
        }

        // Tamamen görünmez olunca paneli gerçekten kapat ve oyunu başlat
        girisPaneli.SetActive(false);
        if (skorArayuzu != null) skorArayuzu.SetActive(true);
        Time.timeScale = 1f;
    }

    public void YenidenBasla()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
} 