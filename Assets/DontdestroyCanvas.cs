using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontdestroyCanvas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
