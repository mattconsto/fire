using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {
    public enum State {ALIVE, BURNING, DEAD, RESPAWNING};

    public float timer   = 1f;
    public float partial = 0f;
    public State state   = State.ALIVE;
    public bool  respawn = false;
    public float spawn   = 0f;

    public GameController controller;

	public void Start() {
        SetParticalSystemRate(false);
	}
	
	public void Update() {
        if(state == State.BURNING) {
            timer -= Time.deltaTime;
            if(timer < partial) {
                if(GetComponent<MeshRenderer>() != null) GetComponent<MeshRenderer>().enabled = false;
                if(GetComponent<MeshCollider>() != null) GetComponent<MeshCollider>().enabled = false;
                SetParticalSystemRate(false);
                partial = 0f;
            }
            if(timer < 0) {
                if(!respawn) {
                    enabled = false;
                    state = State.DEAD;
                } else {
                    spawn = 30f;
                    state = State.RESPAWNING;
                }
            }
        } else if(state == State.RESPAWNING) {
            spawn -= Time.deltaTime;
            if(spawn < 0) {
                if (GetComponent<MeshRenderer>() != null) GetComponent<MeshRenderer>().enabled = true;
                if (GetComponent<MeshCollider>() != null) GetComponent<MeshCollider>().enabled = true;
                timer = 10;
                partial = 5;
                state = State.ALIVE;
            }
        }
	}

    public void SetParticalSystemRate(bool enable) {
        if (GetComponent<ParticleSystem>() != null) {
            ParticleSystem.EmissionModule em = GetComponent<ParticleSystem>().emission;
            em.enabled = enable;
        }
    }

    public void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.tag == "Fire") {
            if (state == State.ALIVE) {
                state = State.BURNING;
                SetParticalSystemRate(true);
                if (controller != null) controller.Increment();
                Destroy(collision.gameObject);
                controller.crackleTarget = 1;
            }
        }
    }
}
