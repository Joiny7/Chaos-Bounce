using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class DropdownController : MonoBehaviour {

    Dropdown dropdown;

    private Dropdown Dropdown
    {
        get
        {
            if(dropdown == null) {
                dropdown = GetComponent<Dropdown> ();
            }
            return dropdown;
        }
    }

    public Action<Achievements> onValueChanged;

    private void Start () {
        UpdateOptions ();
        Dropdown.onValueChanged.AddListener (HandlerDropdownValueChanged);
    }

    private void HandlerDropdownValueChanged (int value) {
        if(onValueChanged != null) {
            onValueChanged ((Achievements)value);
        }
    }

    [ContextMenu("UpdateOptions()")]
    public void UpdateOptions() {
        Dropdown.options.Clear ();
        var values = Enum.GetValues (typeof (Achievements));
        foreach (Achievements achievement in values) {
            Dropdown.options.Add (new Dropdown.OptionData (achievement.ToString ()));
        }
        Dropdown.RefreshShownValue ();
    }


}
