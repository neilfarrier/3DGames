using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    public enum State
    {
        Alive, 
        Dying, 
        Transcending
    }
    State state = State.Alive;
    [SerializeField]
    float levelLoadDelay = 2f;

    [Header("Movement")]
    [SerializeField]
    private float power;
    [SerializeField]
    private float rotatePower;
    private Rigidbody rb;
    private Vector3 thrust = Vector3.up;

    [Header("Audio")]
    private AudioSource thrustAudioSource;
    [SerializeField]
    private AudioClip engineAudioClip;
    [SerializeField]
    private AudioClip successAudioClip;
    [SerializeField]
    private AudioClip deathAudioClip;

    [Header("Particles")]
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustAudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(state == State.Alive)
        {
            RespondToRotateInput();
            RespondToThrustInput();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive)
            return;

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        thrustAudioSource.Stop();
        thrustAudioSource.PlayOneShot(successAudioClip);
        successParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        thrustAudioSource.Stop();
        thrustAudioSource.PlayOneShot(deathAudioClip);
        deathParticles.Play();
        Invoke("LoadFirstScene", levelLoadDelay);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void RespondToRotateInput()
    {
        rb.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward, rotatePower * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward, rotatePower * Time.deltaTime);
        }

        rb.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            thrustAudioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rb.AddRelativeForce(thrust * power);
        if (!thrustAudioSource.isPlaying)
            thrustAudioSource.PlayOneShot(engineAudioClip);
        mainEngineParticles.Play();
    }
}
