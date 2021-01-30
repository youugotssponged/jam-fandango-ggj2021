using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LiveScreenController : MonoBehaviour
{
    public Text[] initials = new Text[3];
    private EntryController ec;
    private LeaderboardController lbc;
    // Start is called before the first frame update
    void Start()
    {
        ec = new EntryController(initials);
        lbc = new LeaderboardController();
    }

    public void LoadMainMenu(){
        //update player object with new initials
        //playerObject.initials = ec.getInitials();
        //lbc.addEntry(/*player object*/);
        Debug.Log(ec.getInitials());
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
