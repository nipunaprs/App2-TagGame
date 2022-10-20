using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEditor;


public class Leaderbaord : MonoBehaviour
{
    public GameObject container;
    private Transform entryContainer;
    private Transform entryTemplate;
    public TextAsset file;
    bool isEmpty = false;

    SortedDictionary<int, string> myDict = new SortedDictionary<int, string>();

    private void Awake()
    {
        entryContainer = container.transform;
        entryTemplate = transform.Find("LeaderboardTemplate");

        entryTemplate.gameObject.SetActive(false);

        LoadData();

        float templateHeight = 50f;
        int count = 0;

        if(!isEmpty)
        {
            foreach (KeyValuePair<int, string> kvp in myDict)
            {
                Transform entryTransform = Instantiate(entryTemplate, entryContainer);
                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * count);
                entryTransform.gameObject.SetActive(true);

                entryTransform.Find("ITScoreTxt").GetComponent<Text>().text = kvp.Key.ToString();
                entryTransform.Find("PlayerNameTxt").GetComponent<Text>().text = kvp.Value;
                count++;
            }
        }
        

       


    }

    void LoadData()
    {
        //AssetDatabase.ImportAsset("Assets/Database/scores.txt"); --> This properly refreshes the txt file but can't build game with this b/c its editor method
        //file = (TextAsset)Resources.Load("scores", typeof(TextAsset)); //Resources.Load doesn't refresh the txt file even when calling it again
        //Resources.Load<TextAsset>("scores");
        //Solution was to use StreamReader instead of the unity text asset file thing
        //Notes for later project lol ^

        var sr = new StreamReader(Application.dataPath + "/Resources/scores.txt");
        var fileData = sr.ReadToEnd();
        sr.Close();

        var lines = fileData.Split("\n"[0]);
        List<string> listOfData = new List<string>(lines);

        //Unity code to read files -- doesn't update every new entry so implemented streamreader instead above
        //var content = file.text;
        //var allData = content.Split("\n");
        //List<string> listOfData = new List<string>(allData);
        

        //Note since using sorteddictionary -- scores that are the same will be replaced in the leaderboard with new ones.
        if(listOfData.Any())
        {

            myDict = new SortedDictionary<int, string>();
            foreach (string x in listOfData)
            {
                string[] arrayz = x.Split("-");
                if(myDict.ContainsKey(int.Parse(arrayz[1])))
                {
                    myDict[int.Parse(arrayz[1])] = arrayz[0].ToString();
                }
                else
                {
                    myDict.Add(int.Parse(arrayz[1]), arrayz[0].ToString());
                }
                
            }
        }
        else
        {
            isEmpty = true;
            Debug.Log("No High Scores");
        }
        

        


        
    }
}
