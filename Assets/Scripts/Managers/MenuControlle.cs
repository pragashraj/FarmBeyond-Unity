using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControlle : MonoBehaviour
{
    [SerializeField] private GameObject menuObj;
    [SerializeField] private GameObject menuUI;
    [SerializeField] private Sprite mainMenu;
    [SerializeField] private Sprite mainMenuOptions;

    private ThirdPersonCharacterControl thirdPersonCharacterControl;
    private ThirdPersonOrbitCamBasic thirdPersonOrbitCam;

    private bool showMenu = false;

    private void Awake()
    {
        thirdPersonOrbitCam = GameObject.Find("Main Camera").GetComponent<ThirdPersonOrbitCamBasic>();
        thirdPersonCharacterControl = GameObject.Find("CH").GetComponent<ThirdPersonCharacterControl>();
    }

    void Start()
    {
        SetImage(mainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            handleScripts();
        }
    }

    private void SetImage(Sprite image)
    {
        menuUI.GetComponent<Image>().sprite = image;
    }

    private void handleScripts()
    {
        showMenu = !showMenu;
        Cursor.visible = showMenu;
        menuObj.SetActive(showMenu);
        thirdPersonOrbitCam.enabled = !showMenu;
        thirdPersonCharacterControl.enabled = !showMenu;
    }

    public void HandleContinue()
    {
        SetImage(mainMenu);
        handleScripts();
    }

    public void HandleNewGame()
    {
        SetImage(mainMenu);
        handleScripts();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleOptionsOnClick()
    {
        SetImage(mainMenuOptions);
    }

    public void HandleQuit()
    {
        SetImage(mainMenu);
        Application.Quit();
    }

    public void HandleRestart()
    {
        SetImage(mainMenu);
    }
}
