using UnityEngine;

public class Iksir : MonoBehaviour
{
    public float ofkeSuresi = 10f; // Güçlendirmenin kaç saniye süreceği

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Eğer iksire değen obje iskeletimiz (Player) ise
        if (collision.CompareTag("Player"))
        {
            PlayerMove oyuncuHareket = collision.GetComponent<PlayerMove>();

            if (oyuncuHareket != null)
            {
                // Karakterin içindeki öfke modunu başlat
                oyuncuHareket.OfeModuAc(ofkeSuresi);

                // İksir şişesini sahneden yok et
                Destroy(gameObject);
            }
        }
    }
} 