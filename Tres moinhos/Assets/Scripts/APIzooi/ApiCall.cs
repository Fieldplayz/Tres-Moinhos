using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class Tree
{
    public int treeNr { get; set; }
    public string zone { get; set; }
    public string coordinate { get; set; }
    public int height { get; set; }
    public int circumference { get; set; }
    public int volume { get; set; }
    public int bioConcentrationId { get; set; }
    public string treeName { get; set; }
    public object bioConcentration { get; set; }
    public object treeNameNavigation { get; set; }
    public object zoneNavigation { get; set; }
}

public class ApiCall : MonoBehaviour
{
    private List<Tree> trees = new List<Tree>();

    private string apiCompleteURL = "https://a3e7-213-13-34-216.ngrok-free.app/api/Tree?$filter=Zone%20eq%20%27";

    [SerializeField]
    private string apiURL = "https://a3e7-213-13-34-216.ngrok-free.app/api/";

    private string tempTrees = "tree?$filter=Zone%20eq%20%27";

    private string zoneID = "C";

    void Start()
    {
        StartCoroutine(GetTreesPerZone("A"));
    }

    public IEnumerator GetTreesPerZone(string zone)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiURL + tempTrees + zone + "%27"); //Zone moet nog in bepaalde quotes ofzo
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);                
            Tree[] tree = JsonConvert.DeserializeObject<Tree[]>(request.downloadHandler.text);
            GetComponent<ZoneGenerator>().SpawnTrees(tree.Length);
            Debug.Log(tree.Length);
        }
    }

}
