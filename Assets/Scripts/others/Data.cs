
[System.Serializable]
public class Data 
{
    public int galleons;
    public int goldToNextLevel;
    public int goldCurrent;
    public int corns;
    public int melons;
    public int mushrooms;
    public int apples;
    public int oranges;
    public int chickens;
    public int pigs;
    public int cows;
    public int goats;
    public int sheeps;
    public int mines;
    public int fishes;
    public int woods;
    public int eggs;
    public int pigMeats;
    public int cowMilks;
    public int goatMilks;
    public int cotton;
    public int gameLevel;
    public int totalRobots = 0;
    public int usedRobots = 0;

    public Data(int galleons, int goldToNextLevel, int goldCurrent, int corns, int melons, int mushrooms, int apples, int oranges, int chickens, int pigs, int cows, int goats, int sheeps, int mines, int fishes, int woods, int eggs, int pigMeats, int cowMilks, int goatMilks, int cotton, int gameLevel, int totalRobots, int usedRobots)
    {
        this.galleons = galleons;
        this.goldToNextLevel = goldToNextLevel;
        this.goldCurrent = goldCurrent;
        this.corns = corns;
        this.melons = melons;
        this.mushrooms = mushrooms;
        this.apples = apples;
        this.oranges = oranges;
        this.chickens = chickens;
        this.pigs = pigs;
        this.cows = cows;
        this.goats = goats;
        this.sheeps = sheeps;
        this.mines = mines;
        this.fishes = fishes;
        this.woods = woods;
        this.eggs = eggs;
        this.pigMeats = pigMeats;
        this.cowMilks = cowMilks;
        this.goatMilks = goatMilks;
        this.cotton = cotton;
        this.gameLevel = gameLevel;
        this.totalRobots = totalRobots;
        this.usedRobots = usedRobots;
    }
}
