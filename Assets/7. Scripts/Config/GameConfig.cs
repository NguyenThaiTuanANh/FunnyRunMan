using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [SerializeField] private SO_LevelDatabase so_LevelDatabase;

    public int GetTotalLevelNumber()
    {
        return this.so_LevelDatabase.levels.Count;
    }

    public SO_LevelData GetLevelDatabase()
    {
        StaticData.selectGameLevel = Mathf.Min(StaticData.selectGameLevel, GetTotalLevelNumber());
        return this.so_LevelDatabase.GetLevel(StaticData.selectGameLevel);
    }
}
