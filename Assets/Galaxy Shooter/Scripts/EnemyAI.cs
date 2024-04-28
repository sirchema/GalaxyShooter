using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [SerializeField]
    private float _speed = 4.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _audioClip;

    // Use this for initialization
    void Start () {
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}

    // Update is called once per frame
    void Update() {
        Movement();
	}

    private void Movement()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //when off the screen on the bottom
        //respawn back on top with a new x position between the bounds of the screen
        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-7.5f, 7.5f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            if(other.transform.parent != null)
            {
                Destroy(other.transform.parent.gameObject);
            }
            Destroy(other.gameObject);            
        }else if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                player.Damage();
            }            
        }

        Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
        _uiManager.UpdateScore();
        AudioSource.PlayClipAtPoint(_audioClip, Camera.main.transform.position, 1f);
        Destroy(this.gameObject);
    }
}
