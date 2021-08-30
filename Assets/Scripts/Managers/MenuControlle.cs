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
    private GameManager gameManager;

    private bool showMenu = false;

    private void Awake()
    {
        thirdPersonOrbitCam = GameObject.Find("Main Camera").GetComponent<ThirdPersonOrbitCamBasic>();
        thirdPersonCharacterControl = GameObject.Find("CH").GetComponent<ThirdPersonCharacterControl>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

    private void PlayAudio(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }

    private void handleScripts()
    {
        showMenu = !showMenu;
        Cursor.visible = showMenu;
        menuObj.SetActive(showMenu);
        thirdPersonOrbitCam.enabled = !showMenu;
        thirdPersonCharacterControl.enabled = !showMenu;

        if (showMenu)
        {
            PlayAudio("Swipe");
        }
    }

    public void HandleContinue()
    {
        PlayAudio("Click");
        SetImage(mainMenu);
        handleScripts();
    }

    public void HandleNewGame()
    {
        PlayAudio("Click");
        SetImage(mainMenu);
        handleScripts();
        gameManager.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void HandleOptionsOnClick()
    {
        PlayAudio("Click");
        SetImage(mainMenuOptions);
    }

    public void HandleQuit()
    {
        PlayAudio("Click");
        SetImage(mainMenu);
        gameManager.SaveGame();
        Application.Quit();
    }

    public void HandleRestart()
    {
        PlayAudio("Click");
        SetImage(mainMenu);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
