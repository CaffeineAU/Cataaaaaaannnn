using UnityEngine;
using System.Collections;

namespace Completed
{
    using System.Collections.Generic;       //Allows us to use Lists. 
    using UnityEngine.Networking;
    using UnityEngine.UI;

    public class GameManager : NetworkManager
    {

        public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
        private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
        private int level = 3;                                  //Current level number, expressed in game as "Day 1".
        private InputField IPAddressText;


        public int RingNumber;
        public int PlayerAppearance;

        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)

                //if not, set instance to this
                instance = this;

            //If instance already exists and it's not this:
            else if (instance != this)

                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
                Destroy(gameObject);

            //Sets this to not be destroyed when reloading scene
            //DontDestroyOnLoad(this);

            //Get a component reference to the attached BoardManager script
            //boardScript = GetComponentInChildren<BoardManager>();
            //Debug.Log("got board manager object: " + boardScript.name);

            //Debug.Log("Getting IP address object");
            //IPAddressText = GameObject.Find("HostAddressInputField").GetComponent<InputField>();
            //Debug.Log("Got IP address object: " + IPAddressText.text);


            //Call the InitGame function to initialize the first level 
            //InitGame();


            GameObject.Find("RingNumberInputField").GetComponent<InputField>().text = PlayerPrefs.GetInt("RingNumberInputField").ToString();
            RingNumberInputFieldListener(PlayerPrefs.GetInt("RingNumberInputField").ToString());
            GameObject.Find("RingNumberInputField").GetComponent<InputField>().onEndEdit.AddListener(RingNumberInputFieldListener);

            //GameObject.Find("AppearanceDropdown").GetComponent<Dropdown>().value = PlayerPrefs.GetInt("PlayerAppearance");
            //AppearanceSelectedListener(PlayerPrefs.GetInt("PlayerAppearance"));
            //GameObject.Find("AppearanceDropdown").GetComponent<Dropdown>().onValueChanged.AddListener(AppearanceSelectedListener);
        }

        public override void OnServerSceneChanged(string sceneName)
        {
            if (sceneName == onlineScene)
            {
                //Call the InitGame function to initialize the first level 
                InitGame();

            }
            base.OnServerSceneChanged(sceneName);
        }

        void RingNumberInputFieldListener(string rings)
        {
            RingNumber = int.Parse(rings);
            Debug.Log("Listened to change RingNumberInputField value " + rings); //prints "Listened to change on value 3.14"
            PlayerPrefs.SetInt("RingNumberInputField", RingNumber);
            PlayerPrefs.Save();
            Debug.Log("Saved new RingNumberInputField " + rings + " in prefs"); //prints "Listened to change on value 3.14"
        }

        void AppearanceSelectedListener(int appearance)
        {
            PlayerAppearance = appearance;
            Debug.Log("Listened to change appearance value " + appearance); //prints "Listened to change on value 3.14"
            PlayerPrefs.SetInt("PlayerAppearance", appearance);
            PlayerPrefs.Save();
            Debug.Log("Saved new appearance in prefs"); //prints "Listened to change on value 3.14"
        }


        //Initializes the game for each level.
        void InitGame()
        {
            //Call the SetupScene function of the BoardManager script, pass it current level number.
            boardScript = GetComponentInChildren<BoardManager>();
            Debug.Log("got board manager object: " + boardScript.name);
            Debug.Log("Setting up board with " + RingNumber + " rings");
            boardScript.SetupScene();


        }

        void Start()
        {

        }

        public void ConnectNetworkClient()
        {
            IPAddressText = GameObject.Find("HostAddressInputField").GetComponent<InputField>();
            Debug.Log("connecting client to address: " + IPAddressText.text);

            //if (!NetworkClient.active)
            //{
            if (!string.IsNullOrEmpty(IPAddressText.text))
                {
                    networkAddress = IPAddressText.text;
                    Debug.Log(networkAddress);
                }
                NetworkClient client = StartClient();
            Debug.Log(client);

            //}
        }

        public void StartHostGame()
        {
            Debug.Log("Starting host");
            StartHost();
        }

        public void DisconnectGame()
        {
            if (NetworkServer.active || NetworkClient.active)
            {
                Debug.Log("Disconecting host");
                StopHost();
            }
        }

        //Update is called every frame.
        void Update()
        {

        }

        //[SyncVar]
        //public string PlayerName;

        //public override void OnStartLocalPlayer()
        //{
        //    CmdSendNameToServer(GameObject.Find("PlayerNameInputField").GetComponent<InputField>().text);
        //}

        //public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
        //{
        //    GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            
        //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //}


        
        //[Command]
        //void CmdSendNameToServer(string name)
        //{
        //    PlayerName = name;
        //}


    }
}