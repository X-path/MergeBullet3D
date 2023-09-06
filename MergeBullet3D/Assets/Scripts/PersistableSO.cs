using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistableSO : MonoBehaviour {
	
	[Header("Meta")]
	public string persisterName;
	[Header("Scriptable Objects")]
	public List<ScriptableObject> objectsToPersist;



	
	public void Awake()
	{
		Debug.Log(Application.persistentDataPath);


		for (int i = 0; i < objectsToPersist.Count; i++) 
		{

			if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i )))
			{

				// File.Delete(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i ));


				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i ), FileMode.Open);
				JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file),objectsToPersist[i]);
				Debug.Log(objectsToPersist[i].ToString());
				file.Close();
				
				
			}

            else 
			{
                //Do Nothing

			}
	}
	}

	void OnDisable()
	{
		SaveData();
	}



	 void SaveData() 
	{

		for (int i = 0; i < objectsToPersist.Count; i++) 
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persisterName, i ));
			var json = JsonUtility.ToJson(objectsToPersist[i]);
			bf.Serialize(file, json);
			file.Close();
		}
		
	}

	private void OnApplicationPause(bool pauseStatus) 
	{
		SaveData();
	}

    void OnApplicationFocus(bool hasFocus)
    {
        if(!hasFocus)
            SaveData();
    }

	private void OnApplicationQuit()
    {

        SaveData();
    }

}