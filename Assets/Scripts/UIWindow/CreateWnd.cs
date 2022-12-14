/****************************************************
    文件：CreateWnd.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2018/12/5 0:47:18
	功能：角色创建界面
*****************************************************/

using PEProtocol;
using UnityEngine;
using UnityEngine.UI;

public class CreateWnd : WindowRoot {
    public InputField iptName;

    protected override void InitWnd() {
        base.InitWnd();

        //显示一个随机名字
        iptName.text = ResService.GetRDNameData(false);
    }
	/// <summary>
	/// 在CreateWnd中调用产生随机名字的方法，并且添加按钮点击的方法
	/// </summary>
	public void ClickRandBtn() {
        AudioService.PlayUIAudio(Constants.UIClickBtn);

        string rdName = ResService.GetRDNameData(false);
        iptName.text = rdName;
    }

    public void ClickEnterBtn() {
        AudioService.PlayUIAudio(Constants.UIClickBtn);

        if (iptName.text != "") {
            //发送名字数据到服务器，登录主城
            GameMsg msg = new GameMsg {
                cmd = (int)CMD.ReqRename,
                reqRename = new ReqRename {
                    name = iptName.text
                }
            };
            NetService.SendMsg(msg);
        }
        else {
            GameRoot.AddTips("当前名字不符合规范");
        }
    }
}