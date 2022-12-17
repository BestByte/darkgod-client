/****************************************************
    文件：BuyWnd.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2019/2/23 22:38:25
	功能：购买交易窗口
*****************************************************/

using PEProtocol;
using UnityEngine;
using UnityEngine.UI;

public class BuyWnd : WindowRoot {
    public Text txtInfo;
    public Button btnSure;

    private int buyType;//0：体力 1：金币

    public void SetBuyType(int type) {
        this.buyType = type;
    }

    protected override void InitWnd() {
        base.InitWnd();
        btnSure.interactable = true;
        RefreshUI();
    }

    private void RefreshUI() {
        switch (buyType) {
            case 0:
                //体力
                txtInfo.text = "是否花费" + Constants.Color("10钻石", TxtColor.Red) + "购买" + Constants.Color("100体力", TxtColor.Green) + "?";
                break;
            case 1:
                //金币
                txtInfo.text = "是否花费" + Constants.Color("10钻石", TxtColor.Red) + "购买" + Constants.Color("1000金币", TxtColor.Green) + "?";
                break;
        }
    }

    public void ClickSureBtn() {
        AudioService.PlayUIAudio(Constants.UIClickBtn);

        //发送网络购买消息 
        GameMsg msg = new GameMsg {
            cmd = (int)CMD.ReqBuy,
            reqBuy = new ReqBuy {
                type = buyType,
                cost = 10
            }
        };

        NetService.SendMsg(msg);
        btnSure.interactable = false;
    }

    public void ClickCloseBtn() {
        AudioService.PlayUIAudio(Constants.UIClickBtn);
        SetWndState(false);
    }
}