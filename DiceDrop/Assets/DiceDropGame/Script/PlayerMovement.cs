using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int currentTileIndex = 0; // Текущая позиция игрока
    public Transform[] tiles; // Ссылки на все клетки на поле
    public float moveSpeed = 5f; // Скорость перемещения

    public TurnManager turnManager; // Ссылка на скрипт управления очередностью ходов
    public CameraController cameraController;

    public Button RollButtons;
    public GameObject Dice;

    public Animator anim;
    public Animator diceAnimator;

    private bool _itsWalk;
    private bool _itsJump = false;

    public int diceRoll;

    private void Start()
    {
        RollButtons.gameObject.SetActive(true);
    }
    private void Update()
    {
        anim.SetBool("Walk", _itsWalk);
        anim.SetBool("Jump", _itsJump);
    }
    public void RollDice()
    {
        Dice.gameObject.SetActive(true);
        diceRoll = Random.Range(1, 7);
        print(diceRoll);
        RollButtons.gameObject.SetActive(false);
        diceAnimator.SetTrigger("Drop");
        diceAnimator.SetTrigger("Roll_" + diceRoll);
    }

    public void MoversPlayer()
    {
        _itsWalk = true;
        cameraController.StartZoomOut();
        StartCoroutine(MovePlayer(diceRoll));
    }

    private IEnumerator MovePlayer(int steps)
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
        _itsWalk = false;
        // Проверяем, на какой клетке стоит игрок
        CheckCurrentTile();
        
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
            cameraController.StopZoomOut();
            turnManager.EndPlayerTurn();
        }
    }

    public void MoveForward(int bonusSteps)
    {
        StartCoroutine(MovePlayer(bonusSteps));
    }

    public void MoveBackward(int penaltySteps)
    {
        StartCoroutine(MovePlayerBackward(penaltySteps));
    }

    private IEnumerator MovePlayerBackward(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            _itsJump = true;
            currentTileIndex--;
            if (currentTileIndex < 0) currentTileIndex = 0; // Ограничение перемещения назад
            Vector3 nextPos = tiles[currentTileIndex].position;
            while (transform.position != nextPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            _itsJump = false;
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

        _itsJump = true;
        yield return new WaitForSeconds(1f);
        Vector3 targetPosition = tiles[targetTileIndex].position;

        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;  // Обновляем позицию игрока на точную позицию клетки
        currentTileIndex = targetTileIndex;   // Обновляем текущий индекс клетки
        _itsJump = false;
        cameraController.StopZoomOut();
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
        _itsJump = true;
        yield return new WaitForSeconds(1f); 
        Vector3 targetPosition = tiles[targetTileIndex].position;

        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;  // Обновляем позицию игрока на точную позицию клетки
        currentTileIndex = targetTileIndex;   // Обновляем текущий индекс клетки
        _itsJump = false;
        cameraController.StopZoomOut();
        CheckCurrentTile();  // Завершаем ход
    }

}
