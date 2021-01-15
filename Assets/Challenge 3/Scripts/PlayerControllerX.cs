using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
	public bool gameOver;

	public float floatForce;
	private float gravityModifier = 1.5f;
	private Rigidbody playerRb;

	public ParticleSystem explosionParticle;
	public ParticleSystem fireworksParticle;

	private AudioSource playerAudio;
	public AudioClip moneySound;
	public AudioClip explodeSound;

	private GameObject bg;

	// Start is called before the first frame update
	void Start()
	{
		Physics.gravity *= gravityModifier;
		playerAudio = GetComponent<AudioSource>();
		playerRb = GetComponent<Rigidbody>();
		bg = GameObject.FindGameObjectsWithTag("Background")[0];


		// Apply a small upward force at the start of the game
		playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

	}

	// Update is called once per frame
	void Update()
	{
		// While space is pressed and player is low enough, float up
		if (Input.GetKey(KeyCode.Space) && gameObject.transform.position.y < (bg.GetComponent<BoxCollider>().size.y - GetComponent<BoxCollider>().size.y * 8.0f) && !gameOver)
		{
			playerRb.AddForce(Vector3.up * floatForce);
		}
		if (gameObject.transform.position.y > (bg.GetComponent<BoxCollider>().size.y - GetComponent<BoxCollider>().size.y * 8.0f))
		{
			playerRb.AddForce(playerRb.velocity * -0.9f);
			playerRb.AddForce(Vector3.down * Mathf.Pow(gameObject.transform.position.y - (bg.GetComponent<BoxCollider>().size.y - GetComponent<BoxCollider>().size.y), 2));
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		// if player collides with bomb, explode and set gameOver to true
		if (other.gameObject.CompareTag("Bomb"))
		{
			explosionParticle.Play();
			playerAudio.PlayOneShot(explodeSound, 1.0f);
			gameOver = true;
			Debug.Log("Game Over!");
			Destroy(other.gameObject);
		}

		// if player collides with money, fireworks
		else if (other.gameObject.CompareTag("Money"))
		{
			fireworksParticle.Play();
			playerAudio.PlayOneShot(moneySound, 1.0f);
			Destroy(other.gameObject);

		}

	}

}
