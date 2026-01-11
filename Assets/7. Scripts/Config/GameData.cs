using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private Transform modelParent;
    [HideInInspector] public SO_LevelData sO_LevelData;

    // Start is called before the first frame update
    void Start()
    {
        sO_LevelData = gameConfig.GetLevelDatabase();
        GameSceneManager.Instance.LoadSubLevel(sO_LevelData.levelName);
        foreach (var modelData in sO_LevelData.modelDatas)
        {
            Instantiate(modelData.modelPrefab, modelData.position, modelData.modelPrefab.transform.rotation, modelParent);
        }
    }

    public GameConfig GetGameConfig()
    {
        return gameConfig;
    }
}
