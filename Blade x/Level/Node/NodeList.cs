﻿using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Swift_Blade.Level;
using System.Collections;
using UnityEngine;
using System.Text;
using System;

public enum NodeType
{
    //Stage
    Stage1,
    Stage2,
    Stage3,
    Stage4,
    
    //Special
    Point,
    Challenge,
    Store,
    Event,
        
    Boss1,
    Boss2,
    Boss3,
    Boss4,
    
    Rest,
       
    Trap,
    Ending,
    
    None,
}

[Serializable]
public class Node
{
    public NodeType nodeType;
    public string nodeName;
    private Door doorPrefab;
    
    public void SetPortalPrefab(Door prefab)
    {
        doorPrefab = prefab;
    }
    public Door GetPortalPrefab()
    {
        return doorPrefab;
    }
        
}

[Serializable]
public class NodeDictionary : IEnumerable<List<Node>>
{
    private Dictionary<NodeType, List<Node>> nodeList;
    private Dictionary<NodeType, List<Node>> originNodeList;

    private List<NodeType> specialNodeTypes;
            
    private bool canFirstAppearSpecialNode = true;
    private bool canSecondAppearSpecialNode = true;
    private bool canAppearRestRoom = false;
    
    private const byte APPEAR_SPECIAL_NODE_PERCENT = 25; //100 / 5 = 16.xxx
    
    private NodeType currentStage = NodeType.Stage4; 
    private NodeType currentBoss = NodeType.Boss4;
    
    public NodeDictionary(List<Node> nodes)
    {
        originNodeList = new Dictionary<NodeType, List<Node>>();
        specialNodeTypes = new List<NodeType>();
        Initialize();
        
        foreach (var item in nodes)
        {
            if (!originNodeList.ContainsKey(item.nodeType))
            {
                originNodeList[item.nodeType] = new List<Node>();
            }
            
            originNodeList[item.nodeType].Add(item);
        }

        nodeList = new Dictionary<NodeType, List<Node>>(originNodeList);
    }

    public string this[NodeType type] => GetRandomNode(type).nodeName;
    
    #region Priavte
    private bool CanAppearSpecialNode() => canSecondAppearSpecialNode || canFirstAppearSpecialNode;
    private bool IsValidScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
            return false;
        
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string scenePath = System.IO.Path.GetFileNameWithoutExtension(path);
                        
            if (scenePath == sceneName)
                return true;
        }
        Debug.LogError("Scene List에 Scene 없습니다.");
        return false;
    }
    private Node SelectRandomNode(List<Node> nodes)
    {
        //only one
        if (nodes.Count == 1)
        {
            return nodes[0];
        }
        
        Node newNode = nodes[Random.Range(0, nodes.Count)];
        
        return newNode;
    }
    private List<NodeType> GetNodeTypes(ref int currentNodeIndex)
    {
        List<NodeType> nodeTypes = new List<NodeType>();
        
        if (canAppearRestRoom)
        {
            InitSpecialNode();
            
            currentNodeIndex = 0;
            canAppearRestRoom = false;
            
            nodeTypes.Add(NodeType.Rest);
            
            if (currentStage == NodeType.Stage4) 
            {
                nodeTypes.Clear();
                nodeTypes.Add(NodeType.Ending);
            }
            else
            {
                currentStage = (NodeType)((int)currentStage + 1);
                currentBoss = (NodeType)((int)currentBoss + 1);
            }
            
        }
        else if (currentNodeIndex >= 4)
        {
            canAppearRestRoom = true;
            nodeTypes.Add(currentBoss);
        }
        else if (CanAppearSpecialNode() && Random.Range(0,100) < currentNodeIndex * APPEAR_SPECIAL_NODE_PERCENT)
        {
            if (canFirstAppearSpecialNode)
                canFirstAppearSpecialNode = false;
            else
                canSecondAppearSpecialNode = false;
            
            NodeType nodeType = specialNodeTypes[Random.Range(0, specialNodeTypes.Count)];
            specialNodeTypes.Remove(nodeType);
            
            nodeTypes.Add(nodeType);
            nodeTypes.Add(currentStage);         
        }
        else
        {
            nodeTypes.Add(currentStage);         
        }
        
        return nodeTypes;
    }
    private Node GetRandomNode(NodeType nodeType)
    {
        if (nodeList.ContainsKey(nodeType) && nodeList[nodeType].Count > 0)
        {
            List<Node> nodes = nodeList[nodeType];
            Node selectedNode = SelectRandomNode(nodes);
            
            if (IsValidScene(selectedNode.nodeName))
                return selectedNode;
            
            Debug.LogError($"{selectedNode.nodeName}은(는) sceneList에 존재하지 않습니다!");
            return null;
        }
        
        Debug.LogError($"{nodeType}은 유효하지 않은 NodeType이거나, {nodeList[nodeType].Count} 해당 타입의 Node가 존재하지 않습니다. ");
        
        return null;
    }

    private void InitSpecialNode()
    {
        canFirstAppearSpecialNode = true;
        canSecondAppearSpecialNode = true;
        
        specialNodeTypes.Clear();
        
        specialNodeTypes.Add(NodeType.Challenge);
        specialNodeTypes.Add(NodeType.Event);
        specialNodeTypes.Add(NodeType.Store);
        specialNodeTypes.Add(NodeType.Trap);
        
        //specialNodeTypes.Add(NodeType.Point);
    }
    
    #endregion

    #region Public
    public void Add(Node newNode)
    {
        nodeList[newNode.nodeType].Add(newNode);
    } 
    public void Initialize()
    {
        currentStage = NodeType.Stage1;     
        currentBoss = NodeType.Boss1;
        
        InitSpecialNode();
    }
    public Node[] GetRandomNodes(ref int currentNodeIndex)
    {
        List<NodeType> nodeTypes = GetNodeTypes(ref currentNodeIndex);
        Node[] nodes = new Node[nodeTypes.Count];
        
        for (int i = 0; i < nodeTypes.Count; i++)
        {
            nodes[i] = GetRandomNode(nodeTypes[i]);
        }
        
        return nodes;
    }
    public NodeType GetCurrentStageType() => currentStage;
    
    public void printNodes()
    {
        foreach (var item in nodeList)
        {
            Debug.Log($"{item.Key} : ");
            foreach (var item2 in item.Value)
            {
                Debug.Log($"{item2.nodeName}, ");
            }
        }
    }
    #endregion
    
    public IEnumerator<List<Node>> GetEnumerator()
    {
        return nodeList.Values.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
}

namespace Swift_Blade.Level
{
    [CreateAssetMenu(fileName = "NodeList", menuName = "SO/Scene/NodeList")]
    public class NodeList : ScriptableObject
    {
        [SerializeField] private List<Node> nodelist;
        private NodeDictionary nodeDictionary;
        
        [SerializeField] private Door expDoor;
        [SerializeField] private Door eventDoor;
        [SerializeField] private Door storeDoor;
        [SerializeField] private Door pointDoor;
        [SerializeField] private Door challengeDoor;
        [SerializeField] private Door bossDoor;
        [SerializeField] private Door restDoor;
        [SerializeField] private Door trapDoor;
        [SerializeField] private Door endingDoor;
        
        [Space]
        [SerializeField] private bool createStageNode;
        
        private readonly StringBuilder stageName = new StringBuilder();
        
        private int stageCount = 0;
        private int currentNodeIndex = 0;
        
        private const string STAGE_ = "Stage_";
        private const string STAGE = "Stage";
        
        public void Initialize()
        {
            if (createStageNode)
            {
                NodeType stageType = NodeType.Stage1;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        Node newNode = new Node
                        {
                            nodeType = stageType
                        };
                        nodelist.Add(newNode);
                    }
                    stageType = (NodeType)(int)stageType + 1;
                }
            }
            
            nodeDictionary = new NodeDictionary(nodelist);
            nodeDictionary.Initialize();
            
            currentNodeIndex = 0;
            stageCount = 0;
            
            stageName.Clear();
            stageName.Append(STAGE_);
        }
        
        private void OnEnable()
        {
            Initialize();
            
            foreach (var node in nodeDictionary)
            {
                foreach (var item in node)
                {
                    AssignDoor(item);
                }
            }
        }
        
        private void AssignDoor(Node item)
        {
            switch (item.nodeType)
            {
                case NodeType.Stage1 or NodeType.Stage2 or NodeType.Stage3 or NodeType.Stage4:
                    ++stageCount;
                    item.nodeName = stageName.Append(stageCount).ToString();
                    item.SetPortalPrefab(expDoor);
                    
                    //stage_1 => stage_2
                    stageName.Remove(stageName.Length - stageCount.ToString().Length, stageCount.ToString().Length);
                    break;
                case NodeType.Event:
                    item.SetPortalPrefab(eventDoor);
                    break;
                case NodeType.Point:
                    item.SetPortalPrefab(pointDoor);
                    break;
                case NodeType.Challenge:
                    item.SetPortalPrefab(challengeDoor);
                    break;
                case NodeType.Store:
                    item.SetPortalPrefab(storeDoor);
                    break;
                case NodeType.Boss1 or NodeType.Boss2 or NodeType.Boss3 or NodeType.Boss4:
                    item.SetPortalPrefab(bossDoor);
                    break;
                case NodeType.Rest:
                    item.SetPortalPrefab(restDoor);
                    break;
                case NodeType.Trap:
                    item.SetPortalPrefab(trapDoor);
                    break;
                case NodeType.Ending:
                    item.SetPortalPrefab(endingDoor);
                    break;
                case NodeType.None:
                    break;
            }
        }
        
        public void IncreaseNodeIndex()
        {
            string activeScene = SceneManager.GetActiveScene().name;
            
            //only Stage Scene Increase 
            if (activeScene.StartsWith(STAGE, StringComparison.OrdinalIgnoreCase))
                ++currentNodeIndex;
            
        }
        
        public int GetCurrentNodeIndex() => currentNodeIndex;
        public NodeType GetCurrentStageType() => nodeDictionary.GetCurrentStageType();
                
        public Node[] GetNodes()
        {
            return nodeDictionary.GetRandomNodes(ref currentNodeIndex);
        }
        
        public string GetNodeNameByNodeType(NodeType nodeType)
        {
            return nodeDictionary[nodeType];
        }

        public void RemoveNode(string nodeName)
        {
            foreach (var node in nodeDictionary)
            {
                for (int i = 0; i < node.Count; i++)
                {
                    if (node[i].nodeType != NodeType.Rest &&
                        node[i].nodeType != NodeType.Event &&
                        node[i].nodeType != NodeType.Challenge &&
                        node[i].nodeType != NodeType.Store &&
                        node[i].nodeType != NodeType.Trap && 
                        node[i].nodeName == nodeName)
                    {
                        node.RemoveAt(i);
                        break;
                    }
                    
                }
            }
        }
        
        [ContextMenu("print")]
        public void PrintNode()
        {
            nodeDictionary.printNodes();
        }
        
    }
}
