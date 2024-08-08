using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Ссылка на игрока
    public Transform opponent; // Ссылка на противника
    public float followSpeed = 5f; // Скорость следования камеры за игроком
    public float zoomInSize = 5f; // Ортографический размер камеры при приближении
    public float zoomOutSize = 7f; // Ортографический размер камеры при отдалении
    public float zoomSpeed = 5f; // Скорость изменения размера камеры

    private Camera cam;
    private Transform currentTarget; // Текущая цель, на которую камера нацелена
    private bool isMoving = false;

    private bool hasReachedFinish = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = zoomInSize; // Устанавливаем начальный размер камеры
        currentTarget = player; // Начальная цель — игрок
    }

    void Update()
    {
        if (!hasReachedFinish)
            FollowTarget();
    }

    void FollowTarget()
    {
        if (currentTarget != null)
        {
            Vector3 targetPosition = new Vector3(currentTarget.position.x, currentTarget.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    public void FocusOnPlayer()
    {
        currentTarget = player;
        StartCoroutine(ZoomIn());
    }

    public void FocusOnOpponent()
    {
        currentTarget = opponent;
        StartCoroutine(ZoomOut());
    }

    public void StartZoomOut()
    {
        isMoving = true;
        StopCoroutine("ZoomIn");
        StartCoroutine("ZoomOut");
    }

    public void StopZoomOut()
    {
        isMoving = false;
        StopCoroutine("ZoomOut");
        StartCoroutine("ZoomIn");
    }

    private IEnumerator ZoomOut()
    {
        while (isMoving && cam.orthographicSize < zoomOutSize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomOutSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator ZoomIn()
    {
        while (!isMoving && cam.orthographicSize > zoomInSize)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoomInSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void OnReachFinish()
    {
        hasReachedFinish = true;
    }
}
