using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

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
    private bool isCollisionDetect = true;
    
    private bool isTransitioning = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }

        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            isCollisionDetect = !isCollisionDetect;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning)
        {
            return;
        }

        if (!isCollisionDetect) return;

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
        isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(loadLevelSound);
        loadLevelParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(deathSound);
        deathParticles.Play();
        Invoke("GameOver", levelLoadDelay);
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex;
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            nextSceneIndex = 0;
        }
        else
        {
            nextSceneIndex = currentSceneIndex + 1;
        }

        SceneManager.LoadScene(nextSceneIndex); // todo allow for more than 2 levels
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

    private void StopApplyingThrust()
    {
        _audioSource.Stop();
        mainEngineParticles.Stop();
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
        _rigidbody.angularVelocity = Vector3.zero;

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
    }
}