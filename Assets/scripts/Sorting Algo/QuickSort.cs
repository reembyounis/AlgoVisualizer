using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : MonoBehaviour
{
    public GameObject[] ObjectArr; // array for saving initiated gameobjects
    public GameObject cube; // for initiating game object

    public bool condition = false; // indicates that we chose one of the dropdown options

    public bool visualize = false; // button for starting to visualize
    QuickInput cubesNum; // number of cubes to initiate from class HeapInput
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

            cubesNum = this.gameObject.GetComponent<QuickInput>(); // get cubesnum input from user

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
            QuickSorting();

        }
    }

    void QuickSorting()
    {
        StartCoroutine(Quick(0, num - 1));
    }

    IEnumerator Quick(int left, int right)
    {
        Vector3 tmp;
        Vector3 newfirst;
        Vector3 newsecond;
        GameObject temp;

        if (left < right)
        {
            float pivot = ObjectArr[right].transform.localPosition.y;
            ObjectArr[right].GetComponent<Renderer>().material.color = Color.red;
            int i = left - 1;
            //int max = ObjectArr[left].transform.localPosition.y;

            for (int j = left; j < right; j++)
            {
                yield return new WaitForSeconds(updateSpeed);
                ObjectArr[j].GetComponent<Renderer>().material.color = Color.yellow;
                if (ObjectArr[j].transform.localPosition.y < pivot)
                {
                    i++;
                    ObjectArr[i].GetComponent<Renderer>().material.color = Color.yellow;
                    yield return new WaitForSeconds(updateSpeed);

                    tmp = ObjectArr[i].transform.localPosition;
                    ObjectArr[i].transform.localPosition = ObjectArr[j].transform.localPosition;

                    newfirst = ObjectArr[i].transform.position;
                    newfirst.y = (ObjectArr[i].transform.localScale.y) / 2;
                    ObjectArr[i].transform.position = newfirst;

                    ObjectArr[j].transform.localPosition = tmp;

                    newsecond = ObjectArr[j].transform.position;
                    newsecond.y = (ObjectArr[j].transform.localScale.y) / 2;
                    ObjectArr[j].transform.position = newsecond;

                    temp = ObjectArr[i];
                    ObjectArr[i] = ObjectArr[j];
                    ObjectArr[j] = temp;

                    yield return new WaitForSeconds(updateSpeed);
                    ObjectArr[i].GetComponent<Renderer>().material.color = Color.cyan;

                }
                yield return new WaitForSeconds(updateSpeed);
                if (ObjectArr[j].transform.localPosition.y > pivot)
                {
                    ObjectArr[j].GetComponent<Renderer>().material.color = Color.white;
                }

            }

            yield return new WaitForSeconds(updateSpeed);
            tmp = ObjectArr[i + 1].transform.localPosition;
            ObjectArr[i + 1].transform.localPosition = ObjectArr[right].transform.localPosition;

            newfirst = ObjectArr[i + 1].transform.position;
            newfirst.y = (ObjectArr[i + 1].transform.localScale.y) / 2;
            ObjectArr[i + 1].transform.position = newfirst;

            ObjectArr[right].transform.localPosition = tmp;

            newsecond = ObjectArr[right].transform.position;
            newsecond.y = (ObjectArr[right].transform.localScale.y) / 2;
            ObjectArr[right].transform.position = newsecond;

            temp = ObjectArr[i + 1];
            ObjectArr[i + 1] = ObjectArr[right];
            ObjectArr[right] = temp;

            ObjectArr[i + 1].GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(updateSpeed);

            int p = i + 1;
            yield return new WaitForSeconds(updateSpeed);
            yield return StartCoroutine(Quick(p + 1, right));
            ObjectArr[right].GetComponent<Renderer>().material.color = Color.green;
            yield return new WaitForSeconds(updateSpeed);
            yield return StartCoroutine(Quick(left, p - 1));
            ObjectArr[p - 1].GetComponent<Renderer>().material.color = Color.green;
        }
    }
}