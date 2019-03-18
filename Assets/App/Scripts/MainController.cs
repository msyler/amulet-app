using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    WebController wb;
    List<AppTransitionItem> itemList = new List<AppTransitionItem>();

    List<AppTransitionItem> items = new List<AppTransitionItem>(); 
    // Start is called before the first frame update
    void Start()
    {
        this.wb = GameObject.Find("WebController").GetComponent<WebController>();
        this.GetItems();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Get current player items
    private void GetItems()
    {
        string itemsURL = this.wb.GetItemsURL();
        RestClient.Get(itemsURL).Then(response =>
        {
            string jsonComplete = "{\"items\": " + response.Text + "}";
            var res = JsonUtility.FromJson<AppTransitionItemObject<AppTransitionItem>>(jsonComplete);

            for (var i = 0; i < res.items.Length; i++)
            {
                itemList.Add(res.items[i]);
            }
        });
    }
}
