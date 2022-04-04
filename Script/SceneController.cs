using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SceneController : MonoBehaviour
{
    public List<PlayableDirector> scene;


    public void playScene()
    {
        scene[QuestController.questIdx].Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
