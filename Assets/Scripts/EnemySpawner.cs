using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // список WaveConfig всех путей в нашей игре
    [SerializeField] List<WaveConfig> waveConfigs;
    // опция зацикливания спауна волн врагов
    [SerializeField] bool looping = false;
    // номер волны, с которой все начнётся
    int startingWave = 0;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            // спавним волны с помощью корутина
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
    }

    // корутин для спавна всех волн
    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            // с помощью индекса-номера волны получили первый WaveConfig для волны врагов
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    // корутин для спавна всех врагов для какой-то определенной волны волны
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (int enemyCount = 0; enemyCount < currentWave.GetNumberOfEmenies(); enemyCount++)
        {
            // создали врага 
            var newEnemy = Instantiate(
                currentWave.GetEnemyPrefab(),
                currentWave.GetWaypoints()[0].transform.position,
                Quaternion.identity);
            // задали для врага WaveConfig чтобы он мог использовать нужные ему параметры волны спауна
            newEnemy.GetComponent<EnemyMove>().SetWaveConfig(currentWave);
            // ждём указанное в WaveConfig время между волнами врагов 
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawns());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
