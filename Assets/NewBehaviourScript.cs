using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NewBehaviourScript : MonoBehaviour
{
    string note = "";
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(Input.GetButtonDown("Jump"))
        {
            note += currentTime + "\n";
        }
        if(Input.GetButtonDown("Fire2"))
        {
            File.WriteAllText(Application.dataPath + "/note.txt", note);
        }
    }
}
