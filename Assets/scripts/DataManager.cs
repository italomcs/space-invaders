/*acess(from others scripts), change, save and load data(player progress)*/
/*put this into scene that need data*/

using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.Serialization;

[Serializable]
public class Data
{
    public string password;
    public string settings;
    public string users;
}

public class DataManager : MonoBehaviour
{
    public static Data data; /*data to save...acess this from others scripts for change values*/

    string dataPath;
    string dataName;
    string dataExtencion;
    string dataFullPath;

    string dataBackupFullPathName;

    public static bool firstLoad;

    public Data defaltData;

    private void Awake()
    {
        defaltData = new Data()
        {
            settings = "{\"player\":{\"sprite\":8,\"sprite1\":9,\"bulletSprite\":7,\"fireSpeed\":1.5,\"bulletSpeed\":5},\"enemies\":[{\"title\":\"\",\"lives\":1,\"score\":10,\"sprite1\":0,\"sprite2\":1,\"sprite3\":6,\"sprite4\":7,\"deathSound\":0},{\"title\":\"\",\"lives\":2,\"score\":20,\"sprite1\":2,\"sprite2\":3,\"sprite3\":6,\"sprite4\":7,\"deathSound\":0},{\"title\":\"\",\"lives\":3,\"score\":30,\"sprite1\":4,\"sprite2\":5,\"sprite3\":6,\"sprite4\":7,\"deathSound\":0}],\"difficulties\":[{\"title\":\"Facil\",\"lives\":3,\"refreshMovement\":0.2,\"refreshSpeed\":1,\"decrasePercent\":0.25,\"fireFrequency\":3,\"bulletSpeed\":5},{\"title\":\"Medio\",\"lives\":2,\"refreshMovement\":0.2,\"refreshSpeed\":2,\"decrasePercent\":0.25,\"fireFrequency\":2,\"bulletSpeed\":5},{\"title\":\"Dificil\",\"lives\":1,\"refreshMovement\":0.2,\"refreshSpeed\":3,\"decrasePercent\":0.25,\"fireFrequency\":1,\"bulletSpeed\":5},{\"title\":\"Personalizada\",\"lives\":10,\"refreshMovement\":0.2,\"refreshSpeed\":0.6,\"decrasePercent\":0.25,\"fireFrequency\":10,\"bulletSpeed\":5}],\"currentDifficulty\":0,\"background\":11,\"startScore\":0,\"gameTime\":60,\"introText\":\"Destrua todos os males\\nusando os produtos da linha\\nLeite de Rosas!!!,\",\"startTitle\":15}",
            users = "{\"list\":[]}"
            //users = "{\"list\":[{\"name\":\"user1\",\"email\":\"user1@email.com\",\"score\":10},{\"name\":\"user2\",\"email\":\"user2@email.com\",\"score\":10},{\"name\":\"user3\",\"email\":\"user3@email.com\",\"score\":10},{\"name\":\"user4\",\"email\":\"user5@email.com\",\"score\":10},{\"name\":\"user5\",\"email\":\"user5@email.com\",\"score\":10},{\"name\":\"user6\",\"email\":\"user6@email.com\",\"score\":10},{\"name\":\"user7\",\"email\":\"user7@email.com\",\"score\":10},{\"name\":\"user8\",\"email\":\"user8@email.com\",\"score\":10},{\"name\":\"user9\",\"email\":\"user9@email.com\",\"score\":10},{\"name\":\"user10\",\"email\":\"user10@email.com\",\"score\":10},{\"name\":\"user11\",\"email\":\"user11@email.com\",\"score\":10},{\"name\":\"user12\",\"email\":\"user12@email.com\",\"score\":10},{\"name\":\"user13\",\"email\":\"user13@email.com\",\"score\":10}]}"
        };

        dataPath = Application.persistentDataPath + "/" + "save" + "/"; /*create folder to user profile*/
        dataName = "data"; /*choose a file name*/
        dataExtencion = ".dat"; /*choose a file extension ex: ".dat" */
        dataFullPath = dataPath + dataName + dataExtencion; /*full file path*/

        dataBackupFullPathName = dataPath + dataName + " 2" + dataExtencion; /*backup file name*/

        if(firstLoad == false) { LoadData(); } /*loady only first time*/
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(dataBackupFullPathName)) { File.Delete(dataBackupFullPathName); } /*delete previos backup file*/
        if (File.Exists(dataFullPath)) { File.Move(dataFullPath, dataBackupFullPathName); } /*create backup file*/
        if (Directory.Exists(dataPath)==false) { Directory.CreateDirectory(dataPath); } /*create profile folder*/
        FileStream file = File.Create(dataFullPath); /*create file*/
        try
        {
            bf.Serialize(file, data); /*put informations(player progress) into data file*/
        }
        catch (SerializationException e)
        {
            Console.WriteLine("Failed to serialize. Reason: " + e.Message);
            throw;
        }
        finally
        {
            file.Close();
        }
    }

    public void LoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        Data dataTemp;

        //try read saved file...
        try
        {
            file = File.Open(dataFullPath, FileMode.Open);
            dataTemp = (Data)bf.Deserialize(file); /*get informations(saved player progress) from file*/
            file.Close();
        }
        //if impossible read saved file or saved file is corrupt try to read backup file...
        catch
        {
            try /*try read backup file*/
            {
                file = File.Open(dataBackupFullPathName, FileMode.Open);
                dataTemp = (Data)bf.Deserialize(file); /*get informations(saved player progress) from file*/
                file.Close();
            }
            catch /*if don't read backup file... set dataTemp to defalt*/
            {
                dataTemp = defaltData;
            }
        }

        data = dataTemp;

        firstLoad = true; /*load on first time*/
    }
}