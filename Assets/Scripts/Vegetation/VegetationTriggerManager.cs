using UnityEngine;
using System.Collections;

public class VegetationTriggerManager : MonoBehaviour
{
    [SerializeField] private GameObject cornManagerObj;
    [SerializeField] private GameObject melonManagerObj;
    [SerializeField] private GameObject mushroomManagerObj;

    private CornManager cornManager;
    private MelonManager melonManager;
    private MushroomManager mushroomManager;

    private string currentObjectTag;

    private void Awake()
    {
        currentObjectTag = gameObject.tag;
    }

    private void Start()
    {
        switch (currentObjectTag)
        {
            case "Corn":
                cornManager = cornManagerObj.GetComponent<CornManager>();
                break;
            case "Melon":
                melonManager = melonManagerObj.GetComponent<MelonManager>();
                break;
            case "Mushroom":
                mushroomManager = mushroomManagerObj.GetComponent<MushroomManager>();
                break;
            default: return;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            HandleTriggerEnter();
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            HandleTriggerExit();
            HandleStay(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HandleStay(true);
        }
    }

    private void HandleTriggerEnter()
    {
        HandleTrigger(true);
    }

    private void HandleTriggerExit()
    {
        HandleTrigger(false);
    }

    private void HandleTrigger(bool active)
    {
        switch (currentObjectTag)
        {
            case "Corn":
                cornManager.Handler(active);
                break;
            case "Melon":
                melonManager.Handler(active);
                break;
            case "Mushroom":
                mushroomManager.Handler(active);
                break;
            default: return;
        }
    }

    private void HandleStay(bool active)
    {
        switch (currentObjectTag)
        {
            case "Corn":
                cornManager.Setstay(active);
                break;
            case "Melon":
                melonManager.Setstay(active);
                break;
            case "Mushroom":
                mushroomManager.Setstay(active);
                break;
            default: return;
        }
    }
}
