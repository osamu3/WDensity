using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WDensity {
	/// <summary>
	/// cf:ref:別クラスから別フォーム(別From)のコントロールを呼び出し（制御）
	/// cf:ref:クラスメソッド呼び出し引数を可変長、且つ名前でコール(呼び出し)
	/// </summary>
	class DbgShowVal {
		private Form1 form;
/*		private int frtPicW;//前景：幅
		private int frtPicH;//前景：高
		private int vrtBrFrtV;//水平バー前景Max
		private int hrzBrFrtV;//垂直バー前景Max
		private int loadImgFrtW;//前景読込画像：幅
		private int loadImgFrtH;//前景読込画像：高
		private int canvsFrtW;//前景カンバス：幅
		private int canvsFrtH;//前景カンバス：高
		private int hrzBrFrtMoveV;//水平移動量：前景
		private int vrtBrFrtMoveV;//垂直移動量：前景
		private int offstFrtX;//オフセットX：前景
		private int offstFrtY;//オフセットY：前景

		private int bckPicW;//背景：高
		private int bckPicH;//背景：高
		private int vrtBrBckV;//水平バー背景Max
		private int hrzBrBckV;//垂直バー背景Max
		private int loadImgBckW;//背景読込画像：幅
		private int loadImgBckH;//背景読込画像：高
		private int canvsBckW;//背景カンバス：幅
		private int canvsBckH;//背景カンバス：高
		private int hrzBrBckMoveV;//水平移動量：背景
		private int vrtBrBckMoveV;//垂直移動量：背景
		private int offstBckX;//オフセットX：背景
		private int offstBckY;//オフセットY：背景
*/
		public DbgShowVal(Form1 f) {
			form = f;
		}
		// cf:ref:数値の文字列化に際し、,（カンマ）区切りの空白右詰で表示している。
		public void setVal(
			int frtPicBxW = 0,		//前景：幅
			int frtPicBxH = 0,		//前景：高
			int hrzBrFrtMax = 0,	//水平バー前景Max
			int vrtBrFrtMax = 0,	//垂直バー前景Max
			int loadImgFrtW = 0,	//読込画像前景：幅
			int loadImgFrtH = 0,	//読込画像前景：高
			int canvsFrtW = 0,		//カンバス前景：幅
			int canvsFrtH = 0,		//カンバス前景：高
			int hrzBrFrtMoveV = 0,	//水平移動量：前景
			int vrtBrFrtMoveV = 0,	//垂直移動量：前景
			int offstFrtX = 0,		//オフセットX：前景
			int offstFrtY =0,		//オフセットY：前景

			int bckPicBxW = 0,		//背景：幅
			int bckPicBxH = 0,		//背景：高
			int hrzBrBckMax = 0,	//水平バー背景Max
			int vrtBrBckMax = 0,	//垂直バー背景Max
			int loadImgBckW = 0,	//読込画像背景：幅
			int loadImgBckH = 0,	//読込画像背景：高
			int canvsBckW = 0,		//カンバス背景：幅
			int canvsBckH = 0,		//カンバス背景：高
			int hrzBrBckMoveV = 0,	//水平移動量：背景
			int vrtBrBckMoveV = 0,	//垂直移動量：背景
			int offstBckX =0,		//オフセットX：前景
			int offstBckY =0		//オフセットY：前景
		){
			//背景
			//ピクチャーボックスの大きさ(背景)
			if (bckPicBxW != 0) form.lblBckPicBxW.Text = bckPicBxW.ToString("#,###").PadLeft(5);
			if (bckPicBxH != 0) form.lblBckPicBxH.Text = bckPicBxH.ToString("#,###").PadLeft(5);

			//トラックバーの最大値(背景)
			if (hrzBrBckMax != 0) form.lblHrzBrBckMax.Text = hrzBrBckMax.ToString("#,###").PadLeft(5);
			if (vrtBrBckMax != 0) form.lblVrtBrBckMax.Text = vrtBrBckMax.ToString("#,###").PadLeft(5);

			//読込画像の大きさ(背景)
			if (loadImgBckW != 0) form.lblLoadBckImgW.Text = loadImgBckW.ToString("#,###").PadLeft(5);
			if (loadImgBckH != 0) form.lblLoadBckImgH.Text = loadImgBckH.ToString("#,###").PadLeft(5);

			//カンバスの大きさ(背景)
			if (canvsBckW != 0) form.lblCanvsBckW.Text = canvsBckW.ToString("#,###").PadLeft(5);
			if (canvsBckH != 0) form.lblCanvsBckH.Text = canvsBckH.ToString("#,###").PadLeft(5);

			//トラックバーの移動量(背景)
			if (hrzBrBckMoveV != 0) form.lblHrzBrBckMoveV.Text = hrzBrBckMoveV.ToString("#,###").PadLeft(5);
			if (vrtBrBckMoveV != 0) form.lblVrtBrBckMoveV.Text = vrtBrBckMoveV.ToString("#,###").PadLeft(5);

			//オフセット量(背景)
			if (offstBckX != 0) form.lblOffstBckX.Text = offstBckX.ToString("#,###").PadLeft(5);
			if (offstBckY != 0) form.lblOffstBckY.Text = offstBckY.ToString("#,###").PadLeft(5);

			//前景
			//ピクチャーボックスの大きさ(前景)
			if (frtPicBxW != 0) form.lblFrtPicBxW.Text = frtPicBxW.ToString("#,###").PadLeft(5);
			if (frtPicBxH != 0) form.lblFrtPicBxH.Text = frtPicBxH.ToString("#,###").PadLeft(5);

			if (hrzBrFrtMax != 0) form.lblHrzBrFrtMax.Text = hrzBrFrtMax.ToString("#,###").PadLeft(5);
			if (vrtBrFrtMax != 0) form.lblVrtBrFrtMax.Text = vrtBrFrtMax.ToString("#,###").PadLeft(5);

			if (loadImgFrtW != 0) form.lblLoadFrtImgW.Text = loadImgFrtW.ToString("#,###").PadLeft(5);
			if (loadImgFrtH != 0) form.lblLoadFrtImgH.Text = loadImgFrtH.ToString("#,###").PadLeft(5);

			if (canvsFrtW != 0) form.lblCanvsFrtW.Text = canvsFrtW.ToString("#,###").PadLeft(5);
			if (canvsFrtH != 0) form.lblCanvsFrtH.Text = canvsFrtH.ToString("#,###").PadLeft(5);

			if (hrzBrFrtMoveV != 0) form.lblHrzBrFrtMoveV.Text = hrzBrFrtMoveV.ToString("#,###").PadLeft(5);
			if (vrtBrFrtMoveV != 0) form.lblVrtBrFrtMoveV.Text = vrtBrFrtMoveV.ToString("#,###").PadLeft(5);
		}
	}

}
