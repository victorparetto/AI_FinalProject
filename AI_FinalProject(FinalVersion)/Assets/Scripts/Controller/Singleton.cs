using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton: MonoBehaviour {

	private static Singleton instance;
	string path;

    public int highestScore;
    public int latestScore;

    public static Singleton Instance
	{
		get
		{
			if (instance == null)
			{
				Debug.LogWarning("Creating Singleton");
				GameObject owner = new GameObject("Singleton");
				instance = owner.AddComponent<Singleton>();
			}
			return instance;
		}
	}

	private void Awake()
	{
		path = Application.persistentDataPath + "/playerInfo.dat";

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
		{
			Debug.LogWarning("Destroying Singleton");
			Destroy(this);
		}

	}

	public void Save () 
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (path);
		PlayerData data = new PlayerData ();
		DataToSave(data);
		bf.Serialize (file, data);
		file.Close ();

	}

	public void Load()
	{
		if (File.Exists (path))
		{
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path, FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize (file);
			file.Close();
			DataToLoad (data);
		}
	}

	void DataToSave(PlayerData data)
	{
        data.savedHighestScore = highestScore;
        data.savedLatestScore = latestScore;
	}

	void DataToLoad(PlayerData data)
	{
        highestScore = data.savedHighestScore;
        latestScore = data.savedLatestScore;
    }
}

[Serializable]
class PlayerData
{
    public int savedHighestScore;
    public int savedLatestScore;
}
