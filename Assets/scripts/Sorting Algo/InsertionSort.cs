using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertionSort : MonoBehaviour
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
            InsertionSorting();
        }
    }

    void InsertionSorting(){
        StartCoroutine(Insertion());
    }

    IEnumerator Insertion()
    {
        int i, j;

        for (i = 1; i < num; i++) 
        { 
            //key=ObjectArr[i];
            j = i - 1;
            ObjectArr[j].GetComponent<Renderer>().material.color=Color.yellow;
    
            while (j >= 0 && ObjectArr[j].transform.localScale.y > ObjectArr[j+1].transform.localScale.y) 
            {
                ObjectArr[j+1].GetComponent<Renderer>().material.color=Color.green;

                Vector3 tmp=ObjectArr[j].transform.localPosition;
                ObjectArr[j].transform.localPosition = ObjectArr[j+1].transform.localPosition;

                Vector3 newfirst=ObjectArr[j].transform.position;
                newfirst.y=(ObjectArr[j].transform.localScale.y)/2;
                ObjectArr[j].transform.position=newfirst;

                ObjectArr[j+1].transform.localPosition = tmp;

                Vector3 newsecond=ObjectArr[j+1].transform.position;
                newsecond.y=(ObjectArr[j+1].transform.localScale.y)/2;
                ObjectArr[j+1].transform.position=newsecond;
                
                GameObject temp = ObjectArr[j];
                ObjectArr[j] = ObjectArr[j+1];
                ObjectArr[j+1] = temp;

                j=j-1;
                yield return new WaitForSeconds(0.5f);

                ObjectArr[j+1].GetComponent<Renderer>().material.color=Color.yellow;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
