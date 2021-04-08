using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSort : MonoBehaviour
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
            QuickSorting();
        }
    }

    void QuickSorting()
    {
        StartCoroutine(Quick(0,num-1));
    }

    IEnumerator Quick(int start,int end)
    {
        int i;
        if(start<end){
            i=Partition(start,end);

            Quick(start,i-1);
            Quick(i+1,end);
        }
        yield return new WaitForSeconds(0.5f);
    }

    int Partition(int start, int end)
    {
        GameObject p = ObjectArr[end];
        int i=start-1;
        for(int j=start;j<=end-1;j++){
            if(ObjectArr[j].transform.localPosition.y <= p.transform.localPosition.y){
                i++;
                Vector3 tmp = ObjectArr[i].transform.localPosition;
                ObjectArr[i].transform.localPosition = ObjectArr[j].transform.localPosition;

                Vector3 newfirst = ObjectArr[i].transform.position;
                newfirst.y = (ObjectArr[i].transform.localScale.y) / 2;
                ObjectArr[i].transform.position = newfirst;

                ObjectArr[j].transform.localPosition = tmp;

                Vector3 newsecond = ObjectArr[j].transform.position;
                newsecond.y = (ObjectArr[j].transform.localScale.y) / 2;
                ObjectArr[j].transform.position = newsecond;

                GameObject temp = ObjectArr[i];
                ObjectArr[i] = ObjectArr[j];
                ObjectArr[j] = temp;
            }
        }
        return i+1;

    }
}