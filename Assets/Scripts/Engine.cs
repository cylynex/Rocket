using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Engine : MonoBehaviour {

    Rigidbody2D rb;
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
    [SerializeField] float fuelConsumptionRate = 1f;

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

        rb = GetComponent<Rigidbody2D>();
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
        
        if (Input.GetKeyDown(KeyCode.T)) {
            fbc.AdjustFuel(0.5f);
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
        rb.AddRelativeForce(Vector2 .up * thrustPower * Time.deltaTime);
        if (!audioSource.isPlaying) {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineEffects.Play();
    }

    void UseFuel() {
        currentFuel -= fuelConsumptionRate * Time.deltaTime;
        if (currentFuel <= 0) {
            currentFuel = 0;
            RunOutOfGas();
        }

        float barFuel = currentFuel / startingFuel;
        fbc.AdjustFuel(barFuel);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (currentState != State.Alive) { return; }

        switch (collision.gameObject.tag) {
            case "TakeOffPad":
                break;
            case "Fuel":
                break;
            case "LandingPad":
                WinLevel();
                break;
            case "Obstacle":
                LoseLevel();
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
        rb.freezeRotation = true;  // keep it from spinnign out of control

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        rb.freezeRotation = false;
    }

    void WinLevel() {
        currentState = State.Transcending;
        SetSound(winSound);
        rb.freezeRotation = true;
        winEffect.Play();
        //Invoke("LoadNextLevel", levelLoadTime);
    }


    void LoseLevel() {
        currentState = State.Dying;
        SetSound(loseSound);
        mainEngineEffects.Stop();
        loseEffect.Play();
        Invoke("ReloadLevel", levelLoadTime);
    }

    void ReloadLevel() {
        gc.LoadLevel("Reload");
    }

}
