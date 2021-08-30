using UnityEngine;
using UnityEngine.UI;

public class BuilderManager : MonoBehaviour
{
    [SerializeField] private Text robotNumberUI;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject robotsSelecterUI;

    private int robotNumber = 0;

    private ThirdPersonOrbitCamBasic thirdPersonOrbitCam;
    private GameObject triggerObject;
    private BuildCampTrigger buildCampTrigger;
    private UIManager uIManager;
    private GameManager gameManager;

    private static string currentCampName;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        thirdPersonOrbitCam = cameraObj.GetComponent<ThirdPersonOrbitCamBasic>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        SetRobotCountText(robotNumber.ToString());
    }

    public void HandleCancelOnClick()
    {
        robotNumber = 0;
        robotsSelecterUI.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1;
        thirdPersonOrbitCam.enabled = true;
    }

    public void InCreaseNumber()
    {
        if (robotNumber < 2)
        {
            HandleRobotNumber(1);
        }
    }

    public void DeCreaseNumber()
    {
        if (robotNumber != 0)
            HandleRobotNumber(-1);
    }

    private void HandleRobotNumber(int no)
    {
        robotNumber += no;
    }

    public void setBuilderCampName(string name)
    {
        currentCampName = name;
    }


    public void HandleBuild()
    {
        triggerObject = GameObject.Find(currentCampName);
        buildCampTrigger = triggerObject.GetComponent<BuildCampTrigger>();
        if (robotNumber > 0)
        {
            int totalPurchasedRobots = gameManager.TotalRobots;
            int totalUsedRobots = gameManager.UsedRobots;

            HandleRobotManager(totalUsedRobots, totalPurchasedRobots);
        } else
        {
            uIManager.SetWarnUI("Please select atleast one robot to build");
        }
    }

    private void HandleRobotManager(int used, int total)
    {
        if (total != 0)
        {
            if (used < total)
            {
                gameManager.UsedRobots += robotNumber;
                buildCampTrigger.HandleRobotWorkers(robotNumber);
                HandleCancelOnClick();
            } else
            {
                uIManager.SetWarnUI("Your all robots are already in working, please try again later");
            }
        } else
        {
            uIManager.SetWarnUI("You haven't purchased any robots!");
        }
    }

    private void SetRobotCountText(string text)
    {
        robotNumberUI.GetComponent<Text>().text = text;
    }
}
