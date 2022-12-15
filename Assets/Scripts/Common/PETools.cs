/****************************************************
	文件：PETools.cs
	作者：Plane
	邮箱: 1785275942@qq.com
	日期：2018/12/05 4:01   	
	功能：工具类
*****************************************************/

/// <summary>
/// 创建一个PETools工具脚本，这里用来生成一个随机数，用来随机名字
/// </summary>
public class PETools {
    public static int RDInt(int min, int max, System.Random rd = null) {
        if (rd == null) {
            rd = new System.Random();
        }
        int val = rd.Next(min, max + 1);
        return val;
    }
}
