using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[RequireComponent(typeof(MeshFilter))]
public class newMesh : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles;

    void Awake(){
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Start(){
        MakeMeshData();
        CreateMesh();

    }

    void MakeMeshData(){
        vertices = new Vector3[]{new Vector3(0,0,0),new Vector3(0,0,1),new Vector3(1,0,0)};
        triangles = new int[]{0,1,2};

    }

    void CreateMesh(){
        Color[] newColors=mesh.colors;
        mesh.vertices=vertices;
        mesh.triangles=triangles;

        newColors[0]=new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
    
 
        mesh.colors = newColors;
        //mesh.RecalculateNormals();
    }

}*/


public class newMesh : MonoBehaviour{

    public Material material = null;

    private Vector3[] vertices = new Vector3[6];
    private int[] triangles = new int[6];
    private Color[] colors=new Color[6];

    private void Start(){
        vertices[0]=new Vector3(0,0,0);
        vertices[1]=new Vector3(0.5f-.05f,0,1);
        vertices[2]=new Vector3(1-.05f,0,0);

        vertices[3]=new Vector3(0.5f,0,1);
        vertices[4]=new Vector3(1.5f,0,1);
        vertices[5]=new Vector3(1,0,0);
        

        triangles[0]=0;
        triangles[1]=1;
        triangles[2]=2;

        triangles[3]=3;
        triangles[4]=4;
        triangles[5]=5;

        colors[0]=Color.white;
        colors[1]= Color.white;
        colors[2]=Color.white;

        colors[3]=Color.white;
        colors[4]= Color.white;
        colors[5]=Color.white;

        Mesh m=getMesh();
        setMesh(ref m);
    }




    private Mesh getMesh(){
        Mesh m = null;

        if(Application.isEditor == true){
            MeshFilter mf=this.GetComponent<MeshFilter>();
            if(mf==null){
                mf=this.gameObject.AddComponent<MeshFilter>();
                mf.sharedMesh=new Mesh();

            }
            m=mf.sharedMesh;
        }

        else{
            MeshFilter mf=this.GetComponent<MeshFilter>();
            if(mf==null){
                mf=this.gameObject.AddComponent<MeshFilter>();
                mf.mesh=new Mesh();
            }
            m=mf.mesh;
        }

        MeshRenderer mr=this.GetComponent<MeshRenderer>();
        if(mr==null){
            mr=this.gameObject.AddComponent<MeshRenderer>();
        }
        mr.material=material;

        return m;
    }


    public void setMesh(ref Mesh m){
        m.Clear();

        m.vertices=vertices;
        m.triangles=triangles;
        m.colors=colors;
        
        m.RecalculateBounds();
        m.RecalculateNormals();
        //m.RecalculateTangents();

    }

}
