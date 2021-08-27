using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomManager : MonoBehaviour
{
    [SerializeField] private GameObject feedingUI;
    [SerializeField] private GameObject cropUI;
    [SerializeField] private GameObject progressingUI;
    [SerializeField] private GameObject mushrooms;
    [SerializeField] private GameObject[] seeders;
    [SerializeField] private GameObject robotManagerObj;
    [SerializeField] private GameObject gameManagerObj;

    private RobotManager robotManager;
    private GameManager gameManager;

    private enum State
    {
        BUILDER_EMPTY, GROWING, GROWN
    }

    private bool onStay = false;
    private State state = State.BUILDER_EMPTY;

    private void Start()
    {
        robotManager = robotManagerObj.GetComponent<RobotManager>();
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    private void Update()
    {
        if (onStay && Input.GetKeyDown(KeyCode.P))
        {
            switch (state)
            {
                case State.BUILDER_EMPTY: HandleGrow();
                    break;
                case State.GROWN: HandleCollect();
                    break;
                default: return;
            }
        }

        if (onStay && state == State.BUILDER_EMPTY)
        {
            HandleFeedingUI(true);
        }
    }

    public void Setstay(bool stay)
    {
        onStay = stay;
    }

    public void Handler(bool uiActive)
    {
        switch (state)
        {
            case State.BUILDER_EMPTY: HandleFeedingUI(uiActive);
                break;
            case State.GROWING: HandleGrowingUI(uiActive);
                break;
            case State.GROWN: HandleCollectUI(uiActive);
                break;
            default: return;
        }
    }

    private void HandleGrow()
    {
        HandleFeedingUI(false);
        StartGrowing();
    }

    private void HandleCollect()
    {
        HandleCollectUI(false);
        mushrooms.SetActive(false);
        gameManager.Mushrooms += 10;
        state = State.BUILDER_EMPTY;
    }

    private void HandleFeedingUI(bool active)
    {
        feedingUI.SetActive(active);
    }

    private void HandleGrowingUI(bool active)
    {
        progressingUI.SetActive(active);
    }

    private void HandleCollectUI(bool active)
    {
        cropUI.SetActive(active);
    }

    private void StartGrowing()
    {
        if (robotManager.GetTotalRobots - robotManager.GetUsedRobots > 0)
        {
            HandleSeeders(true);
            robotManager.IncreaseOrDecreaseRobotsUsed(2);
            state = State.GROWING;
            StartCoroutine(GrowingEnd(6));
        }
    }

    IEnumerator GrowingEnd(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        mushrooms.SetActive(true);
        state = State.GROWN;
        HandleSeeders(false);
        robotManager.IncreaseOrDecreaseRobotsUsed(-2);
        HandleGrowingUI(false);
    }

    private void HandleSeeders(bool active)
    {
        for (int i = 0; i < seeders.Length; i++)
        {
            seeders[i].SetActive(active);
        }
    }
}
