using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject animalInventoryUI;

    [Header("Objects")]
    [SerializeField] private GameObject cameraObj;

    private ThirdPersonOrbitCamBasic thirdPersonOrbitCam;

    private bool isInventoryOpen = false;
    private bool isAnimalInventoryOpen = false;

    private void Start()
    {
        thirdPersonOrbitCam = cameraObj.GetComponent<ThirdPersonOrbitCamBasic>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            HandleInventoryUI(!isInventoryOpen);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            HandleAnimalInventoryUI(!isAnimalInventoryOpen);
        }
    }

    private void HandleInventoryUI(bool active)
    {
        isInventoryOpen = active;
        inventoryUI.SetActive(active);
        HandleCameraActive(active);
    }

    private void HandleAnimalInventoryUI(bool active)
    {
        isAnimalInventoryOpen = active;
        animalInventoryUI.SetActive(active);
        HandleCameraActive(active);
    }

    private void HandleCameraActive(bool active)
    {
        Cursor.visible = active;
        Time.timeScale = active ? 0 : 1;
        thirdPersonOrbitCam.enabled = !active;
    }

    public void HandleInventoryClose()
    {
        HandleInventoryUI(false);
    }

    public void HandleRightButtonOnClick()
    {

    }

    public void HandleLeftButtonOnClick()
    {

    }
}
