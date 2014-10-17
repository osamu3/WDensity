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
			int forePicBxW = 0,		//前景：幅
			int forePicBxH = 0,		//前景：高
			int hrzBrForeMax = 0,	//水平バー前景Max
			int vrtBrForeMax = 0,	//垂直バー前景Max
			int loadImgForeW = 0,	//読込画像前景：幅
			int loadImgForeH = 0,	//読込画像前景：高
			int canvsForeW = 0,		//カンバス前景：幅
			int canvsForeH = 0,		//カンバス前景：高
			int hrzBrForeMoveV = 9999,	//水平移動量：前景
			int vrtBrForeMoveV = 0,	//垂直移動量：前景
			int offstForeX = 0,		//オフセットX：前景
			int offstForeY =0,		//オフセットY：前景

			int backPicBxW = 0,		//背景：幅
			int backPicBxH = 0,		//背景：高
			int hrzBrBackMax = 0,	//水平バー背景Max
			int vrtBrBackMax = 0,	//垂直バー背景Max
			int loadImgBackW = 0,	//読込画像背景：幅
			int loadImgBackH = 0,	//読込画像背景：高
			int canvsBackW = 0,		//カンバス背景：幅
			int canvsBackH = 0,		//カンバス背景：高
			int hrzBrBackMoveV = 0,	//水平移動量：背景
			int vrtBrBackMoveV = 0,	//垂直移動量：背景
			int offstBackX =0,		//オフセットX：前景
			int offstBackY =0		//オフセットY：前景
		){
			//背景
			//ピクチャーボックスの大きさ(背景)
			if (backPicBxW != 9999) form.lblBckPicBxW.Text = backPicBxW.ToString("#,##0").PadLeft(5);
			if (backPicBxH != 9999) form.lblBckPicBxH.Text = backPicBxH.ToString("#,##0").PadLeft(5);

			//トラックバーの最大値(背景)
			if (hrzBrBackMax != 9999) form.lblHrzBrBckMax.Text = hrzBrBackMax.ToString("#,##0").PadLeft(5);
			if (vrtBrBackMax != 9999) form.lblVrtBrBckMax.Text = vrtBrBackMax.ToString("#,##0").PadLeft(5);

			//読込画像の大きさ(背景)
			if (loadImgBackW != 9999) form.lblLoadBckImgW.Text = loadImgBackW.ToString("#,##0").PadLeft(5);
			if (loadImgBackH != 9999) form.lblLoadBckImgH.Text = loadImgBackH.ToString("#,##0").PadLeft(5);

			//カンバスの大きさ(背景)
			if (canvsBackW != 9999) form.lblCanvsBckW.Text = canvsBackW.ToString("#,##0").PadLeft(5);
			if (canvsBackH != 9999) form.lblCanvsBckH.Text = canvsBackH.ToString("#,##0").PadLeft(5);

			//トラックバーの移動量(背景)
			if (hrzBrBackMoveV != 9999) form.lblHrzBrBckMoveV.Text = hrzBrBackMoveV.ToString("#,##0").PadLeft(5);
			if (vrtBrBackMoveV != 9999) form.lblVrtBrBckMoveV.Text = vrtBrBackMoveV.ToString("#,##0").PadLeft(5);

			//オフセット量(背景)
			if (offstBackX != 9999) form.lblOffstBckX.Text = offstBackX.ToString("#,##0").PadLeft(5);
			if (offstBackY != 9999) form.lblOffstBckY.Text = offstBackY.ToString("#,##0").PadLeft(5);

			//前景
			//ピクチャーボックスの大きさ(前景)
			if (forePicBxW != 9999) form.lblFrtPicBxW.Text = forePicBxW.ToString("#,##0").PadLeft(5);
			if (forePicBxH != 9999) form.lblFrtPicBxH.Text = forePicBxH.ToString("#,##0").PadLeft(5);

			if (hrzBrForeMax != 9999) form.lblHrzBrFrtMax.Text = hrzBrForeMax.ToString("#,##0").PadLeft(5);
			if (vrtBrForeMax != 9999) form.lblVrtBrFrtMax.Text = vrtBrForeMax.ToString("#,##0").PadLeft(5);

			if (loadImgForeW != 9999) form.lblLoadFrtImgW.Text = loadImgForeW.ToString("#,##0").PadLeft(5);
			if (loadImgForeH != 9999) form.lblLoadFrtImgH.Text = loadImgForeH.ToString("#,##0").PadLeft(5);

			if (canvsForeW != 9999) form.lblCanvsFrtW.Text = canvsForeW.ToString("#,##0").PadLeft(5);
			if (canvsForeH != 9999) form.lblCanvsFrtH.Text = canvsForeH.ToString("#,##0").PadLeft(5);

			if (hrzBrForeMoveV != 9999) form.lblHrzBrFrtMoveV.Text = hrzBrForeMoveV.ToString("#,##0").PadLeft(5);
			if (vrtBrForeMoveV != 9999) form.lblVrtBrFrtMoveV.Text = vrtBrForeMoveV.ToString("#,##0").PadLeft(5);
		}
	}

}
