using UnityEngine;

public class VegetationController : MonoBehaviour
{
    [SerializeField] private GameObject[] builders;
    [SerializeField] private GameObject[] buildCamps;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        for (int i = 0; i < builders.Length; i++)
        {
            GameObject builder = builders[i];
            switch(builder.gameObject.name)
            {
                case "Corn01": 
                    builder.SetActive(gameManager.CornBuilder1);
                    break;
                case "Corn02": 
                    builder.SetActive(gameManager.CornBuilder2);
                    break;
                case "Melon01":
                    builder.SetActive(gameManager.MelonBuilder1);
                    break;
                case "Melon02":
                    builder.SetActive(gameManager.MelonBuilder2);
                    break;
                case "Mushroom01":
                    builder.SetActive(gameManager.MushroomBuilder1);
                    break;
                case "Mushroom02":
                    builder.SetActive(gameManager.MushroomBuilder2);
                    break;
                default: return;
            }
        }

        for (int i = 0; i < buildCamps.Length; i++)
        {
            GameObject buildCamp = buildCamps[i];
            switch (buildCamp.gameObject.name)
            {
                case "Corn-BuildingCamp01":
                    buildCamp.SetActive(!gameManager.CornBuilder1);
                    break;
                case "Corn-BuildingCamp02":
                    buildCamp.SetActive(!gameManager.CornBuilder2);
                    break;
                case "Melon-BuildingCamp01":
                    buildCamp.SetActive(!gameManager.MelonBuilder1);
                    break;
                case "Melon-BuildingCamp02":
                    buildCamp.SetActive(!gameManager.MelonBuilder2);
                    break;
                case "Mushroom-BuildingCamp01":
                    buildCamp.SetActive(!gameManager.MushroomBuilder1);
                    break;
                case "Mushroom-BuildingCamp02":
                    buildCamp.SetActive(!gameManager.MushroomBuilder2);
                    break;
                default: return;
            }
        }
    }
}
