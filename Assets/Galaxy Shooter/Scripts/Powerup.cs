using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class Powerup : MonoBehaviour {

	[SerializeField]
	private float _speed = 3.0f;

	[SerializeField]
	private int _powerupID; // 0 = triple shot, 1 = speed boost, 2 = shields
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("Collided with: " + other.name);

		if(other.tag == "Player")
		{
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
				//enable triple shot
				if(_powerupID == 0)
				{
                    player.TripleShotPowerupOn();
                }else if (_powerupID == 1)
                {
                    //enable speed boost
					player.SpeedBoostPowerupOn();
                }else if (_powerupID == 2)
				{
					//enable shields
					player.EnabledShields();
                }
            }
			
            Destroy(this.gameObject);
        }		
	}
}
