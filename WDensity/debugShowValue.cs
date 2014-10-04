using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WDensity {
	class DbgShowVal {
		private Form1 form;
		private int frtPicW;//前景：幅
		private int frtPicH;//前景：高
		private int bckPicW;//前景：高
		private int bckPicH;//背景：高

		private int vrtBrFrtV;//水平バー前景Max
		private int hrzBrFrtV;//垂直バー前景Max
		private int vrtBrBckV;//水平バー背景Max
		private int hrzBrBckV;//垂直バー背景Max

		private int loadImgW;//読込画像：幅
		private int loadImgH;//読込画像：高

		private int canvsW;//カンバス：幅
		private int canvsH;//カンバス：高

		private int hrzBrFrtMoveV;//水平移動量：前景
		private int vrtBrFrtMoveV;//垂直移動量：前景
		private int hrzBrBckMoveV;//水平移動量：背景
		private int vrtBrBckMoveV;//垂直移動量：背景

		public DbgShowVal(Form1 f) {
			form = f;
		}
		public void setVal(
			int frtPicBxW = 0,//前景：幅
			int frtPicBxH = 0,//前景：高
			int bckPicBxW = 0,//前景：高
			int bckPicBxH = 0,//背景：高
			int hrzBrFrtV = 0,//水平バー前景Max
			int vrtBrFrtV = 0,//垂直バー前景Max
			int hrzBrBckV = 0,//水平バー背景Max
			int vrtBrBckV = 0,//垂直バー背景Max
			int loadImgW = 0,//読込画像：幅
			int loadImgH = 0,//読込画像：高
			int canvsW = 0,//カンバス：幅
			int canvsH = 0,//カンバス：高
			int hrzBrFrtMoveV = 0,//水平移動量：前景
			int vrtBrFrtMoveV = 0,//垂直移動量：前景
			int hrzBrBckMoveV = 0,//水平移動量：背景
			int vrtBrBckMoveV = 0//垂直移動量：背景
		){
			form.lblFrtPicBxW.Text = frtPicBxW.ToString();
			form.lblFrtPicBxH.Text = frtPicBxH.ToString();

			form.lblBckPicBxW.Text = bckPicBxW.ToString();
			form.lblBckPicBxH.Text = bckPicBxH.ToString();

			form.lblHrzBrFrtV.Text = hrzBrFrtV.ToString();
			form.lblVrtBrFrtV.Text = vrtBrFrtV.ToString();
			form.lblHrzBrBckV.Text = hrzBrBckV.ToString();
			form.lblVrtBrBckV.Text = vrtBrBckV.ToString();

			form.lblLoadImgH.Text = loadImgH.ToString();
			form.lblLoadImgW.Text = loadImgW.ToString();

			form.lblCanvsW.Text = canvsW.ToString();
			form.lblCanvsH.Text = canvsH.ToString();

			form.lblHrzBrFrtMoveV.Text = hrzBrFrtMoveV.ToString();
			form.lblVrtBrFrtMoveV.Text = vrtBrFrtMoveV.ToString();
			form.lblHrzBrBckMoveV.Text = hrzBrBckMoveV.ToString();
			form.lblVrtBrBckMoveV.Text = vrtBrBckMoveV.ToString();
		}
	}

}
