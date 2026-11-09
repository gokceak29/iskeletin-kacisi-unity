using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 3f;
    private int direction = -1;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Oyun dünyasındaki "Player" etiketli iskeletimizi bul
        GameObject oyuncu = GameObject.FindGameObjectWithTag("Player");

        if (oyuncu != null)
        {
            // Eğer iskelet düşmanın SAĞINDAYSA
            if (oyuncu.transform.position.x > transform.position.x)
            {
                direction = 1; // Sağa doğru koş

                // Yüzünü sağa dönmesi için X boyutunu eksi yap
                Vector3 myScale = transform.localScale;
                if (myScale.x > 0) myScale.x *= -1;
                transform.localScale = myScale;
            }
            // Eğer iskelet düşmanın SOLUNDAYSA
            else
            {
                direction = -1; // Sola doğru koş

                // Yüzünü sola dönmesi için X boyutunu artı yap
                Vector3 myScale = transform.localScale;
                if (myScale.x < 0) myScale.x *= -1;
                transform.localScale = myScale;
            }
        }
    }

    void Update()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Duvara çarparsa yönünü dön (Mevcut kodun)
        if (collision.gameObject.CompareTag("Duvar"))
        {
            direction *= -1;
            Vector3 myScale = transform.localScale;
            myScale.x *= -1;
            transform.localScale = myScale;
        }
        // Bize (Player) çarparsa hasar ver (Mevcut kodun)
        else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth oyuncuCani = collision.gameObject.GetComponent<PlayerHealth>();
            if (oyuncuCani != null)
            {
                oyuncuCani.HasarAl();
            }
        }
    }
} 