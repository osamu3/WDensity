using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WDensity {
/// <summary>
/// ファイルの画像は、イメージオブジェクトに読み込まれ、
/// ビットマップオブジェクト型のキャンバスに描画され
/// キャンバスのグラフィックオブジェクトのワールド座標変換メソッドにより移動／拡大・縮小／変形され
/// 画面表示は、ピクチャーボックスコントロールで行なわれる。
/// 
/// </summary>


	public partial class Form1 : Form {
		//オブジェクトの宣言のみ(実体はイニシャルメソッドの中で作成)
		private static Bitmap _CanvasBck;//背景描画先(canvas)とするビットマップオブジェクトの宣言
		private static Bitmap _CanvasFrt;//前景描画先(canvas)とするビットマップオブジェクトの宣言
		private static Image _LoadedBckImgObj;//読み込んだ背景画像のイメージオブジェクトの宣言
		private static Image _LoadedFrtImgObj;//読み込んだ前景描画のイメージオブジェクトの宣言


		public static float _ZoomRatio = 1F;//倍率
		//画像の描画領域(大きさ)と位置を保持する変数⇒領域を拡大・縮小することで画像を拡大・縮小できる。
		private static Rectangle _CanvsRctBckImg = new Rectangle(0, 0, 0, 0);//背景画像用変数
		private static Rectangle _CanvsRctFrtImg = new Rectangle(0, 0, 0, 0);//前景画像用

		private static int _BckPicDrwPsOfstX;//背景画像の中心＝背景描画キャンバスの中心となる位置へのオフセット
		private static int _BckPicDrwPsOfstY;
		private static int _FrtPicDrwPsOfstX;//前景画像の中心＝前景描画キャンバスの中心となる位置へのオフセット
		private static int _FrtPicDrwPsOfstY;

		private int _SclTrckBrInitVal; //　　縮尺　　　　の初期値

		private int _trckBrHrzFrtMaxVal; //前景移動用の水平移動トラックバーの最大値
		private int _trckBrVrtFrtMaxVal; //前景移動用の垂直移動トラックバーの最大値
		private int _trckBrHrzBckMaxVal; //背景移動用の水平移動トラックバーの最大値
		private int _trckBrVrtBckMaxVal; //背景移動用の垂直移動トラックバーの最大値
		private int _trckBrHrzFrtInitVal; //前景移動用の水平移動トラックバーの初期値
		private int _trckBrVrtFrtInitVal; //前景移動用の垂直移動トラックバーの初期値
		private int _trckBrHrzBckInitVal; //背景移動用の水平移動トラックバーの初期値
		private int _trckBrVrtBckInitVal; //背景移動用の垂直移動トラックバーの初期値
		private DbgShowVal dbgShowVal;//デバッグ用のクラス

		public enum _EdtTyp : int {
			Horizontal = 1, Vertical = 2, Scale = 3
		}

		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e) {
			//デバッグ用
			dbgShowVal = new DbgShowVal(this);
			
			//			if (canvas != null) canvas.Dispose();

			//画像を読み込む
			_LoadedBckImgObj = Image.FromFile("house.jpg");
			_LoadedFrtImgObj = Image.FromFile("traget.jpg");

			//描画先(canvas)のビットマップオブジェクトの実体を作成
			_CanvasBck = new Bitmap(_LoadedBckImgObj.Width, _LoadedBckImgObj.Height);
			_CanvasFrt = new Bitmap(_LoadedFrtImgObj.Width, _LoadedFrtImgObj.Height);
			//カンバスの大きさをセットする為の読込画像の大きさ⇒∴ピクチャーボックスの大きさ！＝画像の大きさ
			_CanvsRctBckImg = new Rectangle(0, 0, _LoadedBckImgObj.Width, _LoadedBckImgObj.Height);
			_CanvsRctFrtImg = new Rectangle(0, 0, _LoadedFrtImgObj.Width, _LoadedFrtImgObj.Height);


			_BckPicDrwPsOfstX = (_LoadedBckImgObj.Width - picBxBck.Width) / 2;
			_BckPicDrwPsOfstY = (_LoadedBckImgObj.Height - picBxBck.Height) / 2;
			_FrtPicDrwPsOfstX = (_LoadedFrtImgObj.Width - picBxFrt.Width) / 2;
			_FrtPicDrwPsOfstY = (_LoadedFrtImgObj.Height - picBxFrt.Height) / 2;
			//			zoomRatio = 1d;

			#region　【画像の初期表示】読み込んだ画像の中心が、カンバスの中心になるよう描画
			//　　　       〃　　　　　　　　　　前景画像　　　　　　〃
			drawPic2Center(picBxFrt, _CanvasFrt, _FrtPicDrwPsOfstX, _FrtPicDrwPsOfstY, _LoadedFrtImgObj, _CanvsRctFrtImg);
			//　　　       〃　　　　　　　　　　背景画像　　　　　　〃
			drawPic2Center(picBxBck, _CanvasBck, _BckPicDrwPsOfstX, _BckPicDrwPsOfstY, _LoadedBckImgObj, _CanvsRctBckImg);
			#endregion

			#region トラックバー値の初期設定
			//スライダーの【移動量の最大値】は、読込画像の大きさの１／２とする
			//前景処理用
			_trckBrHrzFrtMaxVal = _LoadedFrtImgObj.Width / 2; //トラックバーの最大値（水平移動用)
			_trckBrVrtFrtMaxVal = _LoadedFrtImgObj.Height / 2;//　　　　　〃　　    （垂直移動用)
			_trckBrHrzFrtInitVal = _trckBrHrzFrtMaxVal / 2;   //つまみの初期位置(読込画像の１／４)（水平移動用)
			_trckBrVrtFrtInitVal = _trckBrVrtFrtMaxVal / 2;   //　　　　　〃　                  　（垂直移動用)

			_trckBrHrzBckMaxVal = _LoadedBckImgObj.Width / 2; //トラックバーの最大値（水平移動用)
			_trckBrVrtBckMaxVal = _LoadedBckImgObj.Height / 2;//　　　　　〃　　    （垂直移動用)
			_trckBrHrzBckInitVal = _trckBrHrzBckMaxVal / 2;   //つまみの初期位置(読込画像の１／４)（水平移動用)
			_trckBrVrtBckInitVal = _trckBrVrtBckMaxVal / 2;   //　　　　　〃　                  　（垂直移動用)

			//背景画像の編集設定で起動
			//初期画面のトラックバーの位置を背景画像選択時の状態にする為、ラジオボタンを背景画像移動にしておく
			rdBtnBckPicMv.Checked = true;
			trckBrHrz.Maximum = _trckBrHrzBckMaxVal;
			trckBrVrt.Maximum = _trckBrVrtBckMaxVal;
			//スライダーのつまみの初期値設定
			trckBrHrz.Value = _trckBrHrzBckInitVal;
			trckBrVrt.Value = _trckBrVrtBckInitVal;
			//			_SclTrckBrInitVal = trckBrScl.Value;//縮尺トラックバーの初期位置を保存
			trckBrHrz.Size = new Size(picBxBck.Width, trckBrHrz.Size.Height);//幅をピクチャ―ボックスの幅とする
			trckBrVrt.Size = new Size(trckBrHrz.Size.Width, picBxBck.Height);//高さをピクチャ―ボックスの高さとする
			

			#endregion

			#region//デバッグ
			dbgShowVal.setVal(	bckPicBxW:	picBxBck.Width,
								bckPicBxH:	picBxBck.Height,
								vrtBrBckMax:_trckBrHrzBckMaxVal,
								hrzBrBckMax:_trckBrVrtBckMaxVal,
								loadImgBckW:_LoadedBckImgObj.Width,
								loadImgBckH:_LoadedBckImgObj.Height,
								canvsBckW:	_CanvasBck.Width,
								canvsBckH:	_CanvasBck.Height,
								vrtBrBckMoveV: _trckBrVrtBckInitVal - trckBrVrt.Value,
								hrzBrBckMoveV: _trckBrHrzBckInitVal - trckBrHrz.Value,
								offstBckX: _BckPicDrwPsOfstX,
								offstBckY: _BckPicDrwPsOfstY
			);
			#endregion
		
		}


		/// <summary>画像初期表示：読み込んだ画像の中心が、ピクチャーボックスの中心となる位置に表示
		/// </summary>
		/// <param name="picBox">画像表示先ピクチャーコントロール(カンバスの一部(全部)を表示)</param>
		/// <param name="canvas">画像描画先キャンバス</param>
		/// <param name="ofstX"></param>
		/// <param name="ofstY"></param>
		/// <param name="loadedImg">ファイルから読み込まれた画像</param>
		/// <param name="rct"></param>
		private void drawPic2Center(PictureBox picBox,Bitmap canvas, int ofstX, int ofstY, Image loadedImg, Rectangle rct) {
			Graphics g = Graphics.FromImage(canvas);
			//一旦画像クリア
			g.Clear(Color.Black);
			g.ResetTransform();
			//カンバスの中心がピクチャーボックス中止になるようオフセット分移動、カンバスのワールド変換行列を水平移動
			g.TranslateTransform(-ofstX, -ofstY);
			g.DrawImage(loadedImg, rct);

			//PictureBoxに表示する
			picBox.Image = canvas;

			g.Dispose();
		}

		/// <summary>トラックバーイベント⇒ 画像【移動】【縮尺】
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trckBr_Scroll(object sender, EventArgs e) {
			#region 画像【水平移動】トラックバーイベント
			if (((TrackBar)sender).Name == "trckBrHrz") {
				//前景処理なら
				if (rdBtnFrtPicMv.Checked) {
					//画像移動
					movePicture(picBxFrt,							//画像表示先ピクチャーボックスコントロール 
								_CanvasFrt,						//描画先カンバス
								_LoadedFrtImgObj,					//描画先カンバス
								_EdtTyp.Horizontal,					//読み込んだ画像
								_trckBrHrzFrtInitVal - trckBrHrz.Value,	//スライダーの移動量
								_CanvsRctFrtImg								//読込画像の大きさ＝カンバスの大きさ
								);
//dbgShowVal.setVal(hrzBrBckMoveV: _trckBrHrzFrtInitVal - trckBrHrz.Value);
				}
				//背景処理なら
				if (rdBtnBckPicMv.Checked) {
					movePicture(picBxBck, _CanvasBck, _LoadedBckImgObj,_EdtTyp.Horizontal,
								_trckBrHrzFrtInitVal - trckBrHrz.Value, _CanvsRctBckImg);
				}
			}
			#endregion

			#region 画像【垂直移動】トラックバーイベント
			if (((TrackBar)sender).Name == "trckBrVrt") {
				//前景？
				if (rdBtnFrtPicMv.Checked) {
					movePicture(picBxFrt, _CanvasFrt, _LoadedFrtImgObj, _EdtTyp.Vertical,
								_trckBrVrtFrtInitVal - trckBrVrt.Value, _CanvsRctFrtImg);
				}
				//背景？
				if (rdBtnBckPicMv.Checked) {
					movePicture(picBxBck, _CanvasBck, _LoadedBckImgObj, _EdtTyp.Vertical,
								_trckBrVrtFrtInitVal - trckBrVrt.Value, _CanvsRctBckImg);
				}
			}
			#endregion

			#region 画像【縮尺】トラックバーイベント
			if (((TrackBar)sender).Name == "trckBrScl") {
				//前景？
				if (rdBtnFrtPicMv.Checked) {
					movePicture(picBxFrt, _CanvasFrt, _LoadedFrtImgObj, _EdtTyp.Scale,
								trckBrScl.Value - _SclTrckBrInitVal, _CanvsRctFrtImg);
				}
				//背景？
				if (rdBtnBckPicMv.Checked) {
					movePicture(picBxBck, _CanvasBck, _LoadedBckImgObj, _EdtTyp.Scale,
								trckBrScl.Value - _SclTrckBrInitVal, _CanvsRctBckImg);
				}
			}
			#endregion
		}


		/// <summary>画像【移動】【縮尺処理】
		/// 
		/// </summary>
		/// <param name="picBox">画像表示先ピクチャーコントロール(カンバスの一部(全部)を表示)</param>
		/// <param name="canvas">画像描画先キャンバス</param>
		/// <param name="loadedImg">ファイルから読み込まれた画像</param>
		/// <param name="edtTyp">水平or垂直移動</param>
		private void movePicture(PictureBox picBox, Bitmap canvas, Image loadedImg, _EdtTyp edtTyp, int mvVal,Rectangle canvasRect) {
			Graphics g = Graphics.FromImage(canvas);
			//一旦画像クリア
			g.Clear(Color.Black);

			#region 画像【水平移動】
			if (edtTyp == _EdtTyp.Horizontal) {
				//変換行列初期化
				g.ResetTransform();
				//ピクチャボックスの中心＝カンバスの中心＋スライダー移動量　となるよう、変換行列をセット
				g.TranslateTransform(-_BckPicDrwPsOfstX - mvVal, -_BckPicDrwPsOfstY);
				//グラフィックオブジェクトに背景画像を背景画像の大きさで描画する。
				//-----------------------------------------------------------------------------------------------------
				//ＩＮＧ：_imgPicBack.Width/Heightではなく、_スケール.Width/Heightを用いる必要がある ↓
				//				           この場合だと、画像そのものの大きさ＝縮尺０↓             ↓
				//-----------------------------------------------------------------------------------------------------
				g.DrawImage(loadedImg, canvasRect);
				//PictureBox1に表示する
				picBox.Image = canvas;
				#region デバッグ出力
					#if DEBUG
				dbgShowVal.setVal(hrzBrBckMoveV: trckBrHrz.Value - _trckBrHrzBckInitVal);
					#endif
				#endregion
			}
			#endregion

			#region 画像【垂直移動】
			if (edtTyp == _EdtTyp.Vertical) {
				g.ResetTransform();
//label1.Text = "垂直移動量＝" + mvVal.ToString();
				//ピクチャボックスの中心＝カンバスの中心＋スライダー移動量　となるよう、変換行列をセット
				g.TranslateTransform(-_BckPicDrwPsOfstX, -_BckPicDrwPsOfstY + mvVal);
				g.DrawImage(loadedImg, canvasRect);

				//PictureBox1に表示する
				picBox.Image = canvas;
			}
			#endregion

			#region 画像【縮尺】
			float scaleVal = 0.0F;
			if (edtTyp == _EdtTyp.Scale) {
				g.ResetTransform();	//ワールド変換行列を単位行列にリセット
				if ((trckBrScl.Value - _SclTrckBrInitVal) >= 0) {//スライダーが中央より右にスライドした場合
					scaleVal = 1F + ((float)(trckBrScl.Value - _SclTrckBrInitVal)) / 100F;
					label1.Text = scaleVal.ToString();
					g.ScaleTransform(scaleVal, scaleVal);
				}
				g.DrawImage(loadedImg, canvasRect);
				//PictureBox1に表示する
				picBox.Image = _CanvasBck;
			}
			#endregion

			g.Dispose();
		}

		private void button1_Click(object sender, EventArgs e) {
			Graphics g = Graphics.FromImage(_CanvasBck);

			//一旦画像クリア
			g.Clear(Color.Black);
			//変換行列初期化
			g.ResetTransform();

label1.Text = _BckPicDrwPsOfstX.ToString();
			//ピクチャボックスの中心＝カンバスの中心となるよう、変換行列をセット
			g.TranslateTransform(-_BckPicDrwPsOfstX, -_BckPicDrwPsOfstY);
			g.DrawImage(_LoadedBckImgObj, new Rectangle(0, 0, _LoadedBckImgObj.Width, _LoadedBckImgObj.Height));

			//PictureBox1に表示する
			picBxBck.Image = _CanvasBck;

			g.Dispose();
		}
	}
}