using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleSceneController : MonoBehaviour {
    public string NextScene = "GameScene";
    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(NextScene);
        }

        //Adds a small rotation effect until the user presses the SPACE key
        Vector3 vector = new Vector3(0f, 2f * Time.deltaTime, 0f);
        transform.Rotate(vector);
    }
}
