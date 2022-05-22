using System.Collections;
using System.Collections.Generic;

// A Data class which contains a map from level ID to name, and level ID to leaderboard ID
public static class LevelMappings
{
    public static List<int> Levels = new List<int>() { -1, 0, 1 };

    public static Dictionary<int, string> IdToName = new Dictionary<int, string>()
    {
        { -1, "Test" },
        { 0, "Placeholder"},
        { 1, "Forest"}
    };

    public static Dictionary<int, int> IdToLeaderboard = new Dictionary<int, int>()
    {
        { -1, 3126},
        { 0, 3193},
        { 1, 3194}
    };
}
