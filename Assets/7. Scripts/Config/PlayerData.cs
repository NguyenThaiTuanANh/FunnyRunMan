using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    [Header("Player Data")]
    public bool isReset;
    public int Coin;
    public int LevelUnlocked;

    [Header("Owned Characters")]
    public HashSet<string> ownedCharacters = new();


    // =====================
    // PlayerPrefs Keys
    // =====================
    private const string COIN_KEY = "Coin";
    private const string LEVEL_KEY = "LevelUnlocked";
    private const string CHARACTER_KEY = "OwnedCharacters";
    private const string ITEM_KEY = "OwnedItems";

    [Header("Test Data")]
    [SerializeField] private bool useTestData = false;

    [SerializeField] private int testCoin = 9999;
    [SerializeField] private int testLevelUnlocked = 10;

    [SerializeField]
    private List<string> testCharacters = new()
{
    "Knight",
    "Mage",
    "Archer"
};

    private void ApplyTestData()
    {
        Coin = testCoin;
        LevelUnlocked = testLevelUnlocked;

        ownedCharacters.Clear();
        foreach (var c in testCharacters)
            ownedCharacters.Add(c);

        SaveAll();
    }

    private void SaveAll()
    {
        PlayerPrefs.SetInt(COIN_KEY, Coin);
        PlayerPrefs.SetInt(LEVEL_KEY, LevelUnlocked);

        SaveCharacters();

        PlayerPrefs.Save();
    }

    // =====================
    // UNITY
    // =====================
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (isReset)
        {
            ResetData();
        }
        if (useTestData)
        {
            ResetData();
            ApplyTestData();
        }
        else
        {
            LoadData();
        }
    }

    // =====================
    // RESET
    // =====================
    private void ResetData()
    {
        PlayerPrefs.DeleteKey(COIN_KEY);
        PlayerPrefs.DeleteKey(LEVEL_KEY);
        PlayerPrefs.DeleteKey(CHARACTER_KEY);
        PlayerPrefs.DeleteKey(ITEM_KEY);
        PlayerPrefs.Save();
    }

    // =====================
    // COIN / LEVEL
    // =====================
    public void SetCoin(int coin)
    {
        Coin = coin;
        PlayerPrefs.SetInt(COIN_KEY, Coin);
        PlayerPrefs.Save();
    }

    public void AddCoin(int value)
    {
        Coin += value;
        SetCoin(Coin);
    }

    public bool SpendCoin(int amount)
    {
        if (Coin < amount) return false;

        Coin -= amount;
        SetCoin(Coin);
        return true;
    }

    public void SaveLevel(int level)
    {
        if (level > LevelUnlocked)
        {
            LevelUnlocked = level;
            PlayerPrefs.SetInt(LEVEL_KEY, LevelUnlocked);
            PlayerPrefs.Save();
        }
    }

    // =====================
    // CHARACTER
    // =====================
    public void AddOwnedCharacter(string characterId)
    {
        if (ownedCharacters.Add(characterId))
            SaveCharacters();
    }

    public bool HasCharacter(string characterId)
    {
        return ownedCharacters.Contains(characterId);
    }

    private void SaveCharacters()
    {
        OwnedCharacterWrapper wrapper = new OwnedCharacterWrapper(ownedCharacters);
        PlayerPrefs.SetString(CHARACTER_KEY, JsonUtility.ToJson(wrapper));
        PlayerPrefs.Save();
    }

    private void LoadCharacters()
    {
        ownedCharacters.Clear();

        string json = PlayerPrefs.GetString(CHARACTER_KEY, "");
        if (string.IsNullOrEmpty(json)) return;

        OwnedCharacterWrapper wrapper = JsonUtility.FromJson<OwnedCharacterWrapper>(json);
        if (wrapper?.list == null) return;

        ownedCharacters = new HashSet<string>(wrapper.list);
    }

    // =====================
    // LOAD ALL
    // =====================
    public void LoadData()
    {
        Coin = PlayerPrefs.GetInt(COIN_KEY, 0);
        LevelUnlocked = PlayerPrefs.GetInt(LEVEL_KEY, 1);

        LoadCharacters();
    }
}

[System.Serializable]
public class OwnedCharacterWrapper
{
    public List<string> list;

    public OwnedCharacterWrapper() { list = new List<string>(); }

    public OwnedCharacterWrapper(HashSet<string> set)
    {
        list = new List<string>(set);
    }
}

