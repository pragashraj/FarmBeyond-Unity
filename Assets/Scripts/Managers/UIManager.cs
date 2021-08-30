using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Responsive")]
    [SerializeField] private RectTransform buildCampTriggerPopup;
    [SerializeField] private RectTransform feedCornPopup;
    [SerializeField] private RectTransform processingPopup;
    [SerializeField] private RectTransform cropPopup;
    [SerializeField] private RectTransform robotPurchaseUI;
    [SerializeField] private RectTransform robotPurchaseButton;
    [SerializeField] private RectTransform robotPurchaseCloseButton;
    [SerializeField] private RectTransform robotPurchaseIncreaseButton;
    [SerializeField] private RectTransform robotPurchaseDecreaseButton;
    [SerializeField] private RectTransform inventoryUI;

    [Header("Text")]
    [SerializeField] private Text chickenCountText;
    [SerializeField] private Text pigCountText;
    [SerializeField] private Text cowCountText;
    [SerializeField] private Text goatCountText;
    [SerializeField] private Text sheepCountText;
    [SerializeField] private Text galleonsText;
    [SerializeField] private GameObject robotPurchaseCount;
    [SerializeField] private Text minesText;
    [SerializeField] private Text fishText;
    [SerializeField] private Text woodText;
    [SerializeField] private Text cornText;
    [SerializeField] private Text melonText;
    [SerializeField] private Text mushroomText;
    [SerializeField] private Text appleText;
    [SerializeField] private Text orangeText;
    [SerializeField] private Text warnMessageText;
    [SerializeField] private Text robotCountText;
    [SerializeField] private Text usedRobotCountText;
    [SerializeField] private Text eggsText;
    [SerializeField] private Text pigMeatText;
    [SerializeField] private Text cowMilkText;
    [SerializeField] private Text goatMilkText;
    [SerializeField] private Text cottonText;
    [SerializeField] private Text balanceText;
    [SerializeField] private Text goldToNextText;
    [SerializeField] private Text goldCurrent;

    [Header("Objects")]
    [SerializeField] private GameObject gameManagerObj;
    [SerializeField] private GameObject warnUI;
    [SerializeField] private GameObject collectedUI;
    [SerializeField] private GameObject feedUI;

    private GameManager gameManager;

    private string warnMessage;

    private void Awake()
    {
        gameManager = gameManagerObj.GetComponent<GameManager>();
    }

    void Update()
    {
        HandleUISizing();
        HandleUIValues();
    }

    private void HandleUIValues()
    {
        chickenCountText.text = gameManager.Chickens.ToString();
        pigCountText.text = gameManager.Pigs.ToString();
        cowCountText.text = gameManager.Cows.ToString();
        goatCountText.text = gameManager.Goats.ToString();
        sheepCountText.text = gameManager.Sheeps.ToString();
        galleonsText.text = gameManager.Galleons.ToString();
        fishText.text = gameManager.Fishes.ToString();
        minesText.text = gameManager.Mines.ToString();
        woodText.text = gameManager.Woods.ToString();
        cornText.text = gameManager.Corns.ToString();
        melonText.text = gameManager.Melons.ToString();
        mushroomText.text = gameManager.Mushrooms.ToString();
        appleText.text = gameManager.Apples.ToString();
        orangeText.text = gameManager.Oranges.ToString();
        warnMessageText.text = warnMessage;
        robotCountText.text = gameManager.TotalRobots.ToString();
        usedRobotCountText.text = gameManager.UsedRobots.ToString();
        eggsText.text = gameManager.Eggs.ToString();
        pigMeatText.text = gameManager.PigMeats.ToString();
        cowMilkText.text = gameManager.CowMilks.ToString();
        goatMilkText.text = gameManager.GoatMilks.ToString();
        cottonText.text = gameManager.Cotton.ToString();
        balanceText.text = gameManager.Galleons.ToString();
        goldToNextText.text = gameManager.GoldToNextLevel.ToString();
        goldCurrent.text = gameManager.GoldCurrent.ToString();
    }

    private void HandleUISizing()
    {
        buildCampTriggerPopup.sizeDelta = getUIResolutionVector(120.0f, 120.0f);
        feedCornPopup.sizeDelta = getUIResolutionVector(120.0f, 120.0f);
        processingPopup.sizeDelta = getUIResolutionVector(120.0f, 120.0f);
        cropPopup.sizeDelta = getUIResolutionVector(120.0f, 120.0f);
        robotPurchaseUI.sizeDelta = getUIResolutionVector(541.0f, 764.0f);
        robotPurchaseButton.sizeDelta = getUIResolutionVector(180.0f, 100.0f);
        robotPurchaseCloseButton.sizeDelta = getUIResolutionVector(60.0f, 60.0f);
        robotPurchaseIncreaseButton.sizeDelta = getUIResolutionVector(40.0f, 40.0f);
        robotPurchaseDecreaseButton.sizeDelta = getUIResolutionVector(40.0f, 40.0f);
        robotPurchaseCount.GetComponent<RectTransform>().sizeDelta = getUIResolutionVector(90.0f, 60.0f);
        robotPurchaseCount.GetComponent<Text>().fontSize = (int)getTextSize(50.0f);
        inventoryUI.sizeDelta = getUIResolutionVector(1350.0f, 900.0f);
    }

    private Vector2 getUIResolutionVector(float wv, float hv)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float width = wv / 1920 * screenWidth;
        float height = hv / 1080 * screenHeight;

        return new Vector2(width, height);
    }

    private float getTextSize(float size)
    {
        float screenWidth = Screen.width;
        return size / 1920 * screenWidth;
    }

    public void SetWarnUI(string message)
    {
        warnUI.SetActive(true);
        warnMessage = message;
        Cursor.visible = true;
    }

    public void CloseWarnUI()
    {
        warnMessage = null;
        warnUI.SetActive(false);
        Cursor.visible = false;
    }

    public void SetCollectedUI(Sprite image)
    {
        collectedUI.GetComponent<Image>().sprite = image;
        collectedUI.SetActive(true);
        collectedUI.GetComponent<Animator>().enabled = true;
        StartCoroutine(CollectAnim(3));
    }

    IEnumerator CollectAnim(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        collectedUI.SetActive(false);
        collectedUI.GetComponent<Animator>().enabled = false;
    }

    public void SetFeedUI()
    {
        feedUI.SetActive(true);
        feedUI.GetComponent<Animator>().enabled = true;
        StartCoroutine(FeedAnim(2));
    }

    IEnumerator FeedAnim(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        feedUI.SetActive(false);
        feedUI.GetComponent<Animator>().enabled = false;
    }
}
