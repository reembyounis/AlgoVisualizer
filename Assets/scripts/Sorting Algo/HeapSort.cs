using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapSort : MonoBehaviour
{
    public GameObject[] ObjectArr;
    public GameObject cube;
    public int num;

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

        Update();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HeapSorting();
        }
    }

    void HeapSorting()
    {
        StartCoroutine(Heap());
    }

    IEnumerator Heap()
    {
        for (int i = num / 2 - 1; i >= 0; i--)
        {
            StartCoroutine(heapify(ObjectArr, num, i));
            yield return new WaitForSeconds(4f);
        }

        for (int i = num - 1; i > 0; i--)
        {

            Vector3 tmp = ObjectArr[0].transform.localPosition;
            ObjectArr[0].transform.localPosition = ObjectArr[i].transform.localPosition;

            Vector3 newfirst = ObjectArr[0].transform.position;
            newfirst.y = (ObjectArr[0].transform.localScale.y) / 2;
            ObjectArr[0].transform.position = newfirst;

            ObjectArr[i].transform.localPosition = tmp;

            Vector3 newsecond = ObjectArr[i].transform.position;
            newsecond.y = (ObjectArr[i].transform.localScale.y) / 2;
            ObjectArr[i].transform.position = newsecond;

            GameObject temp = ObjectArr[0];
            ObjectArr[0] = ObjectArr[i];
            ObjectArr[i] = temp;


            StartCoroutine(heapify(ObjectArr, i, 0));
        }
        yield return new WaitForSeconds(2f);
    }

    IEnumerator heapify(GameObject[] ObjectArr, int n, int i)
    {
        ObjectArr[i].GetComponent<Renderer>().material.color=Color.green;
        
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;

        yield return new WaitForSeconds(2f);

        if(left<n){
            ObjectArr[left].GetComponent<Renderer>().material.color=Color.yellow;
        }

        if(right<n){
            ObjectArr[right].GetComponent<Renderer>().material.color=Color.yellow;
        }
        
        yield return new WaitForSeconds(2f);

        if (left < n && ObjectArr[left].transform.localPosition.y > ObjectArr[largest].transform.localPosition.y)
        {
            largest = left;
        }

        if (right < n && ObjectArr[right].transform.localPosition.y > ObjectArr[largest].transform.localPosition.y)
        {
            largest = right;
        }

        ObjectArr[largest].GetComponent<Renderer>().material.color=Color.red;
        yield return new WaitForSeconds(2f);

        if (largest != i)
        {
            Vector3 tmp = ObjectArr[i].transform.localPosition;
            ObjectArr[i].transform.localPosition = ObjectArr[largest].transform.localPosition;

            Vector3 newfirst = ObjectArr[i].transform.position;
            newfirst.y = (ObjectArr[i].transform.localScale.y) / 2;
            ObjectArr[i].transform.position = newfirst;

            ObjectArr[largest].transform.localPosition = tmp;

            Vector3 newsecond = ObjectArr[largest].transform.position;
            newsecond.y = (ObjectArr[largest].transform.localScale.y) / 2;
            ObjectArr[largest].transform.position = newsecond;

            GameObject temp = ObjectArr[i];
            ObjectArr[i] = ObjectArr[largest];
            ObjectArr[largest] = temp;

            StartCoroutine(heapify(ObjectArr, n, largest));
        }

        ObjectArr[largest].GetComponent<Renderer>().material.color=Color.white;
        yield return new WaitForSeconds(2f);

    }
}
