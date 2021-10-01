using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding 
{
    [SerializeField]
    Grid _gridReference;
    public Transform StartPosition;
    public Transform TargetPosition;
    public List<Node> Path = new List<Node>();
    

    public PathFinding(Transform start,Transform target,Grid grid)
    {
        _gridReference = grid;
        StartPosition = start;
        TargetPosition = target;
        
    }


    public void OnUpdate()
    {
        FindPath(StartPosition.position, TargetPosition.position);
    }


     public void FindPath(Vector3 a_StartPos,Vector3 a_TargetPos)
     {
        Node StartNode = _gridReference.NodeFromWorldPosition(a_StartPos);
        Node TargetNode = _gridReference.NodeFromWorldPosition(a_TargetPos);


        List<Node> OpenList = new List<Node>();
        HashSet<Node> CloseList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while(OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for (int i = 0; i < OpenList.Count; i++)
            {
                if(OpenList[i].FCost < CurrentNode.FCost || OpenList[i].FCost == CurrentNode.FCost && OpenList[i].hCost< CurrentNode.hCost)
                {
                    CurrentNode = OpenList[i];
                }
            }

            OpenList.Remove(CurrentNode);
            CloseList.Add(CurrentNode);

            if(CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach (Node NeighbordNode in _gridReference.GetNeighboringNodes(CurrentNode))
            {
                if (!NeighbordNode.IsWall || CloseList.Contains(NeighbordNode))
                {
                    continue;
                }

                int MoveCost = CurrentNode.gCost + GetManhattenDistance(CurrentNode, NeighbordNode);

                if (MoveCost < NeighbordNode.gCost || !OpenList.Contains(NeighbordNode))
                {
                    NeighbordNode.gCost = MoveCost;
                    NeighbordNode.hCost = GetManhattenDistance(NeighbordNode, TargetNode);
                    NeighbordNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighbordNode))
                    {
                        OpenList.Add(NeighbordNode);
                    }

                }
            }

        }

    }

    void GetFinalPath(Node a_StartingNode,Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_EndNode;

        while(CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }
        FinalPath.Reverse();
         Path = FinalPath;
        _gridReference.FinalPath = FinalPath;
        
    }

  

    int GetManhattenDistance(Node a_nodeA,Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;
    }
}
