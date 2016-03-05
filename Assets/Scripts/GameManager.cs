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
        public BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
        private int level = 3;                                  //Current level number, expressed in game as "Day 1".
        private InputField IPAddressText;


        public static int RingNumber;
        public int PlayerAppearance;


        public void OnEnable()
        {
            Debug.Log("Getting instance");
            if (instance == null)
                instance = this;
            else if (instance != this)
            {
            Debug.Log("Destroying instance");
                Destroy(gameObject);
                return;
            }
        }


        //Awake is always called before any Start functions
        void Awake()
        {
            DontDestroyOnLoad(this);

            //Get a component reference to the attached BoardManager script
            GameObject.Find("RingNumberInputField").GetComponent<InputField>().text = PlayerPrefs.GetInt("RingNumberInputField").ToString();
            RingNumberInputFieldListener(PlayerPrefs.GetInt("RingNumberInputField").ToString());
            GameObject.Find("RingNumberInputField").GetComponent<InputField>().onEndEdit.AddListener(RingNumberInputFieldListener);

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
            Debug.Log("Saved new RingNumberInputField " + RingNumber + " in prefs"); //prints "Listened to change on value 3.14"
             boardScript = GetComponentInChildren<BoardManager>();
            boardScript.rings = RingNumber;
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
            boardScript.SetupScene(RingNumber);


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