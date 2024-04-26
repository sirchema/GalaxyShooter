using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    [SerializeField]
    private float _speed = 10.0f;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
		//move up at 10 speed
		transform.Translate(Vector3.up * _speed * Time.deltaTime);
		DestroyLaser();		
	}

	private void DestroyLaser()
	{
        if (transform.position.y > 5.5f)
        {
            if (this.transform.parent != null)
            {
                Destroy(this.transform.parent.gameObject);
            }

            Destroy(this.gameObject);			
        }
    }
}
