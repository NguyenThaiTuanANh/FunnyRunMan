using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelData
{
    public GameObject modelPrefab;
    public Vector3 position;
}


[CreateAssetMenu(fileName = "SO_LevelData", menuName = "Game/SO_LevelData")]
public class SO_LevelData : ScriptableObject
{
    public int rewardCoin;
    public string levelName;
    public List<ModelData> modelDatas;
}
