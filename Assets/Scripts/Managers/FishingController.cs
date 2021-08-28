using System.Collections;
using UnityEngine;

public class FishingController : MonoBehaviour
{
    private enum State
    {
        EMPTY, FISHING, COLLECTED
    }

    [Header("UI")]
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private GameObject processingUI;
    [SerializeField] private GameObject collectUI;

    [Header("Workers")]
    [SerializeField] private GameObject[] fishers;

    private bool onStay = false;
    private State state = State.EMPTY;

    private GameManager gameManager;
    private RobotManager robotManager;
    private UIManager uIManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        robotManager = GameObject.Find("RobotManager").GetComponent<RobotManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        if (onStay && Input.GetKeyDown(KeyCode.P))
        {
            switch (state)
            {
                case State.EMPTY: HandleOnClick();
                    break;
                case State.COLLECTED: HandleCollect();
                    break;
                default: return;
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            onStay = true;
            switch (state)
            {
                case State.EMPTY: SetTriggerUI(true);
                    break;
                case State.FISHING: SetProgressUI(true);
                    break;
                case State.COLLECTED: SetCollectUI(true);
                    break;
                default: return;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            onStay = false;
            SetTriggerUI(false);
            SetProgressUI(false);
            SetCollectUI(false);
        }
    }

    private void SetTriggerUI(bool active)
    {
        triggerUI.SetActive(active);
    }

    private void SetProgressUI(bool active)
    {
        processingUI.SetActive(active);
    }

    private void SetCollectUI(bool active)
    {
        collectUI.SetActive(active);
    }

    private void SetWorkers(bool active)
    {
        int minersLength = fishers.Length;
        for (int i = 0; i < minersLength; i++)
        {
            fishers[i].SetActive(active);
        }
    }

    private void HandleOnClick()
    {
        int totalRobots = robotManager.GetTotalRobots;
        if (totalRobots != 0)
        {
            int availableRobots = totalRobots - robotManager.GetUsedRobots;
            int fishersLength = fishers.Length;

            if (fishersLength <= totalRobots && fishersLength <= availableRobots)
            {
                SetTriggerUI(false);
                SetProgressUI(true);
                SetWorkers(true);
                state = State.FISHING;
                robotManager.IncreaseOrDecreaseRobotsUsed(fishersLength);
                StartCoroutine(Mining());
            }
            else
            {
                uIManager.SetWarnUI("Robots in use, please try again later");
            }
        } else
        {
            uIManager.SetWarnUI("Currently no robots purchased");
        }
    }

    private void HandleCollect()
    {
        state = State.EMPTY;
        SetCollectUI(false);
        SetTriggerUI(true);
        gameManager.Fishes = gameManager.Fishes + 5;
    }


    IEnumerator Mining()
    {
        yield return new WaitForSecondsRealtime(15);
        state = State.COLLECTED;
        SetWorkers(false);
        SetProgressUI(false);
        SetCollectUI(onStay);
    }
}
