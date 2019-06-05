using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {

    public TextMeshProUGUI Title;
    public TextMeshProUGUI SubTitle;
    public TextMeshProUGUI Description;
    public Sprite[] Sprites;
    public Image Picture;
    public GameObject Panel;
    public string[] Titles;
    public string[] SubTitles;
    public string[] Descriptions;

    public void CloseWindow()
    {
        Picture.sprite = null;
        Title.text = "";
        SubTitle.text = "";
        Description.text = "";
        Panel.SetActive(false);
    }

    public void OpenWindow(string selectedItem)
    {
        if (selectedItem != "")
        {
            switch (selectedItem)
            {
                case "Square":
                    Title.text = Titles[0];
                    SubTitle.text = SubTitles[0];
                    Description.text = Descriptions[0];
                    Picture.sprite = Sprites[0];
                    break;
                case "Ball":
                    Title.text = Titles[1];
                    SubTitle.text = SubTitles[1];
                    Description.text = Descriptions[1];
                    Picture.sprite = Sprites[1];
                    break;
                case "Triangle":
                    Title.text = Titles[2];
                    SubTitle.text = SubTitles[2];
                    Description.text = Descriptions[2];
                    Picture.sprite = Sprites[2];
                    break;
                case "Hexagon":
                    Title.text = Titles[3];
                    SubTitle.text = SubTitles[3];
                    Description.text = Descriptions[3];
                    Picture.sprite = Sprites[3];
                    break;
                case "Rhombus":
                    Title.text = Titles[4];
                    SubTitle.text = SubTitles[4];
                    Description.text = Descriptions[4];
                    Picture.sprite = Sprites[4];
                    break;
                case "DuplicatingBrick":
                    Title.text = Titles[5];
                    SubTitle.text = SubTitles[5];
                    Description.text = Descriptions[5];
                    Picture.sprite = Sprites[5];
                    break;
                case "Demonic":
                    Title.text = Titles[6];
                    SubTitle.text = SubTitles[6];
                    Description.text = Descriptions[6];
                    Picture.sprite = Sprites[6];
                    break;
                case "LineKiller":
                    Title.text = Titles[7];
                    SubTitle.text = SubTitles[7];
                    Description.text = Descriptions[7];
                    Picture.sprite = Sprites[7];
                    break;
                case "TNT":
                    Title.text = Titles[8];
                    SubTitle.text = SubTitles[8];
                    Description.text = Descriptions[8];
                    Picture.sprite = Sprites[8];
                    break;
                case "Grenade":
                    Title.text = Titles[9];
                    SubTitle.text = SubTitles[9];
                    Description.text = Descriptions[9];
                    Picture.sprite = Sprites[9];
                    break;
                case "Ammo":
                    Title.text = Titles[10];
                    SubTitle.text = SubTitles[10];
                    Description.text = Descriptions[10];
                    Picture.sprite = Sprites[10];
                    break;
                case "Fire":
                    Title.text = Titles[11];
                    SubTitle.text = SubTitles[11];
                    Description.text = Descriptions[11];
                    Picture.sprite = Sprites[11];
                    break;
                case "Lightning":
                    Title.text = Titles[12];
                    SubTitle.text = SubTitles[12];
                    Description.text = Descriptions[12];
                    Picture.sprite = Sprites[12];
                    break;
                case "Plasma":
                    Title.text = Titles[13];
                    SubTitle.text = SubTitles[13];
                    Description.text = Descriptions[13];
                    Picture.sprite = Sprites[13];
                    break;
                case "Nuke":
                    Title.text = Titles[14];
                    SubTitle.text = SubTitles[14];
                    Description.text = Descriptions[14];
                    Picture.sprite = Sprites[14];
                    break;
                case "Triple":
                    Title.text = Titles[15];
                    SubTitle.text = SubTitles[15];
                    Description.text = Descriptions[15];
                    Picture.sprite = Sprites[15];
                    break;
                case "Rotator":
                    Title.text = Titles[16];
                    SubTitle.text = SubTitles[16];
                    Description.text = Descriptions[16];
                    Picture.sprite = Sprites[16];
                    break;
                case "Slowy":
                    Title.text = Titles[17];
                    SubTitle.text = SubTitles[17];
                    Description.text = Descriptions[17];
                    Picture.sprite = Sprites[17];
                    break;
                case "FlippyFloop":
                    Title.text = Titles[18];
                    SubTitle.text = SubTitles[18];
                    Description.text = Descriptions[18];
                    Picture.sprite = Sprites[18];
                    break;
                case "Grower":
                    Title.text = Titles[19];
                    SubTitle.text = SubTitles[19];
                    Description.text = Descriptions[19];
                    Picture.sprite = Sprites[19];
                    break;
                case "RotatorPlus":
                    Title.text = Titles[20];
                    SubTitle.text = SubTitles[20];
                    Description.text = Descriptions[20];
                    Picture.sprite = Sprites[20];
                    break;
                case "Shrinker":
                    Title.text = Titles[21];
                    SubTitle.text = SubTitles[21];
                    Description.text = Descriptions[21];
                    Picture.sprite = Sprites[21];
                    break;
                case "Abductor":
                    Title.text = Titles[22];
                    SubTitle.text = SubTitles[22];
                    Description.text = Descriptions[22];
                    Picture.sprite = Sprites[22];
                    break;
                case "Downer":
                    Title.text = Titles[23];
                    SubTitle.text = SubTitles[23];
                    Description.text = Descriptions[23];
                    Picture.sprite = Sprites[23];
                    break;
                case "Doubler":
                    Title.text = Titles[24];
                    SubTitle.text = SubTitles[24];
                    Description.text = Descriptions[24];
                    Picture.sprite = Sprites[24];
                    break;
                case "Wall":
                    Title.text = Titles[25];
                    SubTitle.text = SubTitles[25];
                    Description.text = Descriptions[25];
                    Picture.sprite = Sprites[25];
                    break;
                case "Tenfolder":
                    Title.text = Titles[26];
                    SubTitle.text = SubTitles[26];
                    Description.text = Descriptions[26];
                    Picture.sprite = Sprites[26];
                    break;
                case "VisionEater":
                    Title.text = Titles[27];
                    SubTitle.text = SubTitles[27];
                    Description.text = Descriptions[27];
                    Picture.sprite = Sprites[27];
                    break;
                case "Joker":
                    Title.text = Titles[28];
                    SubTitle.text = SubTitles[28];
                    Description.text = Descriptions[28];
                    Picture.sprite = Sprites[28];
                    break;
                default:
                    break;
            }

            Panel.SetActive(true);
        }
    }
}