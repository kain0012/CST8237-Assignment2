using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSceneController : MonoBehaviour
{
    public GameplayHUD GameplayHUD;
    private float gameStartTime;
    private int currentLevel;
    private GameObject[][] levelsList;
    private float currentLevelStartTime;
    public string NextScene = "TitleScene";
    private GameObject player;
    public int PlayerLives = 5;
    public GameObject PlayerPrefab;
    public GameObject RockLarge;
    public GameObject RockMedium;
    public GameObject RockSmall;
    private ArrayList rocksList;
    private int currentScore;
    public AudioClip LevelClearedSound;

    public void AddRock(GameObject rock)
    {
        rocksList.Add(rock);
    }

    private void AddRocks()
    {
        currentLevelStartTime = Time.time + 1f;
        for (int i = 0; i < levelsList[currentLevel].Length; i++)
        {
            //generates a random position to create the asteroid
            Vector3 vector = new Vector3(Random.Range(-175, 175), 0f, Random.Range(-125, 125));
            Quaternion quaternion = new Quaternion();
            
            //creates the asterorid(rock) instance
            GameObject newRock = Instantiate(levelsList[currentLevel][i], vector, quaternion) as GameObject;
            
            //checks if the new rock is out of the bounds in the X axis
            if ((newRock.transform.position.x > -30f) && 
                (newRock.transform.position.x < 30f))
            {
                //set the position to the X bound
                Vector3 newPosX = new Vector3(30f, 0f, newRock.transform.position.z);
                newRock.transform.position = newPosX;
            }

            //checks if the new rock is out of the bounds in the Z axis
            if ((newRock.transform.position.z > -30f) && 
                (newRock.transform.position.z < 30f))
            {
                //set the position to the Z bound
                Vector3 newPosZ = new Vector3(newRock.transform.position.x, 0f, 30f);
                newRock.transform.position = newPosZ;
            }

            //adds the rock to the list
            rocksList.Add(newRock);
        }
    }

    private IEnumerator GameOver(string message)
    {
        //shows the player's score
        GameplayHUD.UpdateInfo(message, string.Format("Final Score: {0}", currentScore));
        
        //waits for 3 seconds
        yield return new WaitForSeconds(3f);

        //goes tho the next screen
        SceneManager.LoadScene(NextScene);
    }

    public void RemoveRock(GameObject rock)
    {
        //Removes the rock from the list
        rocksList.Remove(rock);
        
        //Adds the rock's points to the player's socre
        currentScore += rock.GetComponent<Rock>().Points;
        
        //Shows the player's score
        GameplayHUD.UpdateScore(currentScore.ToString());
    }

    private void Start()
    {
        //creates the different levels
        //Every level contains a different list of rocks
        GameObject[][] objArrayArray1 = new GameObject[5][];
        objArrayArray1[0] = new GameObject[] { RockSmall, RockMedium, RockLarge };
        objArrayArray1[1] = new GameObject[] { RockSmall, RockSmall, RockSmall, RockMedium, RockLarge };
        objArrayArray1[2] = new GameObject[] { RockSmall, RockSmall, RockMedium, RockMedium, RockLarge, RockLarge };
        objArrayArray1[3] = new GameObject[] { RockLarge, RockLarge, RockSmall, RockLarge, RockLarge };
        objArrayArray1[4] = new GameObject[] { RockMedium, RockMedium, RockMedium, RockLarge, RockLarge, RockLarge };
        levelsList = objArrayArray1;
        rocksList = new ArrayList();
        
        //Creates the rock instances
        AddRocks();
    }

    private void Update()
    {
        if (player == null)
        {
            //check if player is alive
            if (PlayerLives > 0)
            {
                //Decrease player's lives
                PlayerLives--;

                //Show's the current player's lives
                GameplayHUD.UpdateLives(PlayerLives.ToString());

                //instantiante the player at the origin without rotation
                Vector3 vector = new Vector3(0f, 0f, 0f);
                player = Instantiate(PlayerPrefab, vector, new Quaternion()) as GameObject;
            }
            else
            {
                //starts the Game over routine
                StartCoroutine(GameOver("Game Over!"));
            }
        }
        else
        {
            //checks if the player destroyed all the rocks
            if ((rocksList.Count == 0) && !GameplayHUD.InfoBackground.enabled)
            {
                //increases the current level
                currentLevel++;
                if (currentLevel <= levelsList.Length)
                {
                    //calculates a time bonues
					int num = Mathf.RoundToInt(((Time.time / currentLevelStartTime) * 1000f) * currentLevel);
                    currentScore += num;
					//shows the socre
                    GameplayHUD.UpdateScore(currentScore.ToString());
                    
					//shows the level cleared message
					GameplayHUD.UpdateInfo("Level " + currentLevel + " Completed", " Bonus: " + num);
                    
					//plays the level cleared sound
					GetComponent<AudioSource>().PlayOneShot(LevelClearedSound);
					
					//increases the start time
                    gameStartTime = Time.time + 1f;
                }
                else
                {
					//if all the levels were cleared
                    StartCoroutine(GameOver("Victory!!!"));
                }
            }
            if (((Time.time > (gameStartTime + 1f)) && 
				(currentLevel <= levelsList.Length)) && 
				GameplayHUD.InfoBackground.enabled)
            {
				//hides the UI messages and add asteroids
                GameplayHUD.HideInfo();
                AddRocks();
            }
        }
    }
}