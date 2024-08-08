using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // ������ �� ������
    public Transform opponent; // ������ �� ����������
    public float followSpeed = 5f; // �������� ���������� ������ �� �������
    public float zoomInSize = 5f; // ��������������� ������ ������ ��� �����������
    public float zoomOutSize = 7f; // ��������������� ������ ������ ��� ���������
    public float zoomSpeed = 5f; // �������� ��������� ������� ������

    private Camera cam;
    private Transform currentTarget; // ������� ����, �� ������� ������ ��������
    private bool isMoving = false;

    private bool hasReachedFinish = false;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = zoomInSize; // ������������� ��������� ������ ������
        currentTarget = player; // ��������� ���� � �����
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
