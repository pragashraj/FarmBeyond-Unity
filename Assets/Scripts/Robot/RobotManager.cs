using UnityEngine;

public class RobotManager : MonoBehaviour
{
    private static int total_robots;
    private static int usedRobots;

    private void Awake()
    {
        total_robots = 2;
        usedRobots = 0;
    }

    public int GetTotalRobots { get { return total_robots; } }
    public int GetUsedRobots { get { return usedRobots; } }


    public void IncreaseOrDecreaseRobotsTotal(int count)
    {
        total_robots += count;
    }

    public void IncreaseOrDecreaseRobotsUsed(int count)
    {
        usedRobots += count;
    }
}
