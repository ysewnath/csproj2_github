using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.IO.Compression;
using System;
using UnityEditor;

[CreateAssetMenu]
public class SessionManager : ScriptableObject
{
    [SerializeField]
    public string access_token = "";

    [SerializeField]
    public string id = "";

    [SerializeField]
    public string baseURL = "https://endlesslearner.com/";

    public List<DeckInfo> decks;

    public GameObject loginManager;
    LoginManager loginManagerHandler;

    // TODO: Redownload if hash doesn't match!
    public IEnumerator DownloadDecks()
    {
        Debug.Log("downloading decks");
        List<DeckInfo> invalids = new List<DeckInfo>();

        string dirPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\ProjectElle\\";
        if (!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);
        }

        foreach (DeckInfo d in this.decks)
        {
            //string packPath = "Assets/LanguagePacks/" + d.id;
            

            UnityWebRequest www = UnityWebRequest.Get(baseURL + "deck/zip/" + d.id);
            www.SetRequestHeader("Authorization", "Bearer " + this.access_token);
            yield return www.SendWebRequest();

            string packPath = dirPath + d.id;

            if (Directory.Exists(packPath)) Directory.Delete(packPath, true);
            using (BinaryWriter writer = new BinaryWriter(File.Open(packPath + ".zip", FileMode.Create)))
            {
                if(www.error != "" && www.error != null)
                {
                    Debug.Log("could not download zip file from site for deck: " + d.name);
                }
                writer.Write(www.downloadHandler.data);
            }
            if (new FileInfo(packPath + ".zip").Length < 50)
            {
                invalids.Add(d);
            }
            else
            {
                Debug.Log("deck extracted to directory: " +d.name);
                ZipFile.ExtractToDirectory(packPath + ".zip", packPath);
            }
            File.Delete(packPath + ".zip");
        }
        foreach (DeckInfo d in invalids)
        {
            this.decks.Remove(d);
        }
        foreach (var t in this.decks)
        {
            Debug.Log(t);
        }
        //EditorUtility.SetDirty(this);
        ParseDecks();
    }

    public void ParseDecks()
    {

        //
        // simulate parsing
        //
        loginManager = GameObject.Find("LoginHandler");
        loginManagerHandler = loginManager.GetComponent<LoginManager>();
        loginManagerHandler.LoginPrompt.SetActive(false);
        loginManagerHandler.messageBox.SetActive(false);
        loginManagerHandler.deckText.SetActive(false);
        loginManagerHandler.StartScreenPromt.SetActive(true);
        loginManagerHandler.DeckSelection.SetActive(true);
        loginManagerHandler.loggedIn = true;

        loginManagerHandler.loggedInIcon.SetActive(true);
        loginManagerHandler.LoggedInAsText.SetActive(true);
        loginManagerHandler.UserNameText.SetActive(true);
        loginManagerHandler.userNameTextHandler.text = loginManagerHandler.userInputHander.text.ToUpper();     
        
        

    }
}


public class DecksJson
{
    public List<int> ids { get; set; }
    public List<string> names { get; set; }
}

[Serializable]
public class DeckInfo
{
    public DeckInfo(int id_, string name_)
    {
        id = id_;
        name = name_;
    }
    public int id;
    public string name;
}