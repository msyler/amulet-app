using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEditor;
using System;

[Serializable]
public class ItemLocation {
    public float lat;
    public float lng;
}


[Serializable]
public class AppTransitionItem
{
    public string id;
    public string item_name;
    public string desc;
    public ItemLocation location;
    public int model_id;
}

[Serializable]
public class AppTransitionItemObject<T>
{
    public T[] items;
}


public class WebController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Sun;
    public Transform Moon;

    private string itemsRoute = "http://localhost:3000/api/items";
    void Start()
    {
        //string json = "[{\"id\":\"5c72ec27168727249049f1cf\",\"item_name\":\"Sun\",\"desc\":\"Bright!\",\"location\":{ \"lat\":1.5,\"lng\":2.5}},{\"id\":\"5c72ec45168727249049f1d0\",\"item_name\":\"Moon\",\"desc\":\"Have a rabbit\",\"location\":{\"lat\":2.5,\"lng\":3.5}}]";
        
        RestClient.Get(itemsRoute).Then(response => {
            string jsonComplete = "{\"items\": " + response.Text + "}";
            var res = JsonUtility.FromJson<AppTransitionItemObject<AppTransitionItem>>(jsonComplete);
            for (var i = 0; i < res.items.Length; i++)
            {
                if (res.items[i].model_id == 1)
                {
                    Instantiate(this.Moon, new Vector3(res.items[i].location.lat, 0, res.items[i].location.lng), Quaternion.identity);
                }
                if (res.items[i].model_id == 2)
                {
                    Instantiate(this.Sun, new Vector3(res.items[i].location.lat, 0, res.items[i].location.lng), Quaternion.identity);
                }
                Debug.Log(res.items[0].location.lat);
            }
         });

     }

    // Update is called once per frame
    void Update()
    {
        
    }
}
