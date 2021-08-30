using UnityEngine;
using System.Collections;

public class BuildCampTrigger : MonoBehaviour
{
    [SerializeField] private GameObject buildCampUI;
    [SerializeField] private GameObject robotsSelecterUI;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject builerManagerObj;
    [SerializeField] private GameObject[] robot_workers;
    [SerializeField] private GameObject build;

    private ThirdPersonOrbitCamBasic thirdPersonOrbitCam;
    private BuilderManager builderManager;
    private GameManager gameManager;

    private bool onStay = false;
    private bool isBuilUiShowable = true;
    private int noOfRobots = 0;   

    private void Start()
    {
        thirdPersonOrbitCam = cameraObj.GetComponent<ThirdPersonOrbitCamBasic>();
        builderManager = builerManagerObj.GetComponent<BuilderManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (onStay && Input.GetKeyDown(KeyCode.P))
            HandleBuildCamp();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && isBuilUiShowable)
        {
            HandleBuildUIPopup(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isBuilUiShowable)
        {
            HandleBuildUIPopup(false);
            onStay = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onStay = true;
        }
    }

    private void HandleBuildCamp()
    {
        HandleBuildUIPopup(false);
        HandleRobotUIPopup(true);
    }

    private void HandleBuildUIPopup(bool value)
    {
        buildCampUI.SetActive(value);
    }

    private void HandleRobotUIPopup(bool value)
    {
        robotsSelecterUI.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0;
        thirdPersonOrbitCam.enabled = false;
        builderManager.setBuilderCampName(gameObject.name);
    }

    public void HandleRobotWorkers(int count)
    {
        noOfRobots = count;
        SetRobots(count, true);
    }

    private void SetRobots(int count, bool active)
    {
        isBuilUiShowable = false;
        for (int i = 0; i < count; i++)
        {
            robot_workers[i].SetActive(active);
        }
        if (!active)
        {
            gameManager.UsedRobots -= noOfRobots;
        }
        StartCoroutine(OnBuilding());
    }

    IEnumerator OnBuilding()
    {
        yield return new WaitForSeconds(10);
        build.SetActive(true);
        SetRobots(noOfRobots, false);
        gameObject.SetActive(false);
        SetGameManager();
    }

    private void SetGameManager()
    {
        switch (build.gameObject.name)
        {
            case "Corn01":
                gameManager.CornBuilder1 = true;
                break;
            case "Corn02":
                gameManager.CornBuilder2 = true;
                break;
            case "Melon01":
                gameManager.MelonBuilder1 = true;
                break;
            case "Melon02":
                gameManager.MelonBuilder2 = true;
                break;
            case "Mushroom01":
                gameManager.MushroomBuilder1 = true;
                break;
            case "Mushroom02":
                gameManager.MushroomBuilder2 = true;
                break;
            default: return;
        }
    }
}
