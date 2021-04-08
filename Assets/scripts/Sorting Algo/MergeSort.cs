using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeSort : MonoBehaviour
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

        for(int i=0;i<num;i++){
            Debug.Log(ObjectArr[i].transform.position.y);
            Debug.Log(",");
        }
        Debug.Log("*");

        Update();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MergeSorting(ObjectArr,0, num - 1);
        }
    }

    void MergeSorting(GameObject[] ObjectArr, int start, int end)
    {
        if(start<end){

            int mid = start + (end-start) / 2;

            MergeSorting(ObjectArr, start, mid);
            MergeSorting(ObjectArr, mid + 1, end);

            StartCoroutine(merge(ObjectArr, start, mid, end));      
        }
    }

    
    IEnumerator merge(GameObject[] ObjectArr, int p, int q, int r) {

        int i, j, k;
        int n1 = q - p + 1;
        int n2 = r - q;
        GameObject[] L = new GameObject[n1];
        GameObject[] R = new GameObject[n2];

        for (i = 0; i < n1; i++) {
            L[i] = ObjectArr[p + i];
        }
        
        for (j = 0; j < n2; j++) {
            R[j] = ObjectArr[q + 1 + j];
        }

        i = 0;
        j = 0;
        k = 0;

        // +p

        GameObject[] temp = new GameObject[r-p+1];

        while (i < n1 && j < n2) {
            if (L[i].transform.localPosition.y <= R[j].transform.localPosition.y) {
               temp[k] = L[i];
               i++;
            } else {
               temp[k] = R[j];
               j++;
            }
            k++;
        }
        
        while (i < n1) {
            temp[k] = L[i];
            i++;
            k++;
        }

        while (j < n2) {
            temp[k] = R[j];
            j++;
            k++;
        }

        if(p==0 && r==num-1){
            for(i=0;i<num;i++){
                Debug.Log(temp[i].transform.position.y);
                Debug.Log(",");
            }
        }

        /*GameObject[] temp = new GameObject[end-start+1];
        int[] index = new int[end-start+1];

        int i = start, j = mid+1, k = 0;

        while(i <= mid && j <= end) {
            if(ObjectArr[i].transform.localScale.y < ObjectArr[j].transform.localScale.y) {
    
                temp[k] = ObjectArr[i];
                index[k]=i;
                k += 1; i += 1;
            }
            else {
                temp[k] = ObjectArr[j];
                index[k]=j;
                k += 1; j += 1;
            }
        }

        while(i <= mid) {
            temp[k] = ObjectArr[i];
            index[k]=i;
            k += 1; i += 1;
        }

        while(j <= end) {
            temp[k] = ObjectArr[j];
            index[k]=j;
            k += 1; j += 1;
        }

        if(start==0 && end==num-1){
            for(i=0;i<num;i++){
                Debug.Log(temp[i].transform.position.y);
                Debug.Log(",");
            }*/


        /*for(i=start;i<k;i++){
            //ObjectArr[i].GetComponent<Renderer>().material.color=Color.yellow;

            //yield return new WaitForSeconds(2f);

            ObjectArr[start+i].transform.localPosition=temp[i-start].transform.localPosition;

            Vector3 newfirst=ObjectArr[start+i].transform.position;
            newfirst.y=(ObjectArr[start+i].transform.localScale.y)/2;
            ObjectArr[start+i].transform.position=newfirst;

            ObjectArr[start+i]=temp[i-start];

            
            
            Vector3 tmp=ObjectArr[i].transform.position;
            ObjectArr[i].transform.localPosition=temp[i-start].transform.localPosition;

            Vector3 newfirst=ObjectArr[i].transform.position;
            newfirst.y=(ObjectArr[i].transform.localScale.y)/2;
            newfirst.z=ObjectArr[i].transform.localScale.z-10;
            ObjectArr[i].transform.position=newfirst;

            ObjectArr[index[i-start]].transform.localPosition = tmp;

            Vector3 newsecond=ObjectArr[index[i-start]].transform.position;
            newsecond.y=(ObjectArr[index[i-start]].transform.localScale.y)/2;
            ObjectArr[index[i-start]].transform.position=newsecond;

            GameObject something = ObjectArr[i];
            ObjectArr[i] = ObjectArr[index[i-start]];
            ObjectArr[index[i-start]] = something;

        }*/

        yield return new WaitForSeconds(0.1f);

    }
}
