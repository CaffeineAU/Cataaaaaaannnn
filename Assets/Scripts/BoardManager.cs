﻿using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed

{


    public class BoardManager : MonoBehaviour
    {
        public enum TileType
        {
            Desert,
            Forest,
            Mountain,
            Pasture
        }

        public float tileSideLength;

        private Vector3 TileOffsetY;
        private Vector3 TileOffsetX;

        public List<Vector3> TileOffsets = new List<Vector3>();
        public List<Vector3> BorderTileOffsets = new List<Vector3>();

        public int rings;


        private Transform boardHolder;                                          //A variable to store a reference to the transform of our Board object.
        public List<GameObject> Tiles = new List<GameObject>();   //A list of possible locations to place players.
        private List<Vector3> TileLocations = new List<Vector3>();   //A list of possible locations to place players.
        private int numTiles;

        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            Debug.Log("Rings : " + rings);
            TileOffsets.Clear();

            for (var i = 0; i < rings; i++)
            {
                for (var j = -i; j <= i; j++)
                {
                    for (var k = -i; k <= i; k++)
                    {
                        for (var l = -i; l <= i; l++)
                        {
                            if (Math.Abs(j) + Math.Abs(k) + Math.Abs(l) == i * 2 && j + k + l == 0)
                            {
                                TileOffsets.Add(new Vector3(j, k, l));
                            }
                        }
                    }
                }

            }
            for (var i = rings; i < rings+1; i++)
            {
                for (var j = -i; j <= i; j++)
                {
                    for (var k = -i; k <= i; k++)
                    {
                        for (var l = -i; l <= i; l++)
                        {
                            if (Math.Abs(j) + Math.Abs(k) + Math.Abs(l) == i * 2 && j + k + l == 0)
                            {
                                BorderTileOffsets.Add(new Vector3(j, k, l));
                            }
                        }
                    }
                }

            }


            //Clear our list gridPositions.
            TileLocations.Clear();

            foreach (var item in TileOffsets)
            {
                Vector3 newlocation = HexCoordinateToCartesian(item, tileSideLength);
                TileLocations.Add(newlocation);
                Debug.Log("Added location " + newlocation);
            }

        }

        private Vector3 HexCoordinateToCartesian(Vector3 item, float s)
        {
            float y = ((3f / 2f) * s * item.z);
            float x = Mathf.Sqrt(3f) * s * (item.z / 2f + item.x);
            return new Vector3(x, 0, y);
        }


        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;


            foreach (Vector3 location in TileLocations)
            {

                GameObject toInstantiate = Tiles[Random.Range(0, Tiles.Count)];

                //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                GameObject instance =
                    Instantiate(toInstantiate, location, Quaternion.identity) as GameObject;
                Debug.Log("Added tile " + toInstantiate.name + " at " + location.ToString());
                //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                instance.transform.SetParent(boardHolder);
            }

        }


        ////RandomPosition returns a random position from our list gridPositions.
        //Vector3 RandomPosition()
        //{
        //    //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
        //    int randomIndex = Random.Range(0, TradeSquares.Count);

        //    //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
        //    Vector3 randomPosition = TradeSquares[randomIndex];

        //    //Remove the entry at randomIndex from the list so that it can't be re-used.
        //    TradeSquares.RemoveAt(randomIndex);

        //    //Return the randomly selected Vector3 position.
        //    return randomPosition;
        //}


        ////LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
        //void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        //{
        //    //Choose a random number of objects to instantiate within the minimum and maximum limits
        //    int objectCount = Random.Range(minimum, maximum + 1);

        //    //Instantiate objects until the randomly chosen limit objectCount is reached
        //    for (int i = 0; i < objectCount; i++)
        //    {
        //        //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
        //        Vector3 randomPosition = RandomPosition();

        //        //Choose a random tile from tileArray and assign it to tileChoice
        //        GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

        //        //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
        //        Instantiate(tileChoice, randomPosition, Quaternion.identity);
        //    }
        //}


        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene()
        {

            rings = PlayerPrefs.GetInt("RingNumberInputField");
            Debug.Log("Setting up board with " + rings+ " rings");
            //Reset our list of gridpositions.
            InitialiseList();

            //Creates the outer walls and floor.
            BoardSetup();

        }
    }
}