using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnManager : MonoBehaviour
{
    public PlayerMovement playerMovement; // ������ �� ������ ����������� ������
    public OpponentMovement opponentMovement; // ������ �� ������ ����������� ����������
    public Button RollButtons; 
    public CameraController CameraControllers;

    private bool isPlayerTurn = true; // ����������, ������������, ��� ������ ���

    // ������� ��� ������ ���� ������
    public void StartPlayerTurn()
    {
        CameraControllers.StopZoomOut();
        isPlayerTurn = true;
        // ����� ����� ������������ UI-������, ����������� ������ ������� �����
    }

    // �������, ���������� ����� ����, ��� ����� �������� ���
    public void EndPlayerTurn()
    {
        CameraControllers.StopZoomOut();

        isPlayerTurn = false;
        StartCoroutine(StartOpponentTurnAfterDelay(0.5f)); // ��������� �������� ����� ����� ����������
    }

    // Coroutine ��� ������� ���� ���������� ����� ��������
    private IEnumerator StartOpponentTurnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        opponentMovement.TakeTurn();
    }

    // �������, ���������� ����� ����, ��� ��������� �������� ���
    public void EndOpponentTurn()
    {
        StartPlayerTurn();
        RollButtons.gameObject.SetActive(true);
    }
}
