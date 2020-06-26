using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

namespace Completed

{

    public class BoardManager : MonoBehaviour
    {
        // Using Serializable allows us to embed a class with sub properties in the inspector.
        [Serializable]
        public class Count
        {
            public int minimum;             //Minimum value for our Count class.
            public int maximum;             //Maximum value for our Count class.


            //Assignment constructor.
            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }


        public int columns = 8;                                         //Number of columns in our game board.
        public int rows = 8;                                            //Number of rows in our game board.
        public Count wallCount = new Count(5, 9);                      //Lower and upper limit for our random number of walls per level.
        public GameObject exit;                                         //Prefab to spawn for exit.
        public GameObject[] floorTiles;                                 //Array of floor prefabs.
        public GameObject[] outerWallTiles;                             //Array of outer tile prefabs.
        public GameObject[] wallTiles;
        public GameObject[] enemyTiles;
        public GameObject Player;
        public GameObject Boss;
        public GameObject bossLife;
        //private GameObject Player1;
        public int count = 0;
        public GameObject topLeft, topRight, bottomLeft, bottomRight;
        public GameObject bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6, bottom7, bottom8, bottom9;
        public GameObject topo0, topo1, topo2, topo3, topo4, topo5, topo6, topo7, topo8, topo9;
        public GameObject parede0, parede1, parede2, parede3, parede4, parede5, parede6, parede7, parede8, parede9, parede10, parede11, parede12, parede13, parede14, parede15, parede16, parede17, parede18, parede19;
        public GameObject esquerda0, esquerda1, esquerda2, esquerda3, esquerda4, esquerda5, esquerda6, esquerda7, esquerda8, esquerda9;
        public GameObject direita0, direita1, direita2, direita3, direita4, direita5, direita6, direita7, direita8, direita9;
        private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
        private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.

        //Para meter random os barris, cenas que possa apanhar etc
        //Clears our list gridPositions and prepares it to generate a new board.
        void InitialiseList()
        {
            //Clear our list gridPositions.
            gridPositions.Clear();
            
            //Loop through x axis (columns).
            for (int x = 1; x < columns - 1; x++)
            {
                //Within each column, loop through y axis (rows).
                for (int y = 1; y < rows - 3; y++)
                {
                    //At each index add a new Vector3 to our list with the x and y coordinates of that position.
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }


        //Sets up the outer walls and floor (background) of the game board.
        void BoardSetup()
        {
            //Instantiate Board and set boardHolder to its transform.
            boardHolder = new GameObject("Board").transform;

            //Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
            for (int x = 0; x < columns ; x++)
            {
                //Loop along y axis, starting from -1 to place floor or outerwall tiles.
                for (int y = 0; y < rows -2; y++)
                {
                    //Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

                                      
                    //Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
                    GameObject instance =
                        Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                    //Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
                    instance.transform.SetParent(boardHolder);
                }
            }
        }


        //RandomPosition returns a random position from our list gridPositions.
        Vector3 RandomPosition()
        {
            //Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
            int randomIndex = Random.Range(0, gridPositions.Count);

            //Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
            Vector3 randomPosition = gridPositions[randomIndex];

            //Remove the entry at randomIndex from the list so that it can't be re-used.
            gridPositions.RemoveAt(randomIndex);

            //Return the randomly selected Vector3 position.
            return randomPosition;
        }


        //LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            //Choose a random number of objects to instantiate within the minimum and maximum limits
            int objectCount = Random.Range(minimum-1, maximum -2);

            //Instantiate objects until the randomly chosen limit objectCount is reached
            for (int i = 0; i < objectCount; i++)
            {
                //Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
                Vector3 randomPosition = RandomPosition();

                //Choose a random tile from tileArray and assign it to tileChoice
                GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];

                //Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }


        //SetupScene initializes our level and calls the previous functions to lay out the game board
        public void SetupScene(int level)
        {
            //Creates the outer walls and floor.
            BoardSetup();

            //Reset our list of gridpositions.
            InitialiseList();

            //Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

            //Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
            //LayoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

            //Determine number of enemies based on current level number, based on a logarithmic progression
            count = (int)Mathf.Log(level, 1.5f);
            //Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
            LayoutObjectAtRandom(enemyTiles, count, count);

            

            //Exit e os 4 cantos
            Instantiate(exit, new Vector3(columns - 1, rows - 3, 0f), Quaternion.identity);
            //Instantiate(topLeft, new Vector3(columns - 11, rows, 0f), Quaternion.identity);
            //Instantiate(topRight, new Vector3(columns, rows, 0f), Quaternion.identity);
            Instantiate(topLeft, new Vector3(columns - 11, rows - 1, 0f), Quaternion.identity);
            Instantiate(topRight, new Vector3(columns, rows - 1, 0f), Quaternion.identity);
            Instantiate(bottomLeft, new Vector3(columns - 11, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottomRight, new Vector3(columns, rows - 11, 0f), Quaternion.identity);
            //bottom
            Instantiate(bottom0, new Vector3(columns - 1, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom1, new Vector3(columns - 2, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom2, new Vector3(columns - 3, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom3, new Vector3(columns - 4, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom4, new Vector3(columns - 5, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom5, new Vector3(columns - 6, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom6, new Vector3(columns - 7, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom7, new Vector3(columns - 8, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom8, new Vector3(columns - 9, rows - 11, 0f), Quaternion.identity);
            Instantiate(bottom9, new Vector3(columns - 10, rows - 11, 0f), Quaternion.identity);
            //topo
            /*Instantiate(topo0, new Vector3(columns - 1, rows, 0f), Quaternion.identity);
            Instantiate(topo1, new Vector3(columns - 2, rows, 0f), Quaternion.identity);
            Instantiate(topo2, new Vector3(columns - 3, rows, 0f), Quaternion.identity);
            Instantiate(topo3, new Vector3(columns - 4, rows, 0f), Quaternion.identity);
            Instantiate(topo4, new Vector3(columns - 5, rows, 0f), Quaternion.identity);
            Instantiate(topo5, new Vector3(columns - 6, rows, 0f), Quaternion.identity);
            Instantiate(topo6, new Vector3(columns - 7, rows, 0f), Quaternion.identity);
            Instantiate(topo7, new Vector3(columns - 8, rows, 0f), Quaternion.identity);
            Instantiate(topo8, new Vector3(columns - 9, rows, 0f), Quaternion.identity);
            Instantiate(topo9, new Vector3(columns - 10, rows, 0f), Quaternion.identity);
            */        
            //direita
            //Instantiate(direita0, new Vector3(columns, rows - 1, 0f), Quaternion.identity);
            Instantiate(direita1, new Vector3(columns, rows - 2, 0f), Quaternion.identity);
            Instantiate(direita2, new Vector3(columns, rows - 3, 0f), Quaternion.identity);
            Instantiate(direita3, new Vector3(columns, rows - 4, 0f), Quaternion.identity);
            Instantiate(direita4, new Vector3(columns, rows - 5, 0f), Quaternion.identity);
            Instantiate(direita5, new Vector3(columns, rows - 6, 0f), Quaternion.identity);
            Instantiate(direita6, new Vector3(columns, rows - 7, 0f), Quaternion.identity);
            Instantiate(direita7, new Vector3(columns, rows - 8, 0f), Quaternion.identity);
            Instantiate(direita8, new Vector3(columns, rows - 9, 0f), Quaternion.identity);
            Instantiate(direita9, new Vector3(columns, rows - 10, 0f), Quaternion.identity);
            //esquerda
            //Instantiate(esquerda0, new Vector3(columns - 11, rows - 1, 0f), Quaternion.identity);
            Instantiate(esquerda1, new Vector3(columns - 11, rows - 2, 0f), Quaternion.identity);
            Instantiate(esquerda2, new Vector3(columns - 11, rows - 3, 0f), Quaternion.identity);
            Instantiate(esquerda3, new Vector3(columns - 11, rows - 4, 0f), Quaternion.identity);
            Instantiate(esquerda4, new Vector3(columns - 11, rows - 5, 0f), Quaternion.identity);
            Instantiate(esquerda5, new Vector3(columns - 11, rows - 6, 0f), Quaternion.identity);
            Instantiate(esquerda6, new Vector3(columns - 11, rows - 7, 0f), Quaternion.identity);
            Instantiate(esquerda7, new Vector3(columns - 11, rows - 8, 0f), Quaternion.identity);
            Instantiate(esquerda8, new Vector3(columns - 11, rows - 9, 0f), Quaternion.identity);
            Instantiate(esquerda9, new Vector3(columns - 11, rows - 10, 0f), Quaternion.identity);

            //parede
            Instantiate(topo0, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo1, new Vector3(columns - 2, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo2, new Vector3(columns - 3, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo3, new Vector3(columns - 4, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo4, new Vector3(columns - 5, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo5, new Vector3(columns - 6, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo6, new Vector3(columns - 7, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo7, new Vector3(columns - 8, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo8, new Vector3(columns - 9, rows - 1, 0f), Quaternion.identity);
            Instantiate(topo9, new Vector3(columns - 10, rows - 1, 0f), Quaternion.identity);
            
            //parede1
            Instantiate(parede10, new Vector3(columns - 1, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede11, new Vector3(columns - 2, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede12, new Vector3(columns - 3, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede13, new Vector3(columns - 4, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede14, new Vector3(columns - 5, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede15, new Vector3(columns - 6, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede16, new Vector3(columns - 7, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede17, new Vector3(columns - 8, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede18, new Vector3(columns - 9, rows - 2, 0f), Quaternion.identity);
            Instantiate(parede19, new Vector3(columns - 10, rows - 2, 0f), Quaternion.identity);

            
            if(GameManager.instance.level == 3)
            {
                bossLife = Instantiate(Boss, new Vector3(columns - 2, rows - 3, 0f), Quaternion.identity);
            }
            
            Instantiate(Player ,new Vector3(columns - 10, rows - 10, 0f), Quaternion.identity);

           
        }
    }
}