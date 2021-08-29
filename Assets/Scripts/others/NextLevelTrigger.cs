using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private int expectedlevel;
    [SerializeField] private GameObject[] objectsToRemove;

    private UIManager uIManager;
    private GameManager gameManager;

    private void Awake()
    {
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();       
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
        int gameLevel = gameManager.GameLevel;
        if (gameLevel == expectedlevel)
        {
            HandleRemoveObjects();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int gameLevel = gameManager.GameLevel;
            if (gameLevel == expectedlevel)
            {
                HandleRemoveObjects();
            } else
            {
                uIManager.SetWarnUI("Next level not available right now!");
            }
        }
    }

    private void HandleRemoveObjects()
    {
        int len = objectsToRemove.Length;
        if (len > 0)
        {
            for (int i = 0; i < len; i++)
            {
                objectsToRemove[i].SetActive(false);
            }
        }
        gameObject.SetActive(false);
    }
}
