using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int currentTileIndex = 0; // ������� ������� ������
    public Transform[] tiles; // ������ �� ��� ������ �� ����
    public float moveSpeed = 5f; // �������� �����������

    public TurnManager turnManager; // ������ �� ������ ���������� ������������ �����
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
            if (currentTileIndex >= tiles.Length) currentTileIndex = tiles.Length - 1; // ����������� �����������
            Vector3 nextPos = tiles[currentTileIndex].position;
            while (transform.position != nextPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }
        _itsWalk = false;
        // ���������, �� ����� ������ ����� �����
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
            // ��������� ���, ���� ��� ������� ��� �������
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
            if (currentTileIndex < 0) currentTileIndex = 0; // ����������� ����������� �����
            Vector3 nextPos = tiles[currentTileIndex].position;
            while (transform.position != nextPos)
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            _itsJump = false;
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

        _itsJump = true;
        yield return new WaitForSeconds(1f);
        Vector3 targetPosition = tiles[targetTileIndex].position;

        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;  // ��������� ������� ������ �� ������ ������� ������
        currentTileIndex = targetTileIndex;   // ��������� ������� ������ ������
        _itsJump = false;
        cameraController.StopZoomOut();
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
        _itsJump = true;
        yield return new WaitForSeconds(1f); 
        Vector3 targetPosition = tiles[targetTileIndex].position;

        while (Vector2.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition;  // ��������� ������� ������ �� ������ ������� ������
        currentTileIndex = targetTileIndex;   // ��������� ������� ������ ������
        _itsJump = false;
        cameraController.StopZoomOut();
        CheckCurrentTile();  // ��������� ���
    }

}
