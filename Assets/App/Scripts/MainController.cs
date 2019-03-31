using Proyecto26;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Factories;
using Mapbox.Unity.Utilities;

public class MainController : MonoBehaviour
{
    WebController wb;
    List<AppTransitionItem> itemList = new List<AppTransitionItem>();
    [SerializeField]
    AbstractMap _map;

    public Transform InventSlotPrefab;
    public Transform InventSlotPrefabParent;
    public Transform MoonPrefab;
    public Transform SunPrefab;

    List<AppTransitionItem> items = new List<AppTransitionItem>(); 
    // Start is called before the first frame update
    void Start()
    {
        this.wb = GameObject.Find("WebController").GetComponent<WebController>();
        this.GetUserItems();
        this.GetWorldItems();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Get worldItems
    private void GetWorldItems()
    {
        string itemsURL = this.wb.GetWorldItemsURL();
        RestClient.Get(itemsURL).Then(response =>
        {
            string jsonComplete = "{\"items\": " + response.Text + "}";
            var res = JsonUtility.FromJson<AppTransitionItemObject<AppTransitionItem>>(jsonComplete);

            for (var i = 0; i < res.items.Length; i++)
            {
                
                Debug.Log(res.items[i].item_name);
                var instance = (GameObject)Instantiate(Resources.Load("Models/" + res.items[i].item_name));
                string locationString = "";
                locationString += res.items[i].location.lat + ", ";
                locationString += res.items[i].location.lng;
                Debug.Log(locationString);
                instance.transform.localPosition = _map.GeoToWorldPosition(Conversions.StringToLatLon(locationString), true);
            }
        });
    }

    //Get current player items
    private void GetUserItems()
    {
        string itemsURL = this.wb.GetItemsURL();
        
        RestClient.Get(itemsURL).Then(response =>
        {
            string jsonComplete = "{\"items\": " + response.Text + "}";
            var res = JsonUtility.FromJson<AppTransitionItemObject<AppTransitionItem>>(jsonComplete);

            for (var i = 0; i < res.items.Length; i++)
            {
                itemList.Add(res.items[i]);
                
                Transform _slotPrefab = Instantiate(InventSlotPrefab, new Vector3(0,0,0), Quaternion.identity);
                _slotPrefab.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                Text txt = _slotPrefab.Find("Text").GetComponent<Text>();
                txt.text = res.items[i].item_name;

                _slotPrefab.parent = InventSlotPrefabParent;
            }
        });
    }
}
