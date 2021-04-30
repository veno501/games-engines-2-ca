using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject droneFormationPrefab;
    public GameObject droneArriveHuman;
    public GameObject droneArriveAlien;
    public GameObject mediumExplosion;
    public bool spawnDroneTrigger = false;

    public static SceneController singleton;


    void Start() {
        singleton = this;

        GameObject.Find("Music").GetComponent<AudioSource>().time += 100f;

        StartCoroutine(SpawnDroneShot2());
        StartCoroutine(SpawnDroneShot3());
        StartCoroutine(SpawnDroneShot4());
        StartCoroutine(SpawnDroneShot5());
    }
    
    void Update() {
        if (spawnDroneTrigger) {
            spawnDroneTrigger = false;

            // StartCoroutine(SpawnDroneFormation(GameObject.Find("drone spawn 1"), 10.5f));
        }
    }

    public IEnumerator SpawnDroneShot2() {
        GameObject carrier = GameObject.Find("drone spawn 1");
        GameObject prefab = droneArriveAlien;
        yield return new WaitForSeconds(10.5f);

        Random.InitState(3468920);
        
        for (int i = 0; i < 12; i++) {
            yield return new WaitForSeconds(0.13f);
            GameObject ob = (GameObject)Instantiate(prefab, carrier.transform.position, carrier.transform.rotation);
            ob.GetComponent<Arrive>().targetGameObject = GameObject.Find("frigate 1");
            ob.GetComponent<Boid>().velocity = ob.transform.forward*35f+Random.insideUnitSphere*5f;
            ob.name = "alien drone " + i;
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 12; i++) {
            yield return new WaitForSeconds(0.13f);
            GameObject ob = (GameObject)Instantiate(prefab, carrier.transform.position, carrier.transform.rotation);
            ob.GetComponent<Arrive>().targetGameObject = GameObject.Find("frigate 1");
            ob.GetComponent<Boid>().velocity = ob.transform.forward*35f+Random.insideUnitSphere*5f;
            ob.name = "alien drone " + (12+i);
        }
    }

    public IEnumerator SpawnDroneShot3() {
        GameObject carrier = GameObject.Find("drone spawn 2");
        GameObject carrier2 = GameObject.Find("drone spawn 3");
        GameObject prefab = droneArriveHuman;
        yield return new WaitForSeconds(20f);

        Random.InitState(3468920);
        
        for (int i = 0; i < 24; i += 2) {
            yield return new WaitForSeconds(0.3f);
            GameObject ob = (GameObject)Instantiate(prefab, carrier.transform.position, carrier.transform.rotation);
            ob.GetComponent<Arrive>().targetGameObject = GameObject.Find("alien carrier 1");
            ob.GetComponent<Boid>().velocity = ob.transform.forward*30f+Random.insideUnitSphere*10f;
            ob.name = "human drone " + i;

            GameObject ob2 = (GameObject)Instantiate(prefab, carrier2.transform.position, carrier2.transform.rotation);
            ob2.GetComponent<Arrive>().targetGameObject = GameObject.Find("alien carrier 1");
            ob2.GetComponent<Boid>().velocity = ob.transform.forward*30f+Random.insideUnitSphere*10f;
            ob2.name = "human drone " + (i+1);

            if (i == 2) GameObject.Find("CM vcam2").GetComponent<Cinemachine.CinemachineVirtualCamera>().m_LookAt = ob.transform;
        }

        for (int i = 0; i < 24; i++) {
            // GameObject hD = GameObject.Find("human drone " + i);
            // GameObject aD = GameObject.Find("alien drone " + i);
            // hD.GetComponent<Arrive>().targetGameObject = aD;
            // aD.GetComponent<Arrive>().targetGameObject = hD;
            GameObject.Find("human drone " + i).GetComponent<Arrive>().targetGameObject = GameObject.Find("alien drone " + Random.Range(0, 24));
            GameObject.Find("alien drone " + i).GetComponent<Arrive>().targetGameObject = GameObject.Find("human drone " + Random.Range(0, 24));
        }
        yield return new WaitForSeconds(2f);
        GameObject.Find("CM vcam2").GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Follow = GameObject.Find("human drone 2").transform;
    }

    public IEnumerator SpawnDroneShot4() {
        yield return new WaitForSeconds(50f);

        GameObject.Find("frigate 2").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("frigate 4").SetActive(false);

        yield return new WaitForSeconds(15f);

        GameObject.Find("frigate 2").GetComponent<Boid>().enabled = true;
    }

    public IEnumerator SpawnDroneShot5() {
        yield return new WaitForSeconds(74f);

        foreach (Transform c in GameObject.Find("explosion 1").transform) {
            yield return new WaitForSeconds(0.5f);
            c.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(4f);

        for (int i = 0; i < 24; i++) {
            yield return new WaitForSeconds(0.2f);
            GameObject d = GameObject.Find("alien drone " + i);
            Instantiate(mediumExplosion, d.transform.position, d.transform.rotation);
            Destroy(d);
            if (i != 0) GameObject.Find("human drone " + (i-1)).transform.GetChild(2).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("human drone " + 23).transform.GetChild(2).gameObject.SetActive(false);
    }

    // public IEnumerator SpawnDroneFormation(GameObject carrier, float delay) {
    //     yield return new WaitForSeconds(delay);
    //     Debug.Log("spawning drone at " + carrier.name);
    //     GameObject ob = (GameObject)Instantiate(droneFormationPrefab, carrier.transform.position, carrier.transform.rotation);
    // }
}
