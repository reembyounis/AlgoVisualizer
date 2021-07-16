using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSort : MonoBehaviour
{
    public GameObject[] ObjectArr;
    public GameObject cube;
    public int num;

    Vector3 newVec = new Vector3();

    void Start()
    {
        int j = 0;
        for (int i = 0; i < num; i++)
        {
            GameObject go = Instantiate(cube, new Vector3(j, 0, 1), Quaternion.identity) as GameObject;
            j += 2;
            go.transform.localScale = new Vector3(1, Random.Range(1, 10), 1);
            Vector3 tmp = go.transform.position;
            tmp.y = (go.transform.localScale.y) / 2;
            go.transform.position = tmp;
            ObjectArr[i] = go;
        }

        for (int i = 0; i < num; i++)
        {
            Debug.Log(ObjectArr[i].transform.localPosition);
        }
        Debug.Log("*****");

        Update();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MergeSorting(0, num - 1);
        }
    }

    void MergeSorting(int start, int end)
    {
        if (start < end)
        {
            int mid = start + (end - start) / 2;

            MergeSorting(start, mid);
            MergeSorting(mid + 1, end);

            StartCoroutine(merge(start, mid, end));

            /*for (int i = 0; i < num; i++)
            {
                Debug.Log(ObjectArr[i].transform.localPosition);
            }
            Debug.Log("*****");*/
        }
    }


    IEnumerator merge(int low, int mid, int high)
    {
        Debug.Log("low " + low);
        Debug.Log("mid " + mid);
        Debug.Log("high " + high);
        Debug.Log("$$$$$$");
        int leftIndex = mid - low + 1;
        int rightIndex = high - mid;
        int mergeIndex = mid - low + 1;

        GameObject[] tempList = new GameObject[num];
        int[] templistIndex = new int[num];

        while (leftIndex < mid && rightIndex < high)
        {

            if (ObjectArr[leftIndex].transform.localPosition.y < ObjectArr[rightIndex].transform.localPosition.y)
            {
                tempList[mergeIndex] = ObjectArr[leftIndex];
                templistIndex[mergeIndex] = leftIndex;

                //tempList[mergeIndex].transform.localPosition = ObjectArr[leftIndex].transform.localPosition;

                leftIndex++;
            }
            else
            {
                tempList[mergeIndex] = ObjectArr[rightIndex];
                templistIndex[mergeIndex] = rightIndex;
                //tempList[mergeIndex].transform.localPosition = ObjectArr[rightIndex].transform.localPosition;


                rightIndex++;
            }
            mergeIndex++;
        }

        while (leftIndex < mid)
        {

            tempList[mergeIndex] = ObjectArr[leftIndex];
            templistIndex[mergeIndex] = leftIndex;
            //tempList[mergeIndex].transform.localPosition = ObjectArr[leftIndex].transform.localPosition;

            leftIndex++;
            mergeIndex++;
        }

        while (rightIndex < high)
        {

            tempList[mergeIndex] = ObjectArr[rightIndex];
            templistIndex[mergeIndex] = rightIndex;
            //tempList[mergeIndex].transform.localPosition = ObjectArr[rightIndex].transform.localPosition;


            rightIndex++;
            mergeIndex++;
        }

        /*for (int i = 0; i < mergeIndex; i++)
        {
            Debug.Log(tempList[i].transform.localPosition);
        }

        Debug.Log("templist *******");*/

        for (int i = mid - low + 1; i < mergeIndex; i++)
        {
            Debug.Log("templist" + tempList[i].transform.localPosition);
            Debug.Log("objectarr" + ObjectArr[i].transform.localPosition);
            Debug.Log("=======");


            /*ObjectArr[i].transform.localPosition = tempList[i].transform.localPosition;

            newVec = ObjectArr[i].transform.position;
            newVec.y = (ObjectArr[i].transform.localScale.y) / 2;

            ObjectArr[i].transform.position = newVec;

            ObjectArr[i] = tempList[i];*/


            /*Debug.Log("templist" + tempList[i].transform.localPosition);
            Debug.Log("objectarr" + ObjectArr[i].transform.localPosition);
            Debug.Log("^^^^^^^");*/

            /*Debug.Log("templist" + templistIndex[i]);
            
            Debug.Log("^^^^^^^");*/

            Vector3 tmp = ObjectArr[i].transform.localPosition;
            ObjectArr[i].transform.localPosition = ObjectArr[templistIndex[i]].transform.localPosition;

            Vector3 newfirst = ObjectArr[i].transform.position;
            newfirst.y = (ObjectArr[i].transform.localScale.y) / 2;

            ObjectArr[i].transform.position = newfirst;

            ObjectArr[templistIndex[i]].transform.localPosition = tmp;

            Vector3 newsecond = ObjectArr[templistIndex[i]].transform.position;
            newsecond.y = (ObjectArr[templistIndex[i]].transform.localScale.y) / 2;
            ObjectArr[templistIndex[i]].transform.position = newsecond;

            GameObject temp = ObjectArr[i];
            ObjectArr[i] = ObjectArr[templistIndex[i]];
            ObjectArr[templistIndex[i]] = temp;

            Debug.Log("objectarr" + ObjectArr[i].transform.localPosition);
            Debug.Log("objectarr" + ObjectArr[templistIndex[i]].transform.localPosition);
        }

        yield return new WaitForSeconds(0.1f);

    }
}
