using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    public GameObject panelInventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleInventory() {
        Debug.Log("Click Toggle");
        bool isActive = panelInventory.activeSelf;
        panelInventory.SetActive(!isActive);
    }
}
