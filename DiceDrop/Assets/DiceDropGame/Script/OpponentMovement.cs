using System.Collections;
using UnityEngine;

public class OpponentMovement : MonoBehaviour
{
    public int currentTileIndex = 0; // Текущая позиция противника
    public Transform[] tiles; // Ссылки на все клетки на поле
    public float moveSpeed = 5f; // Скорость перемещения
    public CameraController cameraController;
    public TurnManager turnManager; // Ссылка на скрипт управления очередностью ходов

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
            if (currentTileIndex >= tiles.Length) currentTileIndex = tiles.Length - 1; // Ограничение перемещения
            Vector3 nextPos = tiles[currentTileIndex].position;
            while (transform.position != nextPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // Проверяем, на какой клетке стоит противник
        CheckCurrentTile();
        yield return new WaitForSeconds(0.5f); // Небольшая задержка перед возвращением камеры на игрока
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
            // Завершаем ход, если нет бонусов или штрафов
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
            if (currentTileIndex < 0) currentTileIndex = 0; // Ограничение перемещения назад
            Vector3 nextPos = tiles[currentTileIndex].position;
            while (transform.position != nextPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        // После перемещения назад снова проверяем клетку
        CheckCurrentTile();
    }

    public void MovePlayerForBonus(int steps)
    {
        int targetTileIndex = currentTileIndex + steps;

        // Убедимся, что индекс целевой клетки находится в пределах массива
        if (targetTileIndex >= tiles.Length)
        {
            targetTileIndex = tiles.Length - 1;
        }

        cameraController.StartZoomOut();  // Камера отдаляется, когда игрок начинает движение
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

        transform.position = targetPosition;  // Обновляем позицию игрока на точную позицию клетки
        currentTileIndex = targetTileIndex;   // Обновляем текущий индекс клетки
        CheckCurrentTile();  // Завершаем ход
    }

    public void MovePlayerForPenalty(int steps)
    {
        int targetTileIndex = currentTileIndex - steps;

        // Убедимся, что индекс целевой клетки не выходит за пределы массива
        if (targetTileIndex < 0)
        {
            targetTileIndex = 0;  // Ограничиваем индекс первой клеткой
        }

        cameraController.StartZoomOut();  // Камера отдаляется, когда игрок начинает движение
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

        transform.position = targetPosition;  // Обновляем позицию игрока на точную позицию клетки
        currentTileIndex = targetTileIndex;   // Обновляем текущий индекс клетки
        CheckCurrentTile();  // Завершаем ход
    }
}
