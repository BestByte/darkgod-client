/****************************************************
    文件：GuideWnd.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/17 4:52:13
	功能：引导对话界面
*****************************************************/

using PEProtocol;
using UnityEngine;
using UnityEngine.UI;

public class GuideWnd : WindowRoot {
    public Text txtName;
    public Text txtTalk;
    public Image imgIcon;

    private PlayerData pd;
    private AutoGuideCfg curtTaskData;
    private string[] dialogArr;
    private int index;

    protected override void InitWnd() {
        base.InitWnd();

        pd = GameRoot.Instance.PlayerData;
        curtTaskData = MainCitySystem.Instance.GetCurtTaskData();
        dialogArr = curtTaskData.dilogArr.Split('#');
        index = 1;

        SetTalk();
    }

    private void SetTalk() {
        string[] talkArr = dialogArr[index].Split('|');
        if (talkArr[0] == "0") {
            //自己
            SetSprite(imgIcon, PathDefine.SelfIcon);
            SetText(txtName, pd.name);
        }
        else {
            //对话NPC
            switch (curtTaskData.npcID) {
                case 0:
                    SetSprite(imgIcon, PathDefine.WiseManIcon);
                    SetText(txtName, "智者");
                    break;
                case 1:
                    SetSprite(imgIcon, PathDefine.GeneralIcon);
                    SetText(txtName, "将军");
                    break;
                case 2:
                    SetSprite(imgIcon, PathDefine.ArtisanIcon);
                    SetText(txtName, "工匠");
                    break;
                case 3:
                    SetSprite(imgIcon, PathDefine.TraderIcon);
                    SetText(txtName, "商人");
                    break;
                default:
                    SetSprite(imgIcon, PathDefine.GuideIcon);
                    SetText(txtName, "小芸");
                    break;
            }
        }

        imgIcon.SetNativeSize();
        SetText(txtTalk, talkArr[1].Replace("$name", pd.name));
    }


    public void ClickNextBtn() {
        AudioService.PlayUIAudio(Constants.UIClickBtn);

        index += 1;
        if (index == dialogArr.Length) {
            //TODO 发送任务引导完成信息
            GameMsg msg = new GameMsg {
                cmd = (int)CMD.ReqGuide,
                reqGuide = new ReqGuide {
                    guideid = curtTaskData.ID
                }
            };

            NetService.SendMsg(msg);
            SetWndState(false);
        }
        else {
            SetTalk();
        }
    }
}