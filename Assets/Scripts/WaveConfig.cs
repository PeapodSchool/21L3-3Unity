using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// добавили возможность создания объектов WaveConfig с помощью меню в редакторе юнити
[CreateAssetMenu(menuName = "Enemy Wave Config")]
// ScriptableObject - что-то вроде большой переменной для хранения каких-то значений
public class WaveConfig : ScriptableObject
{
    // пребаф врага в волне
    [SerializeField] GameObject enemyPrefab;
    // префаб пути для волны
    [SerializeField] GameObject pathPrefab;
    // время между волнами
    [SerializeField] float timeBetweenSpawns = 0.5f;
    // значение, с помощью которого мы будем корректироват время спауна, делаеть его более случайным
    [SerializeField] float spawnRandomFactor = 0.3f;
    // кол-во врагов в волне
    [SerializeField] int numberOfEnemies = 5;
    // скорость врага в волне
    [SerializeField] float moveSpeed = 2f;

    // набор Гетеров для получения значений полей объекта WaveConfig

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        // в цикле прошли по объекту Path и "вытищили" все точки в список
        foreach (Transform point in pathPrefab.transform)
        {
            waveWaypoints.Add(point);
        }
        return waveWaypoints;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberOfEmenies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }
}
