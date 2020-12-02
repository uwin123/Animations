using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

public class XMLManager : MonoBehaviour
{
    public static XMLManager instance;

    //xml 파일
    public TextAsset enemyFileXml;

    //여러 개의 변수들을 넣어서 구조체 하나를 한 개의 상자처럼 간주하고 사용할 수 있음.
    struct Monparams
    {
        //xml 파일로 부터 각각의 몬스터의 대해서 이들 파라미터 값을 읽어 들이고 구조체 내부 변수에 저장하고 구조체를 이용하여 각 몬스터에게 파라미터 값을 전달한다.
        public string name;
        public int level;
        public int maxHp;
        public int attackMin;
        public int attackMax;
        public int defense;
        public int exp;
        public int rewardMoney;
    }

    //딕셔너리의 키 값으로 적의 이름을 사용할 에정으로, string 타입으로 하고 데이터 값으로는 구조체를 이용. MonParams으로 저장. 
    Dictionary<string, Monparams> dicMonsters = new Dictionary<string, Monparams>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        MakeMonsterXML();
    }
    
    //XML로부터 파라미터 값 읽어 들이기
    void MakeMonsterXML()
    {
        XmlDocument monsterXMLDoc = new XmlDocument();
        monsterXMLDoc.LoadXml(enemyFileXml.text);

        XmlNodeList monsterNodeList = monsterXMLDoc.GetElementsByTagName("row");

        //노드 리스트로부터 각각의 노드를 뽑아냄. 
        foreach(XmlNode monsterNode in monsterNodeList)
        {
            Monparams monParams = new Monparams();
            foreach(XmlNode childNode in monsterNode.ChildNodes)
            {
                if(childNode.Name == "name")
                {
                    //<name>smallspider</name>
                    monParams.name = childNode.InnerText;
                }
                if(childNode.Name == "level")
                {
                    //<level>1</level> Int16.Parse()은 문자열을 정수로 바꿔줌.
                    monParams.level = Int16.Parse(childNode.InnerText);
                }
            }
        }
    }
}
