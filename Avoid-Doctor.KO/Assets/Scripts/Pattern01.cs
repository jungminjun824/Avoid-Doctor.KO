using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern01 : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int maxEnemyCount;
    [SerializeField] private float spawnCycle;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(nameof(SpawnEnemy));
    }

    private void OnDisable()
    {
        StopCoroutine(nameof(SpawnEnemy));
    }

    private IEnumerator SpawnEnemy()
    {
        float waitTime = 1;
        yield return new WaitForSeconds(waitTime);

        int count = 0;
        while (count < maxEnemyCount)
        {
            if(audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            Vector3 position = new Vector3(Random.Range(Constants.min.x, Constants.max.x), Constants.max.y, 0);
            Instantiate(enemyPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(spawnCycle);

            count++;
        }
        // 패턴 오브젝트 비활성화
        gameObject.SetActive(false);
    }
}
