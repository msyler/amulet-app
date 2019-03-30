using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    WebController wb;
    List<AppTransitionItem> itemList = new List<AppTransitionItem>();
    public Transform InventSlotPrefab;
    public Transform InventSlotPrefabParent;

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
        Debug.Log(itemsURL);
        RestClient.Get(itemsURL).Then(response =>
        {
            string jsonComplete = "{\"items\": " + response.Text + "}";
            var res = JsonUtility.FromJson<AppTransitionItemObject<AppTransitionItem>>(jsonComplete);

            for (var i = 0; i < res.items.Length; i++)
            {
                itemList.Add(res.items[i]);
                Debug.Log(res.items[i].item_name);
                Transform _slotPrefab = Instantiate(InventSlotPrefab, new Vector3(0,0,0), Quaternion.identity);
                _slotPrefab.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Text txt = _slotPrefab.Find("Text").GetComponent<Text>();
                txt.text = res.items[i].item_name;
                _slotPrefab.parent = InventSlotPrefabParent;
            }
        });
    }
}
