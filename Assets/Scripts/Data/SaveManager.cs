using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public int highScore;
    public int currentScore;

    public int bombCount;
    public int jokerCount;
    public bool[] WeaponUnlocked = new bool[6] { true, false, false, false, false, false };

    public int currentLevel;
    public bool[] levelUnlocked = new bool[5] { true, false, false, false, false };
    public static SaveManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    public void Load()
    {
        if (File.Exists(UnityEngine.Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(UnityEngine.Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData_Storage data = (PlayerData_Storage)bf.Deserialize(file);

            highScore = data.highScore;
            currentScore = data.currentScore;

            bombCount = data.bombCount;
            jokerCount = data.jokerCount;
            WeaponUnlocked = data.WeaponUnlocked;
            currentLevel = data.currentLevel;
            levelUnlocked = data.LevelUnlocked;

            if (data.WeaponUnlocked == null)
                WeaponUnlocked = new bool[6] { true, false, false, false, false, false };
            if (data.LevelUnlocked == null)
                levelUnlocked = new bool[5] { true, false, false, false, false };
            file.Close();
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(UnityEngine.Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.highScore = highScore;
        data.currentScore = currentScore;
        data.bombCount = bombCount;
        data.jokerCount = jokerCount;

        data.WeaponUnlocked = WeaponUnlocked;
        data.currentLevel = currentLevel;
        data.LevelUnlocked = levelUnlocked;

        bf.Serialize(file, data);
        file.Close();
    }
    public int GetHighScore()
    {
        return highScore;
    }
    public void ChangeScore(int newScore)
    {
        highScore = newScore;
        Save();
    }
    public bool GetWea(int _index)
    {
        return WeaponUnlocked[_index];
    }
    public void ChangeWea(int _index, bool value)
    {
        WeaponUnlocked[_index] = value;
        Save();
    }
    public void ChangeLevel(int _index, bool value)
    {
        levelUnlocked[_index] = value;
        Save();
    }
    public void ResetData()
    {
        if (File.Exists(UnityEngine.Application.persistentDataPath + "/playerInfo.dat"))
        {
            File.Delete(UnityEngine.Application.persistentDataPath + "/playerInfo.dat");
        }

        currentScore = 0;
        WeaponUnlocked = new bool[6] { true, false, false, false, false, false };

        Save();
    }

    [System.Serializable]
    class PlayerData_Storage
    {
        public int highScore;
        public int currentScore;
        public int bombCount;
        public int jokerCount;

        public bool[] WeaponUnlocked = new bool[6];

        public int currentLevel;
        public bool[] LevelUnlocked = new bool[5];
    }
}
