using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    public float jumpPower = 15f; // Zıplama gücünü kendi ayarına göre düzeltebilirsin
    private Rigidbody2D rb;
    private bool isGrounded = false;
    private bool isFacingRight = true;

    [Header("Animasyon Görselleri")]
    public Sprite normalGorsel;  // Yerde dururkenki resim
    public Sprite ziplamaGorseli; // Havadaykenki bacakları açık resim
    private SpriteRenderer spriteRenderer; // Karakterin resmini tutan bileşen

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer'ı koda tanıtıyoruz
    }

    void Update()
    {
        // A-D hareket
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        // Yön Kontrolü
        if (moveInput > 0 && !isFacingRight) Flip();
        else if (moveInput < 0 && isFacingRight) Flip();

        // Space zıplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        // --- GÖRSEL DEĞİŞTİRME SİSTEMİ ---
        // Eğer karakter yerde değilse (havadaysa), zıplama resmini koy
        if (isGrounded == false)
        {
            spriteRenderer.sprite = ziplamaGorseli;
        }
        // Eğer yerdeyse normal resme geri dön
        else
        {
            spriteRenderer.sprite = normalGorsel;
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // İksir toplandığında dışarıdan çağrılacak ana fonksiyon
    public void OfeModuAc(float sure)
    {
        StartCoroutine(OfkeModuCoroutine(sure));
    }

    IEnumerator OfkeModuCoroutine(float sure)
    {
        float orijinalHiz = speed; // Orijinal hızımızı hafızaya alıyoruz
        speed = orijinalHiz * 2f;  // Karakterin hızını iki katına çıkarıyoruz

        // Karakterin görsel rengini kıpkırmızı yapıyoruz (Öfke efekti)
        spriteRenderer.color = Color.red;

        // Skor yöneticisindeki öldürme çarpanını 3 yapıyoruz
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.killMultiplier = 3;
        }

        // Öfke modu süresince (10 saniye) oyunu akışında bekletiyoruz
        yield return new WaitForSeconds(sure);

        // --- SÜRE BİTTİ: HER ŞEYİ ESKİ HALİNE DÖNDÜRÜYORUZ ---
        speed = orijinalHiz;
        spriteRenderer.color = Color.white; // Rengi normale döndür

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.killMultiplier = 1; // Puanı normale döndür
        }
    } 
}