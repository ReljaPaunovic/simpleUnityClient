using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections.Generic;

public class main : MonoBehaviour
{

    public Text queryField;
    public List<string> names = new List<string>();
    public Transform listElement;
    public Transform parrent;

    void showFirstTen()
    {
        if (names.Count >= 10)
        {
            for (var i = 0; i < 10; i++)
            {
                Transform txt = (Transform)Instantiate(listElement, parrent);
                txt.GetComponent<Text>().text = names[i];
            }
        }
        // Just in case there are less than 10 results
        else
        {
            for (var i = 0; i < names.Count; i++)
            {
                Transform txt = (Transform)Instantiate(listElement, parrent);
                txt.GetComponent<Text>().text = names[i];

            }
        }
    }
    public void doSomething()
    {
        // DESTROY ALL CHILDREN!!
        int childs = parrent.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.Destroy(parrent.GetChild(i).gameObject);
        }
        names.Clear();
        // I LIMITED IT TO 100 RESULTS, DEFAULT IS 25

        string query = queryField.text.ToString();
        string url = "https://external.api.yle.fi/v1/programs/items.json?app_id=b8873981&app_key=47bf6d45f9a552889a679154849b1023&language=fi&limit=100&q=" + query;
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            var data = JSON.Parse(www.text);
            var n = data["data"].Count;
            for (var i = 0; i < n; i++)
            {
                names.Add(data["data"][i]["title"]["fi"]);
            }
            showFirstTen();
        }
        else {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
