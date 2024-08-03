using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern01 : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
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
        
        while (true)
        {
            if(audioSource.isPlaying == false)
            {
                audioSource.Play();
            }

            Vector3 position = new Vector3(Random.Range(Constants.min.x, Constants.max.x), Constants.max.y, 0);
            Instantiate(enemyPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(spawnCycle);
        }
    }
}
