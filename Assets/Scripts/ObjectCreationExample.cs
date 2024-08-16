using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;

public class ObjectCreationExample : MonoBehaviour
{
    public Transform ourObject;
    public List<Transform> blackHoles;
    public bool course = true;

    private List<Transform> cubes;

    private Transform object1;
    private Transform object2;
    private Transform object3;
    private Transform object4;

    public int numberOfObjects = 500;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (course)
        {
            // Course();
        }
        else
        {
            Fun();
        }   
    }

    void Course()
    {
        object1 = Instantiate(ourObject);
        object1.position = new Vector3(-3f, 0f, 0f);

        object2 = Instantiate(ourObject);
        object2.position = new Vector3(-1.5f, 0f, 0f);
        object2.eulerAngles = new Vector3(0f, 0f, 10f);

        object3 = Instantiate(ourObject);
        object3.position = new Vector3(1.5f, 0f, 0f);
        object3.localScale = new Vector3(1.0f, 2.0f, 1.0f);

        object4 = Instantiate(ourObject);
        object4.position = new Vector3(3f, 0f, 0f);

        /*Matrix4x4 shear = new Matrix4x4(new Vector4(1f, 0f, 0f, 0f),
                                        new Vector4(0f, 1f, 0f, 0f),
                                        new Vector4(0f, 0.5f, 1f, 0f),
                                        new Vector4(0f, 0f, 0f, 1f));*/
        object2.parent = object1;
        object3.parent = object1;
        object4.parent = object1;
        object1.eulerAngles = new Vector3(0f, 0f, 10f);
    }

    void Fun()
    {
        cubes = new List<Transform>();
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomLocationOnSphere = Random.onUnitSphere * 15f;
            Quaternion randomRotation = Random.rotation;
            cubes.Add(Instantiate(ourObject, randomLocationOnSphere, randomRotation));
            Color randomColor = Random.ColorHSV();
            cubes[i].GetComponent<MeshRenderer>().material.color = randomColor;
            cubes[i].GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", randomColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (course)
        {

        }
        else
        {
            Matrix4x4 rotationYby90 = new Matrix4x4(new Vector4(0f, 0f, 1f, 0f),
                                                    new Vector4(0f, 1f, 0f, 0f),
                                                    new Vector4(-1f, 0f, 0f, 0f),
                                                    new Vector4(0f, 0f, 0f, 1f));
            Matrix4x4 rotationZby90 = new Matrix4x4(new Vector4(0f, -1f, 0f, 0f),
                                                    new Vector4(1f, 0f, 0f, 0f),
                                                    new Vector4(0f, 0f, 1f, 0f),
                                                    new Vector4(0f, 0f, 0f, 1f));
            /*Matrix4x4 rotationYby90 = new Matrix4x4(new Vector4(Mathf.Cos(time/6f), 0f, Mathf.Sin(time/6f), 0f),
                                                    new Vector4(0f, 1f, 0f, 0f),
                                                    new Vector4(-Mathf.Sin(time/6f), 0f, Mathf.Cos(time/6f), 0f),
                                                    new Vector4(0f, 0f, 0f, 1f));*/
            for (int i = 0; i < cubes.Count; i++)
            {
                for (int j = 0; j < blackHoles.Count; j++)
                {
                    float magnitute = (cubes[i].position - blackHoles[j].transform.position).magnitude;
                    Vector3 temp = Vector3.ProjectOnPlane(cubes[i].position - blackHoles[j].transform.position, new Vector3(0, 1, 0));
                    // cubes[i].GetComponent<Rigidbody>().AddForce(rotationYby90.MultiplyVector(rotationZby90.MultiplyVector(new Vector4(temp.x, temp.y, temp.z, 1))) * 36 / magnitute);
                    cubes[i].GetComponent<Rigidbody>().AddForce(rotationYby90.MultiplyVector(new Vector4(temp.x, temp.y, temp.z, 1)) * 36 / magnitute);
                    cubes[i].GetComponent<Rigidbody>().AddForce(blackHoles[j].transform.position - cubes[i].position);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Transform temp = Instantiate(ourObject, new Vector3(0f, 10f, 0f), Random.rotation);
                Color randomColor = Random.ColorHSV();
                temp.GetComponent<MeshRenderer>().material.color = randomColor;
                temp.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", randomColor);
                cubes.Add(temp);
                Debug.Log("Space key was pressed.");
                Debug.Log(cubes.Count);
            }
        }
    }
}
