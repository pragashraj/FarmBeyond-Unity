using UnityEngine;
using UnityEngine.UI;

public class RobotPurchase : MonoBehaviour
{
    [SerializeField] private GameObject robotPurchaseUI;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject robotCountText;
    [SerializeField] private GameObject robotManagerObj;
    [SerializeField] private GameObject gameManagerObj;

    private int robotCount = 0;
    private int galleons = 0;

    private ThirdPersonOrbitCamBasic thirdPersonOrbitCam;
    private RobotManager robotManager;
    private GameManager gameManager;
    private UIManager uIManager;

    private void Start()
    {
        thirdPersonOrbitCam = cameraObj.GetComponent<ThirdPersonOrbitCamBasic>();
        robotManager = robotManagerObj.GetComponent<RobotManager>();
        gameManager = gameManagerObj.GetComponent<GameManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void Update()
    {
        SetRobotCountText(robotCount.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            HandleUIPopup(true, 0);
    }

    private void HandleUIPopup(bool active, int timeScale)
    {
        robotPurchaseUI.SetActive(active);
        Cursor.visible = active;
        Time.timeScale = timeScale;
        thirdPersonOrbitCam.enabled = !active;
    }

    public void CloseOnClick()
    {
        robotCount = 0;
        HandleUIPopup(false, 1);
    }

    public void HandleIncrease()
    {
        HandleRobotCount(1);
    }

    public void HandleDecrease()
    {
        HandleRobotCount(-1);
    }

    public void HandlePurchase()
    {
        if (robotCount != 0)
        {
            if (galleons <= gameManager.Galleons)
            {
                robotManager.IncreaseOrDecreaseRobotsTotal(robotCount);
                gameManager.Galleons -= galleons;
                CloseOnClick();
            }
            else
            {
                uIManager.SetWarnUI("You dont have enough galleons left");
            }
        }
        else
        {
            uIManager.SetWarnUI("Please select atleast one robot to purchase");
        }
    }

    private void HandleRobotCount(int count)
    {
        robotCount += count;
        galleons += count * 5;
    }

    private void SetRobotCountText(string text)
    {
        robotCountText.GetComponent<Text>().text = text;
    }
}
