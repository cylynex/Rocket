using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    Rigidbody rigidBody;
    AudioSource audioSource;

    [Header("Ship Values")]
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float thrustPower = 50f;

    [Header("Sounds")]
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip winSound;

    [Header("Particles")]
    [SerializeField] ParticleSystem mainEngineEffects;
    [SerializeField] ParticleSystem loseEffect;
    [SerializeField] ParticleSystem winEffect;

    // TODO - These shoudl all go into a SO for level variables.
    [Header("Level Values")]
    [SerializeField] float levelLoadTime = 3f;
    [SerializeField] float currentFuel = 100f;
    [SerializeField] float startingFuel = 100f;
    [SerializeField] float fuelConsumptionRate = 10f;

    enum State { Alive, Dying, Transcending, Testing };
    [SerializeField] State currentState = State.Alive;

    GameController gc;
    FuelBarController fbc;

    private void Start() {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        // Fuel Stuff
        fbc = GameObject.FindGameObjectWithTag("FuelController").GetComponent<FuelBarController>();
        currentFuel = startingFuel;
        fbc.AdjustFuel(currentFuel);

        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {

        if (currentState == State.Alive || currentState == State.Testing) {
            Thrust();
            Rotate();
        }

        CheckForDebugInput();
    }


    void CheckForDebugInput() {
        if (Input.GetKeyDown(KeyCode.L)) {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            if (currentState == State.Testing) {
                currentState = State.Alive;
                print("Test Mode Disabled");
            }
            else {
                currentState = State.Testing;
                print("Test Mode Enabled");
            }
        }
    }


    void Thrust() {
        if (currentState == State.Alive || currentState == State.Testing) {
            if (Input.GetKey(KeyCode.Space)) {
                ApplyThrust();
                UseFuel();
            } else {
                audioSource.Stop();
                mainEngineEffects.Stop();
            }
        }
    }

    void ApplyThrust() {
        rigidBody.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineEffects.Play();
    }

    void UseFuel() {
        currentFuel -= fuelConsumptionRate;
        if (currentFuel <= 0) {
            currentFuel = 0;
            RunOutOfGas();
        }

        fbc.AdjustFuel(currentFuel);
    }

    private void OnCollisionEnter(Collision collision) {

        if (currentState != State.Alive) { return; }

        switch (collision.gameObject.tag) {
            case "TakeOffPad":
                break;
            case "Fuel":
                break;
            case "LandingPad":
                WinLevel();
                break;
            default:
                LoseLevel();
                break;
        }
    }
    
    void RunOutOfGas() {
        currentState = State.Dying;
        SetSound(loseSound);
        mainEngineEffects.Stop();
        //loseEffect.Play();
        Invoke("ReloadLevel", levelLoadTime);
    }


    void SetSound(AudioClip soundToPlay) {
        audioSource.Stop();
        audioSource.PlayOneShot(soundToPlay);
    }


    void Rotate() {
        rigidBody.freezeRotation = true;  // keep it from spinnign out of control

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        rigidBody.freezeRotation = false;
    }

    void LoadFirstLevel() {
        SceneManager.LoadScene(0);
    }

    void LoadNextLevel() {
        GameController.levelIndex++;
        string nextLevel = gc.levelSceneNames[GameController.levelIndex];
        print("loading next level: " + nextLevel);
        SceneManager.LoadScene(nextLevel);
    }

    void ReloadLevel() {
        //string loadLevel = gc.levelSceneNames[GameController.levelIndex];
        string loadLevel = gc.levelSceneNames[2];
        SceneManager.LoadScene(loadLevel);
    }

    // TODO move to different script doesnt bleong here
    void WinLevel() {
        currentState = State.Transcending;
        SetSound(winSound);
        rigidBody.freezeRotation = true;
        winEffect.Play();
        Invoke("LoadNextLevel", levelLoadTime);
    }


    void LoseLevel() {
        currentState = State.Dying;
        SetSound(loseSound);
        mainEngineEffects.Stop();
        loseEffect.Play();
        Invoke("ReloadLevel", levelLoadTime);
    }

}
