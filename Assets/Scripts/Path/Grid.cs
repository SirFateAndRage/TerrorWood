using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform StartPosition;
    public LayerMask DecoMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float Distance;

    Node[,] NodeArray;
    public List<Node> FinalPath;

    float nodeDiamaeter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiamaeter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiamaeter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiamaeter);
        CreateGrid();
    }

    //Creo Grilla
    void CreateGrid()
    {
        NodeArray = new Node[gridSizeX, gridSizeY];

        //Rectangulo busco simplemente su punto abajo a la izquierda para poder crear los nodos.
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        //empezando desde abajo la izquierda voy creando mis nodos y diciendole cuales van a ser recorrible para un futuro y cuales no
        for (int x   = 0; x < gridSizeX; x++)
        {
            for (int z  = 0; z < gridSizeY; z++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiamaeter + nodeRadius) + Vector3.forward * (z * nodeDiamaeter + nodeRadius);
                bool Wall = true;

                if (Physics.CheckSphere(worldPoint, nodeRadius, DecoMask))
                {
                    Wall = false;
                }
                NodeArray [x,z] = new Node(Wall, worldPoint, x, z);


            }
        }
    }

    // Buscador de nodos cercanos
    public List<Node> GetNeighboringNodes(Node a_Node)
    {
        List<Node> NeighboringNodes = new List<Node>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                //si es un nodo que ya pasamo lo skipeamos
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = a_Node.gridX + x;
                int checkY = a_Node.gridY + y;

                // estar seguro que exista en la grilla
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    NeighboringNodes.Add(NodeArray[checkX, checkY]); //Adds to the neighbours list.
                }

            }
        }
        return NeighboringNodes;
    }
    public Node  NodeFromWorldPosition(Vector3 a_WorldPosition)
    {
        float xpoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / (gridWorldSize.x));
        float ypoint = ((a_WorldPosition.z + gridWorldSize.y / 2) / (gridWorldSize.y));

        xpoint = Mathf.Clamp01(xpoint);
        ypoint = Mathf.Clamp01(ypoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xpoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * ypoint);

        return NodeArray[x, y];
    }
   
    // para tener yo una representacion visual de la grilla que estoy creando
    
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if(NodeArray != null)
        {
            foreach(Node node in NodeArray)
            {
                if (node.IsWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if(FinalPath != null)
                {
                    if (FinalPath.Contains(node))
                        Gizmos.color = Color.red;
                }

                Gizmos.DrawCube(node.Position, Vector3.one * (nodeDiamaeter - Distance));
            }
        }
   
    }
    */
    
   
    
}
