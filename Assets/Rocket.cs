using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 10f;
    [SerializeField] private float mainThrust = 10f;
    [SerializeField] private float levelLoadDelay = 2f;

    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip loadLevelSound;

    [SerializeField] private ParticleSystem mainEngineParticles;
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private ParticleSystem loadLevelParticles;

    private Rigidbody _rigidbody;
    private AudioSource _audioSource;

    enum State
    {
        Alive,
        Dying,
        Transending
    }

    private State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (state != State.Alive)
        {
            return;
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
            {
                break;
            }
            case "Finish":
            {
                FinishLevel();
                break;
            }
            default:
                StartDeathSequence();
                break;
        }
    }

    private void FinishLevel()
    {
        state = State.Transending;
        _audioSource.Stop();
        _audioSource.PlayOneShot(loadLevelSound);
        loadLevelParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    { 
        state = State.Dying;
        _audioSource.Stop();
        _audioSource.PlayOneShot(deathSound);
        deathParticles.Play();
        Invoke("GameOver", levelLoadDelay);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1); // todo allow for more than 2 levels
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            _audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        _rigidbody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(mainEngine);
        }

        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
    {
        _rigidbody.freezeRotation = true; // take manual control of rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        _rigidbody.freezeRotation = false; // resume physics control of rotation
    }
}