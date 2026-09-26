using UnityEngine;

public class balls : MonoBehaviour
{
	AudioSource _source;
	[SerializeField] private AudioClip _brickHit;
	[SerializeField] private AudioClip _die;
	[SerializeField] private AudioClip _wallHit;
	[SerializeField] private AudioClip _paddleHit;
    public float minY = -5.5f;
    //public float maxVelocity = 15f;
    private Rigidbody2D _rb;
	[SerializeField] private float _speedIncrement = 1.1f;
	[SerializeField] float _paddleInfluence = 0.8f;
	[SerializeField] private float _launchForce = 7.0f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		_source = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
		Vector2 direction = Random.insideUnitCircle.normalized;
		_rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }

	 void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("paddle"))
        {
			_source.PlayOneShot(_paddleHit);
            if (!Mathf.Approximately(collision.rigidbody.linearVelocityY, 0.0f))
            {
                //we compute direction using a weighted sum, where the weights ise a one-minus to be determined
                Vector2 direction = _rb.linearVelocity * (1 - _paddleInfluence)
                                    + collision.rigidbody.linearVelocity * _paddleInfluence;
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction.normalized * _speedIncrement;
            }
        }
		if (collision.gameObject.CompareTag("brick"))
		{
			Destroy(collision.gameObject);	
			_source.PlayOneShot(_brickHit);
			GameBehavior.Instance.ScorePoint(0);	
		}
		if  (collision.gameObject.CompareTag("wall"))
		{
		_source.PlayOneShot(_wallHit);
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
			_source.PlayOneShot(_die);
            ResetBall();
        }

        //if (_rb.linearVelocity.magnitude > maxVelocity)
       // {
       //     _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, maxVelocity);
       // }
    }
    void ResetBall()
    {
        //stop ball
        _rb.linearVelocity = Vector2.zero;
        //respawn at center
        transform.position = Vector3.zero;
        Vector2 direction = Random.insideUnitCircle.normalized;
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }
}
