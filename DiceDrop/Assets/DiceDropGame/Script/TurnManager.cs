using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnManager : MonoBehaviour
{
    public PlayerMovement playerMovement; // Ссылка на скрипт перемещения игрока
    public OpponentMovement opponentMovement; // Ссылка на скрипт перемещения противника
    public Button RollButtons; 
    public CameraController CameraControllers;

    private bool isPlayerTurn = true; // Переменная, определяющая, чей сейчас ход

    // Функция для начала хода игрока
    public void StartPlayerTurn()
    {
        CameraControllers.StopZoomOut();
        isPlayerTurn = true;
        // Здесь можно активировать UI-кнопки, позволяющие игроку бросить кубик
    }

    // Функция, вызываемая после того, как игрок закончит ход
    public void EndPlayerTurn()
    {
        CameraControllers.StopZoomOut();

        isPlayerTurn = false;
        StartCoroutine(StartOpponentTurnAfterDelay(0.5f)); // Небольшая задержка перед ходом противника
    }

    // Coroutine для запуска хода противника после задержки
    private IEnumerator StartOpponentTurnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        opponentMovement.TakeTurn();
    }

    // Функция, вызываемая после того, как противник закончит ход
    public void EndOpponentTurn()
    {
        StartPlayerTurn();
        RollButtons.gameObject.SetActive(true);
    }
}
