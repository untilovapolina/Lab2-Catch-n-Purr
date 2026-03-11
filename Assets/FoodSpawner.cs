using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] foodPrefabs;

    public float spawnDelay = 2f;

    public float spawnWidth = 7f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime; 

        if (timer >= spawnDelay)
        {
            SpawnFood(); 
            timer = 0f; 
        }
    }

    void SpawnFood()
    {
        int randomIndex = Random.Range(0, foodPrefabs.Length);
        GameObject selectedFood = foodPrefabs[randomIndex];

        float randomX = Random.Range(-spawnWidth, spawnWidth);
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0f);

        Instantiate(selectedFood, spawnPosition, Quaternion.identity);
    }
}