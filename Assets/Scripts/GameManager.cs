using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public int numberOfSpawn;
    public float minX = -5.67f;
    public float maxX = 7.75f;
    public float setY = -1.585014f;
    public float delay = 5f;
    public static GameManager Instance;
    private int score = 0;
    public TextMeshProUGUI text;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        text.text = score.ToString();
    }
    public void IncreaseScore(int value)
    {
        score += value;
    }

    public void startSpawning()
    {
        InvokeRepeating("SpawnObject", 2f, delay);
    }
    void SpawnObject()
    {
        StartCoroutine(SpawnObjectswithDelay());
    }

    IEnumerator SpawnObjectswithDelay()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), setY);
        Instantiate(enemy, randomPosition, Quaternion.identity);

        yield return new WaitForSeconds(delay);
    }

    public void Surrender()
    {
        Destroy(player);
    }

}
