using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnNoktalari; // Düşmanın çıkabileceği noktaların listesi
    private GameObject currentEnemy;

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        if (currentEnemy == null)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Eğer liste boşsa hata vermemesi için kontrol
        if (spawnNoktalari.Length == 0) return;

        // Noktalar arasından rastgele birini seç (Kura çekimi)
        int rastgeleIndex = Random.Range(0, spawnNoktalari.Length);
        Transform secilenNokta = spawnNoktalari[rastgeleIndex];

        // Düşmanı o seçilen noktada üret
        currentEnemy = Instantiate(enemyPrefab, secilenNokta.position, Quaternion.identity);
    }
} 