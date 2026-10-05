using UnityEngine;

public class balls : MonoBehaviour
{
	AudioSource _source;
	[SerializeField] private AudioClip _brickHit;
	[SerializeField] private AudioClip _die;
	[SerializeField] private AudioClip _wallHit;
	[SerializeField] private AudioClip _paddleHit;
    public float minY = -5.5f;
    public float maxVelocity = 25f;
    private Rigidbody2D _rb;
	[SerializeField] private float _speedIncrement = 1.1f;
	[SerializeField] private float _minSpeed = 1.1f;
	[SerializeField] float _paddleInfluence = 0.8f;
	[SerializeField] private float _launchForce = 7.0f;
	[SerializeField, Range(0.0f, 1.0f)] private float _steepnessThreshold = 0.25f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		_source = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
		Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0.1f, 1f)).normalized;
		_rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }

	 void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("paddle"))
        {
			_source.PlayOneShot(_paddleHit);
            if (!Mathf.Approximately(collision.rigidbody.linearVelocityY, 0.0f))
            {
			Vector2 direction = _rb.linearVelocity * (1 - _paddleInfluence)
                                    + collision.rigidbody.linearVelocity * _paddleInfluence;
                direction.Normalize();
                CheckSteepness(ref direction);
                _rb.linearVelocity = _rb.linearVelocity.magnitude * direction * _speedIncrement;
            }
        }
		if (collision.gameObject.CompareTag("brick"))
		{
            if (collision.gameObject.TryGetComponent(out BrickHealth brickHP))
            {
                brickHP.Hit();
                _source.PlayOneShot(_brickHit);
                GameBehavior.Instance.ScorePoint(0);
                _rb.linearVelocity *= _speedIncrement;
            }
		}

		if  (collision.gameObject.CompareTag("wall"))
		{
		_source.PlayOneShot(_wallHit);
        _rb.linearVelocity *= _speedIncrement;
		}
    }

    // Update is called once per frame
    void Update()
    {
		_rb.simulated = GameBehavior.Instance.State == Utilities.GameState.Play;

        if (transform.position.y < minY)
        {
			_source.PlayOneShot(_die);
            if (GameBehavior.Instance.Death())
            {
                ResetBall();
            }
        }

        if (_rb.linearVelocity.sqrMagnitude > 0f && _rb.linearVelocity.magnitude < _minSpeed)
        {
            _rb.linearVelocity = _rb.linearVelocity.normalized * _minSpeed;
        }
        if (_rb.linearVelocity.magnitude > maxVelocity)
        {
            _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, maxVelocity);
        }
    }

    void ResetBall()
    {
        //stop ball
        _rb.linearVelocity = Vector2.zero;
        //respawn at center
        transform.position = Vector3.zero;
        Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(0.1f, 1f)).normalized;
        _rb.AddForce(direction * _launchForce, ForceMode2D.Impulse);
    }

	private void CheckSteepness(ref Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < _steepnessThreshold)
        {
            direction.x += 0.5f * Mathf.Sign(direction.x);
            direction.Normalize();
        }
    }
}
