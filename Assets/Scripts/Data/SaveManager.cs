using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public int highScore;
    public int newMaxScore;
    public int currentScore;

    public int bombCount = 5;
    public int jokerCount;

    public bool isButtonPressed;


 //   public Transform[] intro = new Transform[16];
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
            newMaxScore = data.newMaxScore;
            currentScore = data.currentScore;

            bombCount = data.bombCount;
            jokerCount = data.jokerCount;

       //     intro = data.intro;
            isButtonPressed = data.isButtonPressed;


            file.Close();
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(UnityEngine.Application.persistentDataPath + "/playerInfo.dat");
        PlayerData_Storage data = new PlayerData_Storage();

        data.highScore = highScore;
        data.newMaxScore = newMaxScore;
        data.currentScore = currentScore;
        data.bombCount = bombCount;
        data.jokerCount = jokerCount;
     //   data.intro = intro;
        data.isButtonPressed = isButtonPressed;



        bf.Serialize(file, data);
        file.Close();
    }
    public void DeleteSaveFile()
    {
        string path = UnityEngine.Application.persistentDataPath + "/playerInfo.dat";
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Save file deleted.");
        }
        else
        {
            Debug.LogWarning("Save file does not exist.");
        }
    }


    [System.Serializable]
    class PlayerData_Storage
    {
        public int highScore;
        public int newMaxScore;
        public int currentScore;
        public int bombCount;
        public int jokerCount;
        //public Transform[] intro;
        public bool isButtonPressed;


    }
}
