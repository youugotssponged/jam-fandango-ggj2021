using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveScreenController : MonoBehaviour
{
    public GameObject obj;
    private GameManager gm;
    public Text[] initials = new Text[3];
    private EntryController ec;
    private LeaderboardController lbc;
    // Start is called before the first frame update
    void Start()
    {
        gm = obj.GetComponent<GameManager>();
        ec = new EntryController(initials);
        lbc = new LeaderboardController();
    }

    public void LoadMainMenu(){
        Player p = new Player();
        p.playerInitials = ec.getInitials();
        p.time = gm.getTime();
        lbc.addEntry(p);
        lbc.saveEntries();
        SceneManager.LoadScene(1);
    }

    public void upButtonPressed(int i) {
        ec.upButtonPressed(i);
    }

    public void downButtonPressed(int i) {
        ec.downButtonPressed(i);
    }
}
