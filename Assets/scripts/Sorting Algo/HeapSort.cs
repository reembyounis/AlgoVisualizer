using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSort : MonoBehaviour
{
    public GameObject[] ObjectArr; // array for saving initiated gameobjects
    public GameObject cube; // for initiating game object

    public bool condition = false; // indicates that we chose one of the dropdown options

    public bool visualize = false; // button for starting to visualize
    HeapInput cubesNum; // number of cubes to initiate from class HeapInput
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

            cubesNum = this.gameObject.GetComponent<HeapInput>(); // get cubesnum input from user

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
            StartCoroutine(Heap());
            //Heap();

        }
    }
    IEnumerator Heap()
    {
        for (int i = num / 2 - 1; i >= 0; i--)
        {
            StartCoroutine(heapify(num, i));
        }

        for (int i = num - 1; i > 0; i--)
        {
            yield return new WaitForSeconds(updateSpeed);

            Vector3 tmp = ObjectArr[0].transform.localPosition;

            ObjectArr[0].transform.localPosition = ObjectArr[i].transform.localPosition;
            ObjectArr[0].GetComponent<Renderer>().material.color = Color.cyan;

            Vector3 newfirst = ObjectArr[0].transform.position;
            newfirst.y = (ObjectArr[0].transform.localScale.y) / 2;
            ObjectArr[0].transform.position = newfirst;

            ObjectArr[i].transform.localPosition = tmp;

            Vector3 newsecond = ObjectArr[i].transform.position;
            newsecond.y = (ObjectArr[i].transform.localScale.y) / 2;
            ObjectArr[i].transform.position = newsecond;

            //ObjectArr[i].GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(updateSpeed);
            GameObject temp = ObjectArr[0];
            ObjectArr[0] = ObjectArr[i];
            ObjectArr[i] = temp;


            yield return StartCoroutine(heapify(i, 0));
        }
        yield return new WaitForSeconds(updateSpeed);
    }

    IEnumerator heapify(int n, int i)
    {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;


        ObjectArr[i].GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(updateSpeed);


        if (left < n && ObjectArr[left].transform.localPosition.y > ObjectArr[largest].transform.localPosition.y)
        {
            largest = left;
            ObjectArr[left].GetComponent<Renderer>().material.color = Color.yellow;
        }

        if (right < n && ObjectArr[right].transform.localPosition.y > ObjectArr[largest].transform.localPosition.y)
        {
            largest = right;
            ObjectArr[right].GetComponent<Renderer>().material.color = Color.yellow;
        }
        if (largest != i)
        {
            yield return new WaitForSeconds(updateSpeed);
            Vector3 tmp = ObjectArr[i].transform.localPosition;
            ObjectArr[i].transform.localPosition = ObjectArr[largest].transform.localPosition;

            Vector3 newfirst = ObjectArr[i].transform.localPosition;
            newfirst.y = (ObjectArr[i].transform.localScale.y) / 2;
            ObjectArr[i].transform.position = newfirst;

            ObjectArr[largest].transform.localPosition = tmp;

            Vector3 newsecond = ObjectArr[largest].transform.position;
            newsecond.y = (ObjectArr[largest].transform.localScale.y) / 2;
            ObjectArr[largest].transform.position = newsecond;

            GameObject temp = ObjectArr[i];
            ObjectArr[i] = ObjectArr[largest];
            ObjectArr[largest] = temp;


            ObjectArr[i].GetComponent<Renderer>().material.color = Color.white;
            ObjectArr[largest].GetComponent<Renderer>().material.color = Color.white;
            yield return new WaitForSeconds(updateSpeed);

            yield return StartCoroutine(heapify(n, largest));

        }

        ObjectArr[largest].GetComponent<Renderer>().material.color = Color.white;
        yield return new WaitForSeconds(updateSpeed);

    }
}
