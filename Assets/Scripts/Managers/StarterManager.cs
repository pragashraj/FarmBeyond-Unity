using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StarterManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private Sprite mainMenu;
    [SerializeField] private Sprite mainMenuOptions;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        Cursor.visible = true;
        PlayAudio("Theme");
    }

    private void SetImage(Sprite image)
    {
        menuUI.GetComponent<Image>().sprite = image;
    }

    private void PlayAudio(string name)
    {
        FindObjectOfType<AudioManager>().Play(name);
    }

    public void HandleNewGame()
    {
        PlayAudio("Click");
        SetImage(mainMenu);
        gameManager.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        Application.Quit();
    }

    public void LoadGame()
    {
        PlayAudio("Click");
        SetImage(mainMenu);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
