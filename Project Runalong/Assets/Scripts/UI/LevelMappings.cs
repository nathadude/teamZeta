using System.Collections;
using System.Collections.Generic;

// A Data class which contains a map from level ID to name, and level ID to leaderboard ID
public static class LevelMappings
{
    public static List<int> Levels = new List<int>() { 1, 2, 3 };

    public static Dictionary<int, string> IdToName = new Dictionary<int, string>()
    {
        { -1, "Test" },
        { 0, "Placeholder"},
        { 1, "Forest"},
        { 2, "Mountain"},
        { 3, "Ocean"}
    };

    public static Dictionary<int, int> IdToLeaderboard = new Dictionary<int, int>()
    {
        { -1, 3126},
        { 0, 3193},
        { 1, 3194},
        {2, 3440},
        {3, 3441}
    };
}
