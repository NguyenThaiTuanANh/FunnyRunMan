using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_LevelDatabase", menuName = "Game/SO_LevelDatabase")]
public class SO_LevelDatabase : ScriptableObject
{
    public List<SO_LevelData> levels = new();

    public SO_LevelData GetLevel(int levelNumber)
    {
        Debug.Log(levelNumber);
        return levels[levelNumber - 1];
    }
}
