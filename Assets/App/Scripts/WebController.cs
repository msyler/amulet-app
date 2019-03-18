using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEditor;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Threading;
using System.Threading.Tasks;


// ITEM STRUCTURES : BEGIN
[Serializable]
public class ItemLocation
{
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

// ITEM STRUCTURES : END

// USER Object
[Serializable]
public class UserObject
{
    public string id;
    public string username;
    public string password;
}


//MAIN WebController Class
public class WebController : MonoBehaviour
{
    
    // Start is called before the first frame update
    public Transform Sun;
    public Transform Moon;

    //Form input fields
    public Text username;
    public InputField password;

    //Class utilities
    public string userId;
    private String baseURL = "";

    //Environment Enum
    public enum EnvironmentEnum 
    {
        LOCAL,
        STAGING
    };

    //SET ENVIRONMENT (CHANGE for Staging server test)
    public EnvironmentEnum Environment = EnvironmentEnum.LOCAL;

    void OnLevelWasLoaded()
    {
        Debug.Log("On Level Was loaded");
        Debug.Log(this.userId);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        if (!Application.isEditor)
        {
            this.Environment = EnvironmentEnum.STAGING;
        }

        if (this.Environment == EnvironmentEnum.LOCAL)
        {
            this.baseURL = "http://localhost:3000/api/";
        }

        if (this.Environment == EnvironmentEnum.STAGING)
        {
            this.baseURL = "https://amulet-api.herokuapp.com/api/";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Login on server and get User Id for later usage
    public void Login()
    {
        RestClient.Post(this.baseURL + "TempUsers/login", "{\"username\": \"" + this.username.text +  "\", \"password\": \"" + this.password.text +  "\"}").Then(response => 
        {
            var res = JsonUtility.FromJson<UserObject>(response.Text);
            this.userId = res.id;
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }, error => {
            Debug.Log("Login Error");
        });
    }

    public string GetItemsURL()
    {
        return this.baseURL + "TempUsers/" + this.userId + "/item-actives";
    }
}
