using System.Collections;
using UnityEngine;

public class MineController : MonoBehaviour
{
    private enum State
    {
        EMPTY, MINING, MINED
    }

    [Header("UI")]
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private GameObject processingUI;
    [SerializeField] private GameObject collectUI;

    [Header("Workers")]
    [SerializeField] private GameObject[] miners;

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
                case State.MINED: HandleCollect();
                    break;
                default: return;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onStay = true;
            switch (state)
            {
                case State.EMPTY: SetTriggerUI(true);
                    break;
                case State.MINING: SetProgressUI(true);
                    break;
                case State.MINED: SetCollectUI(true);
                    break;
                default: return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
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
        int minersLength = miners.Length;
        for (int i = 0; i < minersLength; i++)
        {
            miners[i].SetActive(active);
        }
    }

    private void HandleOnClick()
    {
        int totalRobots = robotManager.GetTotalRobots;
        if (totalRobots != 0)
        {
            int availableRobots = totalRobots - robotManager.GetUsedRobots;
            int minersLength = miners.Length;

            if (minersLength <= totalRobots && minersLength <= availableRobots)
            {
                SetTriggerUI(false);
                SetProgressUI(true);
                SetWorkers(true);
                state = State.MINING;
                robotManager.IncreaseOrDecreaseRobotsUsed(minersLength);
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
        gameManager.Mines = gameManager.Mines + 5;
    }


    IEnumerator Mining()
    {
        yield return new WaitForSecondsRealtime(15);
        state = State.MINED;
        SetWorkers(false);
        SetProgressUI(false);
        SetTriggerUI(onStay);
    }

}
