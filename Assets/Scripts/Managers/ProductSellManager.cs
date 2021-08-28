using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductSellManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject triggerUI;
    [SerializeField] private GameObject selectedProp;
    [SerializeField] private GameObject selectedPropTemp;
    [SerializeField] private Text total;
    [SerializeField] private InputField countInput;

    [Header("Objects")]
    [SerializeField] private GameObject cameraObj;

    [Header("Galleons per selected")]
    [SerializeField] private string[] selections;
    [SerializeField] private int[] galleons;

    private ThirdPersonOrbitCamBasic thirdPersonOrbitCam;
    private GameManager gameManager;
    private UIManager uIManager;

    private int countValue;
    private string selectedPropName;
    private int totalProps;
    private bool onStay;

    Dictionary<string, string> galleonsPerSelected = new Dictionary<string, string>();

    private void Awake()
    {
        thirdPersonOrbitCam = cameraObj.GetComponent<ThirdPersonOrbitCamBasic>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        uIManager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    void Start()
    {
        countInput.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        selectedPropName = "corn";
        for (int i = 0; i < selections.Length; i++)
        {
            galleonsPerSelected.Add(selections[i], galleons[i].ToString());
        }
    }

    void Update()
    {
        int total = 0;
        switch(selectedPropName)
        {
            case "corn": total = gameManager.Corns;
                break;
            case "melon": total = gameManager.Melons;
                break;
            case "mushroom": total = gameManager.Mushrooms;
                break;
            case "apple": total = gameManager.Apples;
                break;
            case "orange": total = gameManager.Oranges;
                break;
            case "fish": total = gameManager.Fishes;
                break;
            case "eggs": total = gameManager.Eggs;
                break;
            case "pigMeat": total = gameManager.PigMeats;
                break;
            case "cowMilk": total = gameManager.CowMilks;
                break;
            case "goatMilk": total = gameManager.GoatMilks;
                break;
            case "cotton": total = gameManager.Cotton;
                break;
            case "mine": total = gameManager.Mines;
                break;
            case "wood": total = gameManager.Woods;
                break;
            default: return;
        }
        totalProps = total;
        SetTotalValue(total);

        if (onStay && Input.GetKeyDown(KeyCode.P))
        {
            SetMainUI(true);
            HandleCursorActive(false);
        }
    }

    private void SetTotalValue(int value)
    {
        total.text = value.ToString();
    }

    private void ValueChangeCheck()
    {
        try
        {
            countValue = int.Parse(countInput.text);
        }catch(Exception e)
        {
            Debug.Log(e);
            countValue = 0;
        }
    }

    private void SetMainUI(bool active)
    {
        mainUI.SetActive(active);
    }

    private void SetTriggerUI(bool active)
    {
        triggerUI.SetActive(active);
    }

    private void HandleCursorActive(bool active)
    {
        Cursor.visible = !active;
        Time.timeScale = !active ? 0 : 1;
        thirdPersonOrbitCam.enabled = active;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
           
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            onStay = true;
            SetTriggerUI(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SetTriggerUI(false);
            HandleCursorActive(true);
            onStay = false;
        }
    }

    public void HandleClose()
    {
        SetMainUI(false);
        HandleCursorActive(true);
    }

    public void HandleDailySpin()
    {
        uIManager.SetWarnUI("Feature not available right now!");
    }

    public void HandleSell()
    {
        if (countValue > 0)
        {
            if (countValue <= totalProps)
            {
                int total = int.Parse(galleonsPerSelected[selectedPropName]) * countValue;
                switch (selectedPropName)
                {
                    case "corn": gameManager.Corns -= countValue;
                        break;
                    case "melon": gameManager.Melons -= countValue;
                        break;
                    case "mushroom": gameManager.Mushrooms -= countValue;
                        break;
                    case "apple": gameManager.Apples -= countValue;
                        break;
                    case "orange": gameManager.Oranges -= countValue;
                        break;
                    case "fish": gameManager.Fishes -= countValue;
                        break;
                    case "eggs": gameManager.Eggs -= countValue;
                        break;
                    case "pigMeat": gameManager.PigMeats -= countValue;
                        break;
                    case "cowMilk": gameManager.CowMilks -= countValue;
                        break;
                    case "goatMilk": gameManager.GoatMilks -= countValue;
                        break;
                    case "cotton": gameManager.Cotton -= countValue;
                        break;
                    case "mine": gameManager.Mines -= countValue;
                        break;
                    case "wood": gameManager.Woods -= countValue;
                        break;
                    default: return;
                }
                gameManager.Galleons += total;
            } else
            {
                uIManager.SetWarnUI("Count cannot be greater than total");
            }
        } else
        {
            uIManager.SetWarnUI("Please enter valid values to sell");
        }
    }

    public void PropOnClick(Sprite image)
    {
        selectedProp.GetComponent<Image>().sprite = image;
        selectedPropTemp.GetComponent<Image>().sprite = image;
        selectedPropName = image.name;
    }
}
