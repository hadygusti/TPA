using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{

    [SerializeField] GameObject material;
    [SerializeField] GameObject TPHere;
    bool[,] buildable;

    //public 

    public class Node
    {
        public int x1, x2, z1, z2, width, length;
        public Node left, right;

        public Node(int x1, int x2, int z1, int z2)
        {
            this.x1 = x1;
            this.x2 = x2;
            this.z1 = z1;
            this.z2 = z2;
            this.width = this.x2 - this.x1;
            this.length = this.z2 - this.z1;
            this.left = null;
            this.right = null;
        }

        public Node()
        {
            this.left = null;
            this.right = null;
        }
    }

    public void GenerateSize(Node parent, int t)
    {
        if (t <= 0) return;

        Node left = new Node();
        Node right = new Node();

        float ratio = Random.Range(20, 80)/100f;
        //Debug.Log("Ratio: " + ratio);

        if (t % 2 == 0)
        {
            //left.x2 = Random.Range(parent.x1, parent.x2);
            left.x2 = parent.x1 + Mathf.RoundToInt((parent.x2 - parent.x1) * ratio);
            left.x1 = parent.x1;
            left.z1 = parent.z1;
            left.z2 = parent.z2;

            right.x1 = left.x2 + 1;
            right.x2 = parent.x2;
            right.z1 = parent.z1;
            right.z2 = parent.z2;
            //Debug.Log("Parent x1: " + parent.x1 + " Parent x2: " + parent.x2 + " new Between: " + left.x2);
        }
        else if (t % 2 == 1)
        {
            //left.z2 = Random.Range(parent.z1, parent.z2);
            left.z2 = parent.z1 + Mathf.RoundToInt((parent.z2 - parent.z1) * ratio);
            left.z1 = parent.z1;
            left.x1 = parent.x1;
            left.x2 = parent.x2;


            right.z1 = left.z2 + 1;
            right.z2 = parent.z2;
            right.x1 = parent.x1;
            right.x2 = parent.x2;
            //Debug.Log("Parent z1: " + parent.z1 + " Parent z2: " + parent.z2 + " new Between: " + left.z2);
        }

        parent.left = left;
        parent.right = right;

        GenerateSize(parent.right, t - 1);
        GenerateSize(parent.left, t - 1);
    }

    public void path(Node node)
    {
        if (node.left == null || node.right == null)
        {
            //Debug.Log("Anak Abis");
            return;
        }

        Vector3 leftMid = getMid(node.left);
        Vector3 rightMid = getMid(node.right);

        for (int x = Mathf.RoundToInt(leftMid.x); x <= rightMid.x; x++)
        {
            for (int z = Mathf.RoundToInt(leftMid.z); z <= rightMid.z; z++)
            {
                //Debug.Log("x = " + x + " z = " + z);
                buildable[x, z] = false;
            }
        }

        path(node.right);
        path(node.left);
    }

    public void Build()
    {
        for (int x = 0; x < 50; x++)
        {
            for (int z = 0; z < 50; z++)
            {
                if (buildable[x, z])
                {
                    Vector3 coordinate = new Vector3((x*3)+200, 0, (z*3)+200);
                    Instantiate(material, coordinate, Quaternion.identity);
                    //material.transform.parent = this.gameObject.transform;
                }
            }
        }
    }

    public Vector3 getMid(Node node)
    {
        Vector3 mid = new Vector3(node.x1 + (node.x2 - node.x1) / 2, 0, node.z1 + (node.z2 - node.z1) / 2);
        return mid;
    }

    //public bool checkRatio(int width, int length, int newWidth, int newLength)
    //{
    //    if (width == newWidth)
    //    {
    //        if (newWidth)
    //    }
    //}

    // Start is called before the first frame update
    void Start()
    {
        buildable = new bool[50, 50];
        for (int i = 0; i < 50; i++)
        {
            for (int j = 0; j < 50; j++)
            {
                buildable[i, j] = true;
            }
        }

        Node root = new Node(1, 49, 1, 49);
        GenerateSize(root, 5);
        path(root);
        MakeGetOut(root);
        Build();

        TPHere.transform.position = GetMostLeftChild(root);
        print(TPHere.transform.position);
        print(GetMostLeftChild(root));
    }

    public Vector3 GetMostLeftChild(Node node)
    {
        if (node.left == null)
        {
            Vector3 mid = getMid(node);
            mid.x = mid.x*3 + 200;
            mid.z = mid.z*3 + 200;
            return mid;
        }
        return GetMostLeftChild(node.left);
    }

    public Vector3 GetMostRightChildPos(Node node)
    {
        if (node.right == null) return getMid(node);
        return GetMostRightChildPos(node.right);
    }

    public Node GetMostRightChild(Node node)
    {
        if (node.right == null) return node;
        return GetMostRightChild(node.right);
    }

    public void MakeGetOut(Node root)
    {
        Node mostRightChild = GetMostRightChild(root);
        //buildable[45, 49] = false;

        int i = 49;

        while(buildable[30, i] == true && i > 0)
        {
            buildable[30, i] = false;
            i--;
        }

        //for (int i = (int)mostRightChild.z1; i < 50; i++)
        //{
        //    buildable[(int)mostRightChild.x1, i] = false;
        //    return;
        //    //for(int j = (int)mostRightChild.x1; j < 50; j++)
        //    //{
        //    //    if(buildable[j, i] == false)
        //    //    {
        //    //        return;
        //    //    }else
        //    //    {
        //    //        buildable[j, i] = false;

        //    //    }
        //    //}
        //}
        ////for (int i = (int)mostRightChild.x1; i < 50; i++)
        ////{
        ////    buildable[(int)mostRightChild.z1, i] = false;
        ////}
    }
}
