using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float moveSpeed = 10f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine firing;
    float padding = 0.5f;
    // объект для управления копиями лазера
    [SerializeField] GameObject laserPrefab;
    // объект-корутин для управления остановкой стрельбы игрока
    Coroutine firingCoroutine;

    private void Bounds()
    {
        Camera camera = Camera.main;
        xMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = camera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = camera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = camera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Start is called before the first frame update
    void Start()
    {
        Bounds();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Fire();
    }

    private void PlayerMove()
    {
        float deltaX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float newX = Mathf.Clamp( transform.position.x + deltaX, xMin, xMax);
        float deltaY = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float newY = Mathf.Clamp( transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newX, newY);
    }

    // функция для стрельбы
    private void Fire()
    {
        // если нажали на кнопку Fire1, то есть в нашем случае - пробел
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        // если отпустили клавишу пробел
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    // фуекция-корутин для стрельбы
    IEnumerator FireContinuously()
    {
        while (true)
        {
            // Instantiate(что создаем, позиция в момент создания, вращение)
            // laserPrefab - копия лазера, transform.position - создаем в том месте, где находится игрок
            // Quaternion.identity - при создании не вращаем объект
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            // говорим элементу Rigidbody2D принять в качестве своей скорости вектор Vector2(0, projectileSpeed),
            // где projectileSpeed - скорость движения лазера (изменение координаты У)
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
