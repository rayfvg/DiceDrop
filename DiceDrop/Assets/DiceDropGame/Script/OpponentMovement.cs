using System.Collections;
using UnityEngine;

public class OpponentMovement : MonoBehaviour
{
    public int currentTileIndex = 0; // ������� ������� ����������
    public Transform[] tiles; // ������ �� ��� ������ �� ����
    public float moveSpeed = 5f; // �������� �����������
    public CameraController cameraController;
    public TurnManager turnManager; // ������ �� ������ ���������� ������������ �����

    public void TakeTurn()
    {
        cameraController.FocusOnOpponent();
        int diceRoll = Random.Range(1, 7);
        StartCoroutine(MoveOpponent(diceRoll));
    }

    private IEnumerator MoveOpponent(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            currentTileIndex++;
            if (currentTileIndex >= tiles.Length) currentTileIndex = tiles.Length - 1; // ����������� �����������
            Vector3 nextPos = tiles[currentTileIndex].position;
            while (transform.position != nextPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // ���������, �� ����� ������ ����� ���������
        CheckCurrentTile();
        yield return new WaitForSeconds(0.5f); // ��������� �������� ����� ������������ ������ �� ������
    }

    private void CheckCurrentTile()
    {
        GameObject currentTile = tiles[currentTileIndex].gameObject;
        if (currentTile.GetComponent<BonusTile>() != null)
        {
            currentTile.GetComponent<BonusTile>().ActivateBonus(gameObject);
        }
        else if (currentTile.GetComponent<PenaltyTile>() != null)
        {
            currentTile.GetComponent<PenaltyTile>().ActivatePenalty(gameObject);
        }
        else
        {
            // ��������� ���, ���� ��� ������� ��� �������
            turnManager.EndOpponentTurn();
            cameraController.FocusOnPlayer();
        }
    }

    public void MoveForward(int bonusSteps)
    {
        StartCoroutine(MoveOpponent(bonusSteps));
    }

    public void MoveBackward(int penaltySteps)
    {
        StartCoroutine(MoveOpponentBackward(penaltySteps));
    }

    private IEnumerator MoveOpponentBackward(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            currentTileIndex--;
            if (currentTileIndex < 0) currentTileIndex = 0; // ����������� ����������� �����
            Vector3 nextPos = tiles[currentTileIndex].position;
            while (transform.position != nextPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // ����� ����������� ����� ����� ��������� ������
        CheckCurrentTile();
    }

    public void MovePlayerForBonus(int steps)
    {
        int targetTileIndex = currentTileIndex + steps;

        // ��������, ��� ������ ������� ������ ��������� � �������� �������
        if (targetTileIndex >= tiles.Length)
        {
            targetTileIndex = tiles.Length - 1;
        }

        cameraController.StartZoomOut();  // ������ ����������, ����� ����� �������� ��������
        StartCoroutine(SmoothMoveToTile(targetTileIndex));
    }

    private IEnumerator SmoothMoveToTile(int targetTileIndex)
    {
        yield return new WaitForSeconds(1f);

        Vector3 targetPosition = tiles[targetTileIndex].position;

        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;  // ��������� ������� ������ �� ������ ������� ������
        currentTileIndex = targetTileIndex;   // ��������� ������� ������ ������
        CheckCurrentTile();  // ��������� ���
    }

    public void MovePlayerForPenalty(int steps)
    {
        int targetTileIndex = currentTileIndex - steps;

        // ��������, ��� ������ ������� ������ �� ������� �� ������� �������
        if (targetTileIndex < 0)
        {
            targetTileIndex = 0;  // ������������ ������ ������ �������
        }

        cameraController.StartZoomOut();  // ������ ����������, ����� ����� �������� ��������
        StartCoroutine(SmoothMoveToTilePen(targetTileIndex));
    }

    private IEnumerator SmoothMoveToTilePen(int targetTileIndex)
    {
        yield return new WaitForSeconds(1f);

        Vector3 targetPosition = tiles[targetTileIndex].position;

        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;  // ��������� ������� ������ �� ������ ������� ������
        currentTileIndex = targetTileIndex;   // ��������� ������� ������ ������
        CheckCurrentTile();  // ��������� ���
    }
}
