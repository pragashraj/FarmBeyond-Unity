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
    [SerializeField] private GameObject robotManagerObj;

    private ThirdPersonOrbitCamBasic thirdPersonOrbitCam;
    private BuilderManager builderManager;
    private RobotManager robotManager;

    private bool onStay = false;
    private bool isBuilUiShowable = true;
    private int noOfRobots = 0;   

    private void Start()
    {
        thirdPersonOrbitCam = cameraObj.GetComponent<ThirdPersonOrbitCamBasic>();
        builderManager = builerManagerObj.GetComponent<BuilderManager>();
        robotManager = robotManagerObj.GetComponent<RobotManager>();
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
        if (!active) robotManager.IncreaseOrDecreaseRobotsUsed(-noOfRobots);
        StartCoroutine(OnBuilding());
    }

    IEnumerator OnBuilding()
    {
        yield return new WaitForSeconds(10);
        build.SetActive(true);
        SetRobots(noOfRobots, false);
        gameObject.SetActive(false);
    }
}
