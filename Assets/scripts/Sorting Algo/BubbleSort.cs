using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSort : MonoBehaviour
{
    public GameObject[] ObjectArr; // array for saving initiated gameobjects
    public GameObject cube; // for initiating game object

    public bool condition = false; // indicates that we chose one of the dropdown options

    public bool visualize = false; // button for starting to visualize
    BubbleInput cubesNum; // number of cubes to initiate from class InsertInput
    int num = 0; // saving number of cubes
    public float updateSpeed; // speed of visualizing
    public bool newArray = false; // indicator for generating new array
    void Update()
    {
        if (condition || (!condition && newArray))
        {
            if (newArray) // generate new array
            {
                newArray = false;
            }

            cubesNum = this.gameObject.GetComponent<BubbleInput>(); // get cubesnum input from user

            if (num != 0)
            {
                for (int i = 0; i < num; i++) // reset number of cubes initiated
                {
                    Destroy(ObjectArr[i]);
                }
            }

            num = cubesNum.output; // getting cubes number input
            condition = false;

            int j = (20 - num) / 2;
            for (int i = 0; i < num; i++) // initiating the cubes
            {
                GameObject go = Instantiate(cube, new Vector3(j, 0, 1), Quaternion.identity) as GameObject;
                j += 2;
                go.transform.localScale = new Vector3(1, Random.Range(1, 10), 1);
                Vector3 tmp = go.transform.position;
                tmp.y = (go.transform.localScale.y) / 2;
                go.transform.position = tmp;
                ObjectArr[i] = go;
            }
        }

        if (visualize) // visualize = true, sorting the cubes
        {
            visualize = false;
            StartCoroutine(Bubble());

        }

    }
    IEnumerator Bubble() // Bubble Sort Algorithm
    {
        int i, j;
        for (i = 0; i < num; i++)
        {
            for (j = 0; j < num - i - 1; j++)
            {
                ObjectArr[j].GetComponent<Renderer>().material.color = Color.red;
                yield return new WaitForSeconds(updateSpeed);
                ObjectArr[j + 1].GetComponent<Renderer>().material.color = Color.yellow;
                yield return new WaitForSeconds(updateSpeed);

                if (ObjectArr[j].transform.localScale.y > ObjectArr[j + 1].transform.localScale.y)
                {
                    Vector3 tmp = ObjectArr[j].transform.localPosition;
                    ObjectArr[j].transform.localPosition = ObjectArr[j + 1].transform.localPosition;

                    Vector3 newfirst = ObjectArr[j].transform.position;
                    newfirst.y = (ObjectArr[j].transform.localScale.y) / 2;
                    ObjectArr[j].transform.position = newfirst;

                    ObjectArr[j + 1].transform.localPosition = tmp;

                    Vector3 newsecond = ObjectArr[j + 1].transform.position;
                    newsecond.y = (ObjectArr[j + 1].transform.localScale.y) / 2;
                    ObjectArr[j + 1].transform.position = newsecond;

                    GameObject temp = ObjectArr[j];
                    ObjectArr[j] = ObjectArr[j + 1];
                    ObjectArr[j + 1] = temp;
                    //ObjectArr[j + 1].GetComponent<Renderer>().material.color = Color.yellow;
                    yield return new WaitForSeconds(updateSpeed);
                }
                else
                {
                    ObjectArr[j].GetComponent<Renderer>().material.color = Color.yellow;
                }

                yield return new WaitForSeconds(updateSpeed);
            }

            ObjectArr[j].GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(updateSpeed);
        }
    }
}
