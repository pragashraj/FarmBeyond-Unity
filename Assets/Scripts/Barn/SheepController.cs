using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SheepController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject animalsUI;
    [SerializeField] private GameObject animalUI;
    [SerializeField] private GameObject collectUI;
    [SerializeField] private Sprite animalImage;
    [SerializeField] private Sprite collectImage;

    [Header("Objects")]
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject gameManagerObj;
    [SerializeField] private GameObject prefab;

    [Header("Inspector variables")]
    [SerializeField] private int cornsPerAnimal = 15;
    [SerializeField] private int galleonsPerAnimal = 15;
    [SerializeField] private int collectiblePerAnimal = 3;
    [SerializeField] private int maxAnimals = 2;
    [SerializeField] private Vector3[] positions;

    private bool onStay = false;
    private bool animalUIShown = false;
    private static bool isCollectible = false;
    private static bool isProcessing = false;

    private ThirdPersonOrbitCamBasic thirdPersonOrbitCam;
    private GameManager gameManager;
    private UIManager uIManager;

    private void Awake()
    {
        thirdPersonOrbitCam = cameraObj.GetComponent<ThirdPersonOrbitCamBasic>();
        gameManager = gameManagerObj.GetComponent<GameManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Start()
    {
        SetAnimalsUI(false);
        int count = gameManager.Sheeps;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                SetPrefabs(i);
            }
        }
    }

    void Update()
    {
        if (onStay)
        {
            HandleOnStay();
        }
    }

    private void SetPrefabs(int i)
    {
        GameObject instantiatedObject = Instantiate(prefab, positions[i], Quaternion.Euler(0f, i * 45f, 0f));
        int rand = Random.Range(0, 2);
        string anim = rand == 0 ? "idle1" : "idle2";
        instantiatedObject.GetComponent<Animation>().Play(anim);
        instantiatedObject.name = "Sheep " + i.ToString();
    }

    private void SetMainUI(bool active)
    {
        mainUI.SetActive(active);
    }

    private void SetAnimalsUI(bool active)
    {
        animalsUI.SetActive(active);
    }

    private void SetCameraController(bool active)
    {
        Cursor.visible = !active;
        Time.timeScale = active ? 1 : 0;
        thirdPersonOrbitCam.enabled = active;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animalUI.GetComponent<Image>().sprite = animalImage;
            collectUI.GetComponent<Image>().sprite = collectImage;
            SetMainUI(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onStay = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onStay = false;
            SetMainUI(false);
        }
    }

    private void HandleOnStay()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            HandleAnimalsOnClick();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            HandleFeedOnClick();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            HandleCollectOnClick();
        }
        else return;
    }

    private void HandleAnimalsOnClick()
    {
        bool active = !animalUIShown;
        SetAnimalsUI(active);
        SetMainUI(!active);
        SetCameraController(!active);
        animalUIShown = active;
    }

    private void HandleFeedOnClick()
    {
        int corns = gameManager.Corns;
        int sheeps = gameManager.Sheeps;
        if (sheeps > 0)
        {
            int totalCornsNeeded = sheeps * cornsPerAnimal;
            if (totalCornsNeeded <= corns)
            {
                gameManager.Corns -= totalCornsNeeded;
                isProcessing = true;
                isCollectible = false;
                uIManager.SetFeedUI();
                StartCoroutine(StartFeeding(10f));
            }
            else
            {
                uIManager.SetWarnUI("You dont have enough corns left");
            }
        }
        else
        {
            uIManager.SetWarnUI("You dont have sheeps to feed, purchase first");
        }
    }

    IEnumerator StartFeeding(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        isCollectible = true;
        isProcessing = false;
    }

    private void HandleCollectOnClick()
    {
        if (isCollectible)
        {
            SetMainUI(false);
            isCollectible = false;
            isProcessing = false;
            int total = collectiblePerAnimal * gameManager.Sheeps;
            gameManager.Cotton += total;
            uIManager.SetCollectedUI(collectImage);
        }
        else
        {
            if (isProcessing)
            {
                uIManager.SetWarnUI("Processing......");
            }
            else
            {
                uIManager.SetWarnUI("Currently no collectible found");
            }
        }
    }

    public void HandleAnimalPurchase()
    {
        int animals = gameManager.Sheeps;
        if (maxAnimals == animals)
        {
            uIManager.SetWarnUI("Sheeps limit exceeded");
        }
        else
        {
            int galleons = gameManager.Galleons;
            if (galleons == 0)
            {
                uIManager.SetWarnUI("You dont have enough galleons left");
            }
            else
            {
                if (galleonsPerAnimal < galleons)
                {
                    gameManager.Sheeps += 1;
                    gameManager.Galleons -= galleonsPerAnimal;
                    SetPrefabs(gameManager.Sheeps);
                }
                else
                {
                    uIManager.SetWarnUI("You dont have enough galleons left");
                }
            }
        }
    }
}
