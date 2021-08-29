using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int galleons;
    private static int goldToNextLevel;
    private static int goldCurrent;

    private static int corns;
    private static int melons;
    private static int mushrooms;
    private static int apples;
    private static int oranges;

    private static int chickens;
    private static int pigs;
    private static int cows;
    private static int goats;
    private static int sheeps;

    private static int mines;
    private static int fishes;
    private static int woods;
    private static int eggs;
    private static int pigMeats;
    private static int cowMilks;
    private static int goatMilks;
    private static int cotton;

    private static int gameLevel;

    public int Galleons { get { return galleons; } set { galleons = value; } }
    public int GoldToNextLevel { get { return goldToNextLevel; } set { goldToNextLevel = value; } }
    public int GoldCurrent { get { return goldCurrent; } set { goldCurrent = value; } }

    public int Chickens { get { return chickens; } set { chickens = value; } }
    public int Pigs { get { return pigs; } set { pigs = value; } }
    public int Cows { get { return cows; } set { cows = value; } }
    public int Goats { get { return goats; } set { goats = value; } }
    public int Sheeps { get { return sheeps; } set { sheeps = value; } }

    public int Mines { get { return mines; } set { mines = value; } }
    public int Fishes { get { return fishes; } set { fishes = value; } }
    public int Woods { get { return woods; } set { woods = value; } }

    public int Corns { get { return corns; } set { corns = value; } }
    public int Melons { get { return melons; } set { melons = value; } }
    public int Mushrooms { get { return mushrooms; }  set { mushrooms = value; } }
    public int Apples { get { return apples; } set { apples = value; } }
    public int Oranges { get { return oranges; } set { oranges = value; } }

    public int Eggs { get { return eggs; } set { eggs = value; } }
    public int PigMeats { get { return pigMeats; } set { pigMeats = value; } }
    public int CowMilks { get { return cowMilks; } set { cowMilks = value; } }
    public int GoatMilks { get { return goatMilks; } set { goatMilks = value; } }
    public int Cotton { get { return cotton; } set { cotton = value; } }

    public int GameLevel { get { return gameLevel; } set { gameLevel = value; } }

    private void Awake()
    {
        galleons = 0;
        goldCurrent = 0;
        goldToNextLevel = 5;
        corns = 0;
        melons = 0;
        mushrooms = 0;
        chickens = 0;
        pigs = 0;
        cows = 0;
        goats = 0;
        sheeps = 0;
        eggs = 0;
        pigMeats = 0;
        cowMilks = 0;
        goatMilks = 0;
        cotton = 0;
        apples = 0;
        oranges = 0;
        mines = 0;
        fishes = 0;
        woods = 0;
        gameLevel = 1;
    }

    private void Update()
    {
        if (goldCurrent == goldToNextLevel)
        {
            gameLevel += 1;
            goldToNextLevel *= 2;
        }
    }
}
