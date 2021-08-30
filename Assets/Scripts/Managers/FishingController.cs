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
    private UIManager uIManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        int totalRobots = gameManager.TotalRobots;
        if (totalRobots != 0)
        {
            int availableRobots = totalRobots - gameManager.UsedRobots;
            int fishersLength = fishers.Length;

            if (fishersLength <= totalRobots && fishersLength <= availableRobots)
            {
                SetTriggerUI(false);
                SetProgressUI(true);
                SetWorkers(true);
                state = State.FISHING;
                gameManager.UsedRobots += fishersLength;
                StartCoroutine(Fishing());
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


    IEnumerator Fishing()
    {
        yield return new WaitForSecondsRealtime(15);
        state = State.COLLECTED;
        SetWorkers(false);
        SetProgressUI(false);
        SetCollectUI(onStay);
        gameManager.UsedRobots -= fishers.Length;
    }
}
