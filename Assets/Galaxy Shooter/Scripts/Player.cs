using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;
    
    private float _canFire = 0.0f;
    
    [SerializeField]
	private float _speed = 5.0f;

    [SerializeField]
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    private GameManager _gameManager;

    private UIManager _uiManager;

    private SpawnManager _spawnManager;

    public bool canTripeShot = false;

    public bool canSpeedBoost = false;

    public int lives = 3;

    public bool shieldActive = false;

	// Use this for initialization
	void Start () {
		//Debug.Log(transform.position);
		
		//current pos = new position
		transform.position = new Vector3(0,0,0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if(_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        }

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }


	}
	
	// Update is called once per frame
	void Update () {

        Movement();
        ScreenLimits();

        //if space key pressed
        //spawn laser at player position
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
	}

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if(canTripeShot)
            {
                //SpawnLaser(_tripleShotPrefab, 0f, 0f);
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

                //Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                //Instantiate(_laserPrefab, transform.position + new Vector3(0.55f, 0.06f, 0), Quaternion.identity);
                //Instantiate(_laserPrefab, transform.position + new Vector3(-0.55f, 0.06f, 0), Quaternion.identity);
            } else
            {
                //spawn my laser
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (canSpeedBoost)
        {
            transform.Translate(Vector3.right * _speed * 2f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 2f * verticalInput * Time.deltaTime);
        } else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        
    }

	private void ScreenLimits()
	{
        //if player on the y is greater than 0
        //set player position on the Y to 0

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.3f)
        {
            transform.position = new Vector3(-9.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.31f)
        {
            transform.position = new Vector3(9.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        //subtract 1 life from the player
        
        if(shieldActive)
        {
            shieldActive = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        lives--;
        _uiManager.UpdateLives(lives);

        //if lives < 1 (meaning 0)
        //destroy this object
        if(lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }

    }

    public void TripleShotPowerupOn()
    {
        canTripeShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripeShot = false;
    }

    public void SpeedBoostPowerupOn()
    {
        canSpeedBoost = true;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedBoost = false;
    }

    public void EnabledShields()
    {
        shieldActive = true;
        _shieldGameObject.SetActive(true);
    }
}
