using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MatriceaDeTrecere : MonoBehaviour
{
    [System.Serializable]
    public class Baza
    {
        public string nume = "Baza noua";

        public Vector2 u1 = new Vector2(1, 0);
        public Vector2 u2 = new Vector2(0, 1);
        [Space]
        public float v1 = 5;
        public float v2 = 5;
        [Space]
        public Vector2 rezultanta;
        public float lungimeRezultanta;

        [HideInInspector]
        public LineRenderer v1Line;
        [HideInInspector]
        public LineRenderer v2Line;
        [HideInInspector]
        public LineRenderer rezultantaLine;
        [HideInInspector]
        public LineRenderer u1SpaceLine;
        [HideInInspector]
        public LineRenderer u2SpaceLine;

        [HideInInspector]
        public Color pointsColour;

        [HideInInspector]
        public GameObject v1Pos;
        [HideInInspector]
        public GameObject v2Pos;

        public Baza()
        {
            GameObject v1Object = new GameObject("u1");
            GameObject v2Object = new GameObject("u2");
            GameObject rezultantaObject = new GameObject("rezultanta");
            GameObject u1SpaceObject = new GameObject("u1Space");
            GameObject u2SpaceObject = new GameObject("u2Space");

            v1Object.AddComponent<LineRenderer>();
            v2Object.AddComponent<LineRenderer>();
            rezultantaObject.AddComponent<LineRenderer>();
            u1SpaceObject.AddComponent<LineRenderer>();
            u2SpaceObject.AddComponent<LineRenderer>();

            v1Line = v1Object.GetComponent<LineRenderer>();
            v2Line = v2Object.GetComponent<LineRenderer>();
            rezultantaLine = rezultantaObject.GetComponent<LineRenderer>();
            u1SpaceLine = u1SpaceObject.GetComponent<LineRenderer>();
            u2SpaceLine = u2SpaceObject.GetComponent<LineRenderer>();

            v1Line.startWidth = .3f;
            v2Line.startWidth = .3f;
            rezultantaLine.startWidth = .5f;
            u1SpaceLine.startWidth = .4f;
            u2SpaceLine.startWidth = .4f;

            v1Pos = GameObject.CreatePrimitive(PrimitiveType.Cube);
            v2Pos = GameObject.CreatePrimitive(PrimitiveType.Cube);

            pointsColour = Random.ColorHSV();
        }


        

        public void CalcRezultanta()
        {
            rezultanta = v1 * u1 + v2 * u2;

            lungimeRezultanta = Mathf.Sqrt(Mathf.Pow(rezultanta.x, 2) + Mathf.Pow(rezultanta.y, 2));
        }

        public void Die()
        {
            DestroyImmediate(v1Line.gameObject);
            DestroyImmediate(v2Line.gameObject);
            DestroyImmediate(rezultantaLine.gameObject);
            DestroyImmediate(u1SpaceLine.gameObject);
            DestroyImmediate(u2SpaceLine.gameObject);
            DestroyImmediate(v1Pos.gameObject);
            DestroyImmediate(v2Pos.gameObject);
        }
    }

    public Material u1;
    public Material u2;
    public Material rezultanta;
    public Material v1;
    public Material v2;

    public List<Baza> baze;

    [Range(0.1f, 10f)]
    public float pointsFrequency = 2;
    [Range(0.05f, 2f)]
    public float pointsRadius = 1;

    //[ExecuteAlways]
    public void Update()
    {
        foreach(Baza baza in baze) {
            baza.v1Line.SetPosition(1, baza.v1 * baza.u1);
            baza.v2Line.SetPosition(1, baza.v2 * baza.u2);

            baza.u1SpaceLine.SetPosition(1, baza.u1 * 10000);
            baza.u2SpaceLine.SetPosition(1, baza.u2 * 10000);

            baza.v1Pos.transform.position = baza.v1 * baza.u1;
            baza.v2Pos.transform.position = baza.v2 * baza.u2;

            baza.CalcRezultanta();

            baza.rezultantaLine.SetPosition(1, baza.rezultanta);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 1);
        for (int i = 0; i < baze.Count; i++)
        {
            Gizmos.color = baze[i].pointsColour;

            for(int x = 0; x < 10; x += (int)pointsFrequency)
            {
                for (int y = 0; y < 10; y += (int)pointsFrequency)
                {
                    float xPos = x * baze[i].u1.x + x * baze[i].u2.x;
                    float yPos = y * baze[i].u1.y + y * baze[i].u2.y;

                    Vector3 pos = new Vector3(xPos, yPos, 0);
                    Gizmos.DrawSphere(pos, pointsRadius);
                }
            }

        }
    }

    public void AddNewBase()
    {
        if (baze == null)
            baze = new List<Baza>();

        Baza bazaNoua = new Baza();

        baze.Add(bazaNoua);

        bazaNoua.v1Line.material = v1;
        bazaNoua.v2Line.material = v2;
        bazaNoua.u1SpaceLine.material = u1;
        bazaNoua.u2SpaceLine.material = u2;
        bazaNoua.rezultantaLine.material = rezultanta;

        bazaNoua.v1Line.transform.SetParent(this.transform);
        bazaNoua.v2Line.transform.SetParent(this.transform);
        bazaNoua.u1SpaceLine.transform.SetParent(this.transform);
        bazaNoua.u2SpaceLine.transform.SetParent(this.transform);
        bazaNoua.rezultantaLine.transform.SetParent(this.transform);
        bazaNoua.v1Pos.transform.SetParent(this.transform);
        bazaNoua.v2Pos.transform.SetParent(this.transform);

        Update();
    }

    public void DeleteBase(int whichOne)
    {
        baze[whichOne].Die();
        baze.RemoveAt(whichOne);
    }
}
