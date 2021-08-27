using System.Collections;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    private enum State
    {
        EMPTY, GROWING, GROWN
    }

    [SerializeField] private GameObject wateringUI;
    [SerializeField] private GameObject cropUI;
    [SerializeField] private GameObject processingUI;
    [SerializeField] private GameObject fruits;
    [SerializeField] private GameObject gameManagerObj;

    private bool onStay;
    private State state = State.EMPTY;
    private string currentObjectTag = "APPLE";

    private GameManager gameManager;

    void Start()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();
        HandleFruitsObj(state != State.EMPTY);
        GetCurrentObject();
    }

    void Update()
    {
        if(onStay && Input.GetKeyDown(KeyCode.P))
        {
            PerformAction();
        }
    }

    private void GetCurrentObject()
    {
        string tag = gameObject.tag;
        if (tag == "Apple")
        {
            currentObjectTag = "APPLE";
        }
        else if (tag == "Orange")
        {
            currentObjectTag = "ORANGE";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onStay = true;
            HandleOnTriggerStay(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onStay = false;
            HandleFeedUI(false);
            HandleGrowingUI(false);
            HandleGrownUI(false);
        }
    }

    private void HandleOnTriggerStay(bool active)
    {
        switch(state)
        {
            case State.EMPTY: HandleFeedUI(active);
                break;
            case State.GROWING: HandleGrowingUI(active);
                break;
            case State.GROWN: HandleGrownUI(active);
                break;
            default: return;
        }
    }

    private void PerformAction()
    {
        switch (state)
        {
            case State.EMPTY: StartCoroutine(HandleGrowing());
                break;
            case State.GROWN: HandleCropping();
                break;
            default: return;
        }
    }

    private void HandleFeedUI(bool active)
    {
        wateringUI.SetActive(active);
    }

    private void HandleGrowingUI(bool active)
    {
        processingUI.SetActive(active);
    }

    private void HandleFruitsObj(bool active)
    {
        fruits.SetActive(active);
    }

    private void HandleGrownUI(bool active)
    {
        cropUI.SetActive(active);
    }

    IEnumerator HandleGrowing()
    {
        HandleFeedUI(false);
        state = State.GROWING;
        yield return new WaitForSeconds(15);
        HandleFruitsObj(true);
        HandleGrowingUI(false);
        state = State.GROWN;
    }

    private void HandleCropping()
    {
        HandleFruitsObj(false);
        HandleGrownUI(false);
        HandleGrowingUI(false);
        state = State.EMPTY;
        if (currentObjectTag == "APPLE")
        {
            gameManager.Apples += 5;
        }
        if (currentObjectTag == "ORANGE")
        {
            gameManager.Oranges += 5;
        }
    }
}
