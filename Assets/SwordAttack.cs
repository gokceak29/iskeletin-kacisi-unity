using UnityEngine;
using System.Collections; // Zamanlayıcı (bekleme) işlemleri için gerekli kütüphane

public class SwordAttack : MonoBehaviour
{
    private BoxCollider2D kilicCollider;
    private bool isAttacking = false;
    private Vector3 baslangicAcisi; // Kılıcın duruş açısını aklımızda tutacağız

    void Start()
    {
        kilicCollider = GetComponent<BoxCollider2D>();

        // Oyun başladığında kılıcın kesme özelliğini (collider'ı) kapatıyoruz
        kilicCollider.enabled = false;

        // Kılıcın Unity'de ayarladığın o ilk duruş açısını kaydediyoruz
        baslangicAcisi = transform.localEulerAngles;
    }

    void Update()
    {
        // Farenin sol tuşuna (0) basıldığında ve şu an zaten saldırmıyorsak
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            StartCoroutine(SaldiriYap());
        }
    }

    // Coroutine: Unity'ye "zamanı bekleterek işlem yap" dediğimiz özel yapı
    IEnumerator SaldiriYap()
    {
        isAttacking = true;
        kilicCollider.enabled = true; // Kılıç artık düşmanı kesebilir!

        // Kılıcı görsel olarak savurmak için açısını 60 derece eğiyoruz
        transform.localEulerAngles = new Vector3(baslangicAcisi.x, baslangicAcisi.y, baslangicAcisi.z - 60f);

        // Kılıcın havada kalma (savrulma) süresi. Burayı zevkine göre değiştirebilirsin
        yield return new WaitForSeconds(0.15f);

        // Kılıcı tekrar eski duruşuna getirip kesme özelliğini kapatıyoruz
        transform.localEulerAngles = baslangicAcisi;
        kilicCollider.enabled = false;
        isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kılıç savrulurken düşmana değerse
        if (other.CompareTag("Enemy"))
        {
            ScoreManager.instance.AddKill(); 

            Destroy(other.gameObject);
        }
    }
} 