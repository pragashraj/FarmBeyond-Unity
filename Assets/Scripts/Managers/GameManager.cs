using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static int galleons = 0;
    private static int goldToNextLevel = 5;
    private static int goldCurrent = 0;

    private static int corns = 0;
    private static int melons = 0;
    private static int mushrooms = 0;
    private static int apples = 0;
    private static int oranges = 0;

    private static int chickens = 0;
    private static int pigs = 0;
    private static int cows = 0;
    private static int goats = 0;
    private static int sheeps = 0;

    private static int mines = 0;
    private static int fishes = 0;
    private static int woods = 0;
    private static int eggs = 0;
    private static int pigMeats = 0;
    private static int cowMilks = 0;
    private static int goatMilks = 0;
    private static int cotton = 0;

    private static int gameLevel = 1;
    private static int totalRobots = 0;
    private static int usedRobots = 0;

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

    private RobotManager robotManager;

    private void Awake()
    {
        robotManager = GameObject.Find("RobotManager").GetComponent<RobotManager>();
        Data data = SavingData.LoadPlayer();
        if (data != null)
        {
            LoadData(data);
        }
    }

    private void Update()
    {
        if (goldCurrent == goldToNextLevel)
        {
            gameLevel += 1;
            goldToNextLevel *= 2;
        }
        totalRobots = robotManager.GetTotalRobots;
        usedRobots = robotManager.GetUsedRobots;
    }

    public void SaveGame()
    {
        Data data = new Data(
                galleons,
                goldToNextLevel,
                goldCurrent,
                corns,
                melons,
                mushrooms,
                apples,
                oranges,
                chickens,
                pigs,
                cows,
                goats,
                sheeps,
                mines,
                fishes,
                woods,
                eggs,
                pigMeats,
                cowMilks,
                goatMilks,
                cotton,
                gameLevel,
                totalRobots,
                usedRobots
            );
        SavingData.SavePlayer(data);
    }

    private void LoadData(Data data)
    {
        galleons = data.galleons;
        goldCurrent = data.goldCurrent;
        goldToNextLevel = data.goldToNextLevel;
        corns = data.corns;
        melons = data.melons;
        mushrooms = data.mushrooms;
        chickens = data.chickens;
        pigs = data.pigs;
        cows = data.cows;
        goats = data.goats;
        sheeps = data.sheeps;
        eggs = data.eggs;
        pigMeats = data.pigMeats;
        cowMilks = data.cowMilks;
        goatMilks = data.goatMilks;
        cotton = data.cotton;
        apples = data.apples;
        oranges = data.oranges;
        mines = data.mines;
        fishes = data.fishes;
        woods = data.woods;
        gameLevel = data.gameLevel;
        totalRobots = data.totalRobots;
        usedRobots = data.usedRobots;
    }
}
