using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private float _fireRate = 0.25f;
    
    private float _canFire = 0.0f;
    
    [SerializeField]
	private float _speed = 5.0f;

	// Use this for initialization
	void Start () {
		//Debug.Log(transform.position);
		
		//current pos = new position
		transform.position = new Vector3(0,0,0);
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
        Debug.Log("Entra Shoot");
        if (Time.time > _canFire)
        {
            //spawn my laser
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
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
}
